using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            log.Write($"Connect result: {printer.Connect("COM7")}");
        }

        private void btnRunPaper_Click(object sender, EventArgs e)
        {
            log.Write($"Btn result: {printer.PaperOut()}");
        }
    }
}
