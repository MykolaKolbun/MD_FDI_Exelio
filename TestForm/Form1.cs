using System;
using System.Windows.Forms;

namespace TestForm
{
    public partial class Form1 : Form
    {

        ExcelioLibT printer = new ExcelioLibT();
        LoggerT log = new LoggerT("MD");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            log.Write($"Connect result: {printer.Connect("COM8")}");
        }

        private void btnRunPaper_Click(object sender, EventArgs e)
        {
            printer.PrinterStatus();
            SetCboxes();
        }

        private void btnGetTime_Click(object sender, EventArgs e)
        {
            log.Write($"Btn result: {printer.VoidReceipt()}");
            SetCboxes();
        }

        private void btnXRep_Click(object sender, EventArgs e)
        {
            log.Write($"Btn result: {printer.XReport()}");
            SetCboxes();
        }

        private void btnOpenReceipt_Click(object sender, EventArgs e)
        {
            log.Write($"Btn Open Receipt result: {printer.OpenReceipt(1, "0000", 1)}");
            SetCboxes();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            printer.FiscalText("Ticket nr: 123456789");
            printer.FiscalText("Entry time: 12:34 56.78.09");
            printer.FiscalText("Exit time:  12:34 56.78.90");
            printer.FiscalText("Ticket nr: 123456789");
            printer.FiscalText("Ticket nr: 123456789");
            printer.FiscalText("Ticket nr: 123456789");
            log.Write($"Btn Open Receipt result: {printer.Sale("Parking", 0.3, 5, "Hrs")}");
            SetCboxes();
        }

        private void btnCloseReceipt_Click(object sender, EventArgs e)
        {
            log.Write($"Btn Open Receipt result: {printer.CloseReceipt()}");
            SetCboxes();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            log.Write($"Btn Open Receipt result: {printer.Total(1, 0.3)}");
            log.Write($"Btn Open Receipt result: {printer.Total(2, (1.5 - 0.3))}");
            SetCboxes();
        }

        private void btnZRep_Click(object sender, EventArgs e)
        {
            log.Write($"Btn result: {printer.ZReport()}");
            SetCboxes();
        }

        private void btnPrintZ_Click(object sender, EventArgs e)
        {
            log.Write($"Btn result: {printer.PrintZReport()}");
            SetCboxes();
        }


        private void SetCboxes()
        {
            cb02.Checked = printer.clockError;
            cb03.Checked = printer.noDisplay;
            cb04.Checked = printer.printerError;
            cb05.Checked = printer.generalError;

            cb15.Checked = printer.coverPaper;

            cb20.Checked = printer.outOfPaper;
            cb21.Checked = printer.lowPaper;
            cb23.Checked = printer.fiscalOpened;
            cb25.Checked = printer.fiscalNotOpened;
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
        }

        private void btnCashIn_Click(object sender, EventArgs e)
        {
            printer.CashInOut(125);
            SetCboxes();
        }

        private void btnCashOut_Click(object sender, EventArgs e)
        {
            printer.CashInOut(-125);
            SetCboxes();
        }
    }
}
