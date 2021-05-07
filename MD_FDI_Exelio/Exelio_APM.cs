using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiData.FiscalDevices;

namespace MD_FDI_Exelio
{
    public class Exelio_APM : IFiscalDevice2, ICash
    {
        public string DeviceId => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string ShortName => throw new NotImplementedException();

        public FiscalDeviceCapabilities Capabilities => throw new NotImplementedException();

        public StateInfo DeviceState => throw new NotImplementedException();

        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;
        public event EventHandler<ErrorClearedEventArgs> ErrorCleared;
        public event EventHandler<IrregularityDetectedEventArgs> IrregularityDetected;
        public event EventHandler<EventArgs> DeviceStateChanged;
        public event EventHandler<JournalizeEventArgs> Journalize;
        public event EventHandler<TraceEventArgs> Trace;

        public Result AddDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Result AddItem(Item item)
        {
            throw new NotImplementedException();
        }

        public Result AddPayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Result CashIn(Cash cash)
        {
            throw new NotImplementedException();
        }

        public Result CashOut(Cash cash)
        {
            throw new NotImplementedException();
        }

        public TransactionResult CloseTransaction()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Result EndOfDay()
        {
            throw new NotImplementedException();
        }

        public Result Install(FiscalDeviceConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void Notify(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Result OpenTransaction(TransactionData transactionData)
        {
            throw new NotImplementedException();
        }

        public void SetDisplayLanguage(CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public void StartServiceDialog(IntPtr windowHandle, ServiceLevel serviceLevel)
        {
            throw new NotImplementedException();
        }

        public Result VoidTransaction()
        {
            throw new NotImplementedException();
        }

        Result IFiscalDevice.CloseTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
