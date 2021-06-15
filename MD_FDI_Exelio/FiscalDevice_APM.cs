using SkiData.FiscalDevices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Timers;

namespace MD_FDI_Exelio
{
    public class FiscalDevice_APM : IFiscalDevice, ICash
    {
        #region Fields
        private FiscalDeviceCapabilities fiscalDeviceCapabilities;
        public static System.Timers.Timer reCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer timeCheckTimer = new System.Timers.Timer(60000);
        public static System.Timers.Timer paymentTimeoutTimer = new System.Timers.Timer(285000);
        public bool inTransaction = false;
        public bool transactionTimeOutExceed = false;
        private bool disposed;
        //public bool isReady = true;
        public StateInfo deviceState;
        public static bool shiftNotClosed = false;
        private ServiceForm_APM sf = null;
        string transaction = "";
        private byte paymentType;
        public string paymentMachineName = "";
        public string paymentMachineId = "";
        public static string MachineID = "";
        public string connectionString = "";
        public DeviceType paymentMachineType;
        bool zrepDone = false, shiftStarted = false;
        List<Item> items;  //  Temp container for Item. Try to print two receipts on one list.
        List<Payment> payments; // Temp container for Payment. Try to print two receipts on one list.
        ExellioLib printer = new ExellioLib();
        public FiscalDeviceConfiguration fiscalDeviceConfiguration;
        #endregion

        #region Construction

        public FiscalDevice_APM()
        {
            OnTrace(new TraceEventArgs("Constructor called...", TraceLevel.Info));
            fiscalDeviceCapabilities = new FiscalDeviceCapabilities(true, false, true);
        }

        #endregion

        #region IFiscal Members
        public string DeviceId => "";

        public string Name => "MD_Exelio_FiscalDevice";

        public string ShortName => "Exelio";

        public StateInfo DeviceState => deviceState;

        public FiscalDeviceCapabilities Capabilities => fiscalDeviceCapabilities;

        public void Notify(int notificationId)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Notify {notificationId}");
            switch (notificationId)
            {
                case 1:
                    if (sf != null)
                    {
                        sf.Close();
                    }
                    break;
                default:
                    break;
            }
        }

        public Result Install(FiscalDeviceConfiguration configuration)
        {
            inTransaction = true;
            fiscalDeviceConfiguration = configuration;
            paymentMachineId = configuration.DeviceId;
            MachineID = paymentMachineId;
            paymentMachineName = configuration.DeviceName;
            connectionString = $"COM{configuration.CommunicationChannel}";
            Logger log = new Logger(MachineID);
            deviceState = new StateInfo(true, SkiDataErrorCode.Ok, "New connection");
            try
            {
                int err = printer.Connect(connectionString);
                if (err == 0)
                {
                    StatusChangedEvent(true, (int)deviceState.ErrorCode, "РРО подлючен");
                }
                else
                {
                    ErrorAnalizer((uint)err);
                }

                SetTimer();
                if (deviceState.FiscalDeviceReady)
                {
                    try
                    {
                        err = printer.PrinterStatus();
                        if (err != 0)
                        {
                            ErrorAnalizer((uint)err);
                        }

                        inTransaction = false;
                    }
                    catch (Exception e)
                    {

                        log.Write($"FD  : Install - Exception: {e.Message}");
                        StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                        inTransaction = false;
                    }
                }
            }
            catch(Exception e)
            {
                log.Write($"FD  : Install Exception - {e.Message}");
                inTransaction = false;
            }
            log.Write($"FD  : Install - {deviceState.FiscalDeviceReady}");
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result OpenTransaction(TransactionData transactionData)
        {
            Logger log = new Logger(MachineID);
            items = new List<Item>();
            payments = new List<Payment>();
            inTransaction = true;
            transaction = transactionData.ReferenceId;
            SetPaymentTimeoutTimerTimer();
            log.Write($"FD  : OpenTransaction: {deviceState.FiscalDeviceReady}");
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result AddItem(Item item)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Add Item - Item ID: {item.Id}, Item Name: {item.Name}, Item Quantity: {item.Quantity}");
            items.Add(item);
            return new Result(true);
        }

        public Result AddDiscount(Discount discount)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Add discount {discount.Amount}");
            return new Result(true);
        }

        public Result AddPayment(Payment payment)
        {
            Logger log = new Logger(MachineID);
            log.Write($"FD  : Add Payment - Payment Type: {payment.PaymentType}, Payment Amount: {payment.Amount}");
            payments.Add(payment);
            return new Result(true);
        }

