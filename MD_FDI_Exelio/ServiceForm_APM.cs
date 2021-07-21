using SkiData.FiscalDevices;
using System;
using System.Windows.Forms;

namespace MD_FDI_Exelio
{
    public partial class ServiceForm_APM : Form
    {
        public delegate void StatusChanged(bool isready, int errorCode, string errorMessage);
        public event StatusChanged StatusChangedEvent;
        private ExellioLib printer;
        FiscalDevice_APM fd;
        string MachineId;
        public delegate void ErrorCheckHandler();
        public event ErrorCheckHandler ErrorCheckEvent;
        ConfirmationWindow cw;
        string errStr = "";

        private void OnStatusChangedEvent(bool isready, int errorCode, string errorMessage)
        {
            if (StatusChangedEvent != null)
            {
                StatusChangedEvent(isready, errorCode, errorMessage);
            }
        }

        public ServiceForm_APM(ExellioLib printer, string machineID, FiscalDevice_APM fd)
        {
            InitializeComponent();
            lblTime.Text = DateTime.Now.ToString("dd-MM-yy HH: mm:ss");
            this.printer = printer;
            MachineId = machineID;
            this.fd = fd;
            Transaction tr = new Transaction();
            tbxAmount.Text = tr.Get().ToString();
            ClockTimer.Enabled = true;
        }

         ~ServiceForm_APM()
        {
            ClockTimer.Enabled = false;
        }

        private void Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd-MM-yy HH: mm:ss");
        } 

        private void ServiceForm_APM_Load(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void cw_NumReadyEvent(byte type, bool isOk)
        {
            Enabled = false;
            bool receiptDone = true;
            Logger log = new Logger(MachineId);
            if (isOk)
            {
                if (type == 0)
                {
                    try
                    {
                        double sum = double.Parse(tbCashInAmount.Text.Replace('.', ','));
                        fd.ErrorAnalizer((uint)printer.CashInOut(sum));
                        UpdateStatus();
                        if (fd.deviceState.FiscalDeviceReady)
                        {
                            Transaction tr = new Transaction(3, Convert.ToUInt32(sum));
                            tr.UpdateData(tr);
                        }
                        log.Write($"FDFS: CashIn amount: {sum}, result: {fd.deviceState.FiscalDeviceReady}");
                    }
                    catch (Exception ex)
                    {
                        log.Write($"FDFS: CashIn exception: {ex.Message}");
                        OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                        UpdateStatus();
                        Enabled = true;
                        MessageBox.Show(new Form { TopMost = true }, "Ошибка!!!\n" + ex.Message);
                    }
                }
                if (type == 1)
                {
                    try
                    {
                        double sum = double.Parse(tbCashOutAmount.Text.Replace('.', ','));
                        if (fd.deviceState.FiscalDeviceReady)
                        {
                            int err = printer.CashInOut(-sum);
                            if (err != 0)
                            {
                                log.Write($"FDFS: CashOut (InOut) error: {err}");
                                receiptDone &= fd.ErrorAnalizer((uint)err);
                            }
                        }

                        if (fd.deviceState.FiscalDeviceReady)
                        {
                            Transaction tr = new Transaction(4, Convert.ToUInt32(sum));
                            tr.UpdateData(tr);
                        }
                        log.Write($"FDFS: CashOut amount: {sum}, result: {fd.deviceState.FiscalDeviceReady}");
                    }
                    catch (Exception ex)
                    {
                        log.Write($"FDFS: CashOut exception: {ex.Message}");
                        OnStatusChangedEvent(false, (int)SkiDataErrorCode.DeviceError, ex.Message);
                        UpdateStatus();
                        Enabled = true;
                        MessageBox.Show(new Form { TopMost = true }, "Ошибка!!!\n" + ex.Message);
                    }
                }
            }
            Enabled = true;
            UpdateStatus();
        }

        private void txbCashIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnCashIn_Click(sender, null);
            }
        }
        private void txbCashOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && (e.KeyChar != 8) && (e.KeyChar != 44) && (e.KeyChar != 46);
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                btnCashOut_Click(sender, null);
            }
        }

        private void UpdateStatus()
        {
            cbClockErr.Checked = printer.clockError;
            cbCoverOpened.Checked = printer.coverPaper;
            cbDispError.Checked = printer.noDisplay;
            cbEJ2000.Checked = printer.lowCriticalECL;
            cbEJ3000.Checked = printer.low3000ECL;
            cbEJ4000.Checked = printer.low4000ECL;
            cbError.Checked = printer.generalError;
            cbFiscalOpened.Checked = printer.fiscalOpened;
            cbLowPaper.Checked = printer.lowPaper;
            cbMechErr.Checked = printer.printerError;
            cbNoPaper.Checked = printer.outOfPaper;
        }

        private void btnCashIn_Click(object sender, EventArgs e)
        {
            if (tbCashInAmount.Text != "")
            {
                if (cw == null)
                {
                    cw = new ConfirmationWindow(0, double.Parse(tbCashInAmount.Text.Replace('.', ',')));
                    cw.NumReadyEvent += cw_NumReadyEvent;
                    cw.ShowDialog();
                    cw = null;
                    tbCashInAmount.Text = "";
                }
            }
        }

        private void btnCashOut_Click(object sender, EventArgs e)
        {
            if (tbCashOutAmount.Text != "")
            {
                if (cw == null)
                {
                    cw = new ConfirmationWindow(1, double.Parse(tbCashOutAmount.Text.Replace('.', ',')));
                    cw.NumReadyEvent += cw_NumReadyEvent;
                    cw.ShowDialog();
                    cw = null;
                    tbCashOutAmount.Text = "";
                }
            }
        }

        private void btnXRep_Click(object sender, EventArgs e)
        {
            try
            {
                fd.ErrorAnalizer((uint)printer.XReport());
                UpdateStatus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnZRep_Click(object sender, EventArgs e)
        {
            try
            {
                printer.ZReport();
                UpdateStatus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrintEJ_Click(object sender, EventArgs e)
        {
            try
            {
                printer.PrintZReport();
                UpdateStatus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            try
            {
                double sum = double.Parse(tbSaleAmount.Text.Replace('.', ','));
                switch (chbCashless.Checked)
                {
                    case true:
                        printer.OpenReceipt(1, "0000", 1);
                        printer.FiscalText("Special sale");
                        printer.Sale("Parking", sum, 1, "Hrs");
                        printer.Total(2, sum);
                        printer.CloseReceipt();
                        break;
                    case false:
                        printer.OpenReceipt(1, "0000", 1);
                        printer.FiscalText("Special sale");
                        printer.Sale("Parking", sum, 1, "Hrs");
                        printer.Total(1, sum);
                        printer.CloseReceipt();
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateStatus();
        }

        private void btnVoidFiscal_Click(object sender, EventArgs e)
        {
            try
            {
                printer.VoidReceipt();
                UpdateStatus();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UInt32 newAmount = 0;
            UInt32.TryParse(tbxAmount.Text, out newAmount);
            Transaction tr = new Transaction();
            tr.Set(newAmount);
        }
    }
}