        public Result CloseTransaction()
        {
            bool doSale = true;
            bool receiptDone = true;
            if (!transactionTimeOutExceed)
            {
                Logger log = new Logger(MachineID);
                log.Write($"FD  : Close Transaction");
                string paymentTypeStr = "";
                try
                {
                    int err = printer.OpenReceipt(1, "0000", 1);
                    if (err != 0)
                    {
                        receiptDone = ErrorAnalizer((uint)err);
                    }

                    foreach (Payment payment in payments)
                    {
                        if (payment.PaymentType == PaymentType.CreditCard)
                        {
                            log.Write($"FD  : Print bank response");
                            #region Add Receipt from bank
                            try
                            {
                                SQLConnect sql = new SQLConnect();
                                printer.FiscalText("---Ответ банка----");
                                log.Write($"FD  : Transaction ID: {transaction}");
                                string[] lines = sql.GetTransactionFromDBbyDevice(paymentMachineId, transaction).Split('\n');

                                for (int line = 0; line < lines.Length; line++)
                                {
                                    if (lines[line].Length > 1)
                                    {
                                        printer.FiscalText(lines[line]);
                                    }
                                }
                                printer.FiscalText("------------------------");
                            }
                            catch (Exception e)
                            {
                                log.Write($"FD  : Print bank response exception: {e.Message}");
                            }
                            #endregion
                        }
                    }

                    #region Add Item
                    foreach (Item item in items)
                    {
                        if (item.TotalPrice < 0)
                        {
                            doSale = doSale & false;
                        }
                    }
                    if (doSale)
                    {
                        foreach (Item item in items)
                        {
                            ParkingItem parkingItem = item as ParkingItem;
                            if (parkingItem != null)
                            {
                                DateTime myEntryTime = parkingItem.EntryTime;
                                DateTime myExitTime = parkingItem.PaidUntil;
                                err = printer.FiscalText($"ID:{parkingItem.TicketId}");
                                if (err != 0)
                                {
                                    receiptDone = receiptDone & ErrorAnalizer((uint)err);
                                }

                                err = printer.FiscalText($"   Въезд в:{myEntryTime.ToString(@"dd.MM.yy HH:mm:ss")}");
                                if (err != 0)
                                {
                                    receiptDone = receiptDone & ErrorAnalizer((uint)err);
                                }

                                err = printer.FiscalText($"Выехать до:{myExitTime.ToString(@"dd.MM.yy HH:mm:ss")}");
                                if (err != 0)
                                {
                                    receiptDone = receiptDone & ErrorAnalizer((uint)err);
                                }
                            }
                            ulong code = Convert.ToUInt64(item.Id);
                            int quantity = Convert.ToInt32(item.Quantity);
                            uint price = Convert.ToUInt32(item.UnitPrice);
                            string unit = item.QuantityUnit;
                            string name = item.Name;
                            log.Write($"FD  : Unit: {unit}, Item: {name}");
                            if (deviceState.FiscalDeviceReady)
                            {
                                err = printer.Sale(name, price, quantity, unit);
                                if (err != 0)
                                {
                                    receiptDone &= ErrorAnalizer((uint)err);
                                }
                            }
                            log.Write($"FD  : Total price: {price}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        #endregion

                        #region Add Payment
                        foreach (Payment payment in payments)
                        {
                            switch (payment.PaymentType)
                            {
                                case PaymentType.Cash:
                                    paymentType = 1;
                                    break;
                                case PaymentType.CreditCard:
                                    paymentType = 2;
                                    break;
                                default:
                                    paymentType = 1;
                                    break;
                            }
                            //uint sum = Convert.ToUInt32(payment.Amount);
                            if (deviceState.FiscalDeviceReady)
                            {
                                err = printer.Total(paymentType, (double)payment.Amount);
                                if (err != 0)
                                {
                                    receiptDone &= ErrorAnalizer((uint)err);
                                }
                            }
                            log.Write($"FD  : Paid amount: {payment.Amount}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        #endregion

                        #region CloseReceipt
                        if (deviceState.FiscalDeviceReady)
                        {
                            err = printer.CloseReceipt();
                            if (err != 0)
                            {
                                receiptDone &= ErrorAnalizer((uint)err);
                            }
                            log.Write($"FD  : Close receipt, result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        if (!deviceState.FiscalDeviceReady)
                        {
                            receiptDone = false;
                            err = printer.VoidReceipt();
                            if (err != 0)
                            {
                                ErrorAnalizer((uint)err);
                            }
                        }
                        if (deviceState.FiscalDeviceReady)
                        {
                            foreach (Payment payment in payments)
                            {
                                if (payment.PaymentType == PaymentType.Cash)
                                {
                                    Transaction tr = new Transaction(paymentType, Convert.ToUInt32(payment.Amount));
                                    tr.UpdateData(tr);
                                    log.Write($"TR  : Payment serialization: {paymentTypeStr} {Convert.ToUInt32(payment.Amount)}");
                                }
                            }
                        }
                        inTransaction = false;
                        log.Write($"FD  : Transaction result: {deviceState.FiscalDeviceReady & receiptDone}");
                        inTransaction = false;
                        #endregion
                    }
                    else
                    {
                        receiptDone = false;
                        err = printer.VoidReceipt();
                        if (err != 0)
                        {
                            ErrorAnalizer((uint)err);
                        }
                        log.Write($"FD  : Credit Entry detected. Void receipt.");
                        return new TransactionResult(false, "Credit Entry detected");
                    }
                }
                catch (Exception e)
                {
                    int err = printer.VoidReceipt();
                    receiptDone = false;
                    if (err != 0)
                    {
                        ErrorAnalizer((uint)err);
                    }
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                    log.Write($"FD  : Exception for Close Transaction: {e.Message}");
                    inTransaction = false;
                }
                transaction = null;
                inTransaction = false;
                return new TransactionResult(deviceState.FiscalDeviceReady & receiptDone, $" ");
            }
            else
            {
                Logger log = new Logger(MachineID);
                log.Write($"FD  : Payment time out");
                return new TransactionResult(false, "Transaction canceled by Time out");
                inTransaction = false;
            }
        }

        public void SetDisplayLanguage(CultureInfo cultureInfo)
        {
            new Result(true);
        }

        public void StartServiceDialog(IntPtr windowHandle, ServiceLevel serviceLevel)
        {
            try
            {
                if (sf == null)
                {
                    sf = new ServiceForm_APM(printer, MachineID, this);
                    //this.sf.StatusChangedEvent += StatusChangedEvent;
                    sf.ShowDialog();
                    sf = null;
                }
            }
            catch (Exception)
            {
            }
        }

        public Result VoidTransaction()
        {
            Logger log = new Logger(MachineID);
            try
            {
                if (transaction != null)
                {
                    int err = printer.VoidReceipt();
                    if (err != 0)
                    {
                        ErrorAnalizer((uint)err);
                    }
                }
                log.Write($"FD  : Void Transaction, result: {deviceState.FiscalDeviceReady}");
                inTransaction = false;
            }
            catch (Exception e)
            {
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                log.Write($"FD  : Exception for VoidTransaction: {e.Message}");
                inTransaction = false;
            }
            return new Result(deviceState.FiscalDeviceReady);
        }

        public Result EndOfDay()
        {
            return new Result(true);
        }
        #endregion

        #region ICash Members
        public Result CashIn(Cash cash)
        {
            Logger log = new Logger(MachineID);
            bool receiptDone = true;
            inTransaction = true;
            uint amount = 0;
            if (cash.Amount >= 0)
            {
                amount = Convert.ToUInt32(cash.Amount) * 100;
            }
            else
            {
                amount = Convert.ToUInt32(-cash.Amount) * 100;
            }

            try
            {
                if (deviceState.FiscalDeviceReady)
                {
                    int err = printer.CashInOut((double)cash.Amount);
                    if (err != 0)
                    {
                        receiptDone = ErrorAnalizer((uint)err);
                    }
                }
                log.Write($"FD  : CashIn amount: {cash.Amount} from source: {cash.Source.ToString()}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                if (deviceState.FiscalDeviceReady)
                {
                    Transaction tr = new Transaction(3, amount);
                    tr.UpdateData(tr);
                    log.Write($"TR  : CashIn Serialization: {amount}");
                }
            }
            catch (Exception e)
            {
                receiptDone = false;
                log.Write($"FD  : CashIn exception: {e.Message}");
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                inTransaction = false;
            }
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady & receiptDone);
        }

        public Result CashOut(Cash cash)
        {
            Logger log = new Logger(MachineID);
            bool receiptDone = true;
            inTransaction = true;
            uint amount = 0;
            if (cash.Amount >= 0)
            {
                amount = Convert.ToUInt32(cash.Amount) * 100;
            }
            else
            {
                amount = Convert.ToUInt32(-cash.Amount) * 100;
            }

            try
            {
                if (deviceState.FiscalDeviceReady)
                {
                    int err = printer.CashInOut(-(double)cash.Amount);
                    if (err != 0)
                    {
                        receiptDone = ErrorAnalizer((uint)err);
                    }

                }
                log.Write($"FD  : CashOut amount: {cash.Amount} from source: {cash.Source.ToString()}, result: {deviceState.FiscalDeviceReady & receiptDone}");
                if (deviceState.FiscalDeviceReady)
                {
                    Transaction tr = new Transaction(3, amount);
                    tr.UpdateData(tr);
                    log.Write($"TR  : CashIn Serialization: {amount}");
                }
            }
            catch (Exception e)
            {
                receiptDone = false;
                log.Write($"FD  : CashOut exception: {e.Message}");
                StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, e.Message);
                inTransaction = false;
            }
            inTransaction = false;
            return new Result(deviceState.FiscalDeviceReady & receiptDone);
        }
        #endregion

        #region Destructors
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    printer.Close();
                }
            }
            disposed = true;
        }

        ~FiscalDevice_APM()
        {
            Dispose(false);
        }
        #endregion

        #region Fiscal Device Events
        public event EventHandler<EventArgs> DeviceStateChanged;
        public event EventHandler<ErrorClearedEventArgs> ErrorCleared;
        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        public event EventHandler<IrregularityDetectedEventArgs> IrregularityDetected;
        public event EventHandler<JournalizeEventArgs> Journalize;
        public event EventHandler<TraceEventArgs> Trace;

        public void OnTrace(TraceEventArgs args)
        {
            if (Trace != null)
            {
                Trace(this, args);
            }
        }

        public void OnDeviceStateChanged(EventArgs args)
        {
            if (DeviceStateChanged != null)
            {
                DeviceStateChanged(this, args);
            }
        }

        public void OnErrorOccurred(ErrorOccurredEventArgs args)
        {
            OnTrace(new TraceEventArgs($"Error {args.ErrorMessage} occurred", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, args.ErrorMessage);
            if (ErrorOccurred != null)
            {
                ErrorOccurred(this, args);
            }
        }

        public void OnErrorCleared(ErrorClearedEventArgs args)
        {
            OnTrace(new TraceEventArgs($"Error {deviceState.ErrorCode} cleared", TraceLevel.Error));
            deviceState = new StateInfo(args.FiscalDeviceReady, args.ErrorCode, "Ok");
            if (ErrorCleared != null)
            {
                ErrorCleared(this, args);
            }
        }

        public void OnIrregularityDetected(IrregularityDetectedEventArgs args)
        {
            if (IrregularityDetected != null)
            {
                IrregularityDetected(this, args);
            }
        }

        public void OnJournalize(JournalizeEventArgs args)
        {
            if (Journalize != null)
            {
                Journalize(this, args);
            }
        }
        #endregion

        #region Custom methods
        public bool ErrorAnalizer(uint error)
        {
            /*
             * 0 - No error
             * 3 - Connection error
             * 
             * 100 - General error
             * 101 - Printer error
             * 102 - Critical Low EJ Memmory
             * 107 - Fiscal receipt already opened
             * 110 - Out of Paper
             * 111 - Cover not closed
             * 200 - Low paper
             * 201 - Low memmory (3000)
             * 202 - Low memmory (4000)
             * 
             */
            Logger log = new Logger(MachineID);
            if (error != 0)
            {
                log.Write($"EA  : Error: ({error})");
            }

            switch (error)
            {
                case 1:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, "Ошибка");
                    return false;
                case 2:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, "Ошибка");
                    return false;
                case 3:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.CommunicationError, "Нет связи с принтером");
                    return false;
                case 100:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "Ошибка");
                    return false;
                case 101:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "Механическая проблема принтера");
                    return false;
                case 102:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "Закончилась память ЭЖ");
                    return false;
                case 107:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "Фискальный чек уже открыто");
                    return false;
                case 110:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.OutOfPaper, "Закончилась бумага");
                    return false;
                case 111:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.PaperJam, "Крышка принтера не закрыта");
                    return false;
                case 200:
                    StatusChangedEvent(true, (int)SkiDataErrorCode.PaperLowWarning, "Заканчивается бумага");
                    return true;
                case 201:
                    StatusChangedEvent(true, (int)SkiDataErrorCode.OtherWarning, "Мало памяти ЭЖ (3000)");
                    return true;
                case 202:
                    StatusChangedEvent(true, (int)SkiDataErrorCode.OtherWarning, "Мало памяти ЭЖ (4000)");
                    return true;

                default:
                    StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, "Ошибка");
                    return false;
            }
        }


        private void SetTimer()
        {
            timeCheckTimer.Elapsed += OnTimedEvent;
            timeCheckTimer.AutoReset = true;
            timeCheckTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            bool receiptDone = true;
            Logger log = new Logger(MachineID);
            TimeSpan startZRep = TimeSpan.Parse("23:57");
            TimeSpan endZRep = TimeSpan.Parse("23:58");
            TimeSpan startShift = TimeSpan.Parse("00:02");
            TimeSpan endShift = TimeSpan.Parse("00:04");
            TimeSpan now = DateTime.Now.TimeOfDay;
            # region Automated End of Day
            if (!inTransaction)
            {
                if (startZRep <= endZRep)
                {
                    if ((now >= startZRep && now <= endZRep) && (!zrepDone))
                    {
                        try
                        {
                            log.Write($"FDAU: Close shift");
                            int err = printer.ZReport();
                            if (err != 0)
                            {
                                ErrorAnalizer((uint)err);
                            }

                            if (deviceState.FiscalDeviceReady)
                            {
                                zrepDone = true;
                                shiftStarted = false;
                            }
                            log.Write($"FDAU: Close shift result: {deviceState.FiscalDeviceReady}");
                        }
                        catch (Exception exc)
                        {
                            log.Write($"FDAU: Close shift exception: {exc.Message}");
                            StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, exc.Message);
                        }
                    }
                }
            }
            #endregion

            #region Automated New day

            if (!inTransaction)
            {
                if (startShift <= endShift)
                {
                    if ((now >= startShift && now <= endShift) && (!shiftStarted) && (zrepDone))
                    {
                        try
                        {
                            log.Write($"FDAU: New Shift");
                            StatusChangedEvent(true, (int)SkiDataErrorCode.Ok, "New shift");
                            int err = printer.PrinterStatus();
                            if (err != 0)
                            {
                                ErrorAnalizer((uint)err);
                            }

                            if (deviceState.FiscalDeviceReady)
                            {
                                err = printer.SetDateTime();
                                if (err != 0)
                                {
                                    ErrorAnalizer((uint)err);
                                }

                                if (deviceState.FiscalDeviceReady)
                                {
                                    shiftStarted = true;
                                }
                            }
                            if (deviceState.FiscalDeviceReady)
                            {
                                Transaction tr = new Transaction();
                                err = printer.CashInOut(tr.Get());
                                if (err != 0)
                                {
                                    receiptDone &= ErrorAnalizer((uint)err);
                                }

                                log.Write($"FDAU: Transfer amount result: {deviceState.FiscalDeviceReady}");
                            }
                            zrepDone = false;
                            log.Write($"FDAU: New shift result: {deviceState.FiscalDeviceReady & receiptDone}");
                        }
                        catch (Exception exc)
                        {
                            log.Write($"FDAU: New shift exception: {exc.Message}");
                            StatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, exc.Message);
                        }
                    }
                }
            }
            #endregion
        }

        private void StatusChangedEvent(bool isready, int errorCode, string errorMessage)
        {
            if (isready)
            {
                if (!deviceState.FiscalDeviceReady)
                {
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, true));
                }
            }
            else
            {
                if (deviceState.FiscalDeviceReady)
                {
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, false));
                }
                else
                {
                    OnErrorCleared(new ErrorClearedEventArgs(deviceState.ErrorCode, false));
                    OnErrorOccurred(new ErrorOccurredEventArgs(errorMessage, (SkiDataErrorCode)errorCode, false));
                }
            }
            Logger log = new Logger(MachineID);
            log.Write($"FDST: Device State: {deviceState.FiscalDeviceReady}, description: {deviceState.Description}");
        }

        private void SetPaymentTimeoutTimerTimer()
        {
            paymentTimeoutTimer.Elapsed += OnPaymentTimeOutTimerEvent;
            paymentTimeoutTimer.AutoReset = false;
            paymentTimeoutTimer.Enabled = true;
            transactionTimeOutExceed = false;
        }

        private void OnPaymentTimeOutTimerEvent(object sender, ElapsedEventArgs e)
        {
            transactionTimeOutExceed = true;
            paymentTimeoutTimer.Enabled = false;
            paymentTimeoutTimer.Elapsed -= OnPaymentTimeOutTimerEvent;
        }

        #endregion
    }
}
