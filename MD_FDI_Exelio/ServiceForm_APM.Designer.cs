
namespace MD_FDI_Exelio
{
    partial class ServiceForm_APM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm_APM));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbBlocked24 = new System.Windows.Forms.CheckBox();
            this.cbCMDforbiden = new System.Windows.Forms.CheckBox();
            this.cbAmountOverload = new System.Windows.Forms.CheckBox();
            this.cbRAMReset = new System.Windows.Forms.CheckBox();
            this.cbCoverOpened = new System.Windows.Forms.CheckBox();
            this.cbNoPaper = new System.Windows.Forms.CheckBox();
            this.cbLowPaper = new System.Windows.Forms.CheckBox();
            this.cbFiscalOpened = new System.Windows.Forms.CheckBox();
            this.cbEJ4000 = new System.Windows.Forms.CheckBox();
            this.cbEJ3000 = new System.Windows.Forms.CheckBox();
            this.cbEJ2000 = new System.Windows.Forms.CheckBox();
            this.cbClockErr = new System.Windows.Forms.CheckBox();
            this.cbDispError = new System.Windows.Forms.CheckBox();
            this.cbMechErr = new System.Windows.Forms.CheckBox();
            this.cbError = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnVoidFiscal = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSaleAmount = new System.Windows.Forms.TextBox();
            this.btnSale = new System.Windows.Forms.Button();
            this.chbCashless = new System.Windows.Forms.CheckBox();
            this.chbReturn = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPrintEJ = new System.Windows.Forms.Button();
            this.btnZRep = new System.Windows.Forms.Button();
            this.btnXRep = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCashOutAmount = new System.Windows.Forms.TextBox();
            this.tbCashInAmount = new System.Windows.Forms.TextBox();
            this.btnCashOut = new System.Windows.Forms.Button();
            this.btnCashIn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.ClockTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbBlocked24);
            this.groupBox1.Controls.Add(this.cbCMDforbiden);
            this.groupBox1.Controls.Add(this.cbAmountOverload);
            this.groupBox1.Controls.Add(this.cbRAMReset);
            this.groupBox1.Controls.Add(this.cbCoverOpened);
            this.groupBox1.Controls.Add(this.cbNoPaper);
            this.groupBox1.Controls.Add(this.cbLowPaper);
            this.groupBox1.Controls.Add(this.cbFiscalOpened);
            this.groupBox1.Controls.Add(this.cbEJ4000);
            this.groupBox1.Controls.Add(this.cbEJ3000);
            this.groupBox1.Controls.Add(this.cbEJ2000);
            this.groupBox1.Controls.Add(this.cbClockErr);
            this.groupBox1.Controls.Add(this.cbDispError);
            this.groupBox1.Controls.Add(this.cbMechErr);
            this.groupBox1.Controls.Add(this.cbError);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 209);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Статус ФР";
            // 
            // cbBlocked24
            // 
            this.cbBlocked24.AutoSize = true;
            this.cbBlocked24.Location = new System.Drawing.Point(8, 48);
            this.cbBlocked24.Name = "cbBlocked24";
            this.cbBlocked24.Size = new System.Drawing.Size(140, 17);
            this.cbBlocked24.TabIndex = 14;
            this.cbBlocked24.Text = "Заблокирован 24 часа";
            this.cbBlocked24.UseVisualStyleBackColor = true;
            // 
            // cbCMDforbiden
            // 
            this.cbCMDforbiden.AutoSize = true;
            this.cbCMDforbiden.Location = new System.Drawing.Point(154, 94);
            this.cbCMDforbiden.Name = "cbCMDforbiden";
            this.cbCMDforbiden.Size = new System.Drawing.Size(149, 17);
            this.cbCMDforbiden.TabIndex = 13;
            this.cbCMDforbiden.Text = "Выполнение запрещено";
            this.cbCMDforbiden.UseVisualStyleBackColor = true;
            // 
            // cbAmountOverload
            // 
            this.cbAmountOverload.AutoSize = true;
            this.cbAmountOverload.Location = new System.Drawing.Point(154, 71);
            this.cbAmountOverload.Name = "cbAmountOverload";
            this.cbAmountOverload.Size = new System.Drawing.Size(153, 17);
            this.cbAmountOverload.TabIndex = 12;
            this.cbAmountOverload.Text = "Поля сумм переполнены";
            this.cbAmountOverload.UseVisualStyleBackColor = true;
            // 
            // cbRAMReset
            // 
            this.cbRAMReset.AutoSize = true;
            this.cbRAMReset.Location = new System.Drawing.Point(154, 48);
            this.cbRAMReset.Name = "cbRAMReset";
            this.cbRAMReset.Size = new System.Drawing.Size(95, 17);
            this.cbRAMReset.TabIndex = 11;
            this.cbRAMReset.Text = "ОП сброшена";
            this.cbRAMReset.UseVisualStyleBackColor = true;
            // 
            // cbCoverOpened
            // 
            this.cbCoverOpened.AutoSize = true;
            this.cbCoverOpened.Location = new System.Drawing.Point(154, 25);
            this.cbCoverOpened.Name = "cbCoverOpened";
            this.cbCoverOpened.Size = new System.Drawing.Size(112, 17);
            this.cbCoverOpened.TabIndex = 10;
            this.cbCoverOpened.Text = "Крышка открыта";
            this.cbCoverOpened.UseVisualStyleBackColor = true;
            // 
            // cbNoPaper
            // 
            this.cbNoPaper.AutoSize = true;
            this.cbNoPaper.Location = new System.Drawing.Point(154, 163);
            this.cbNoPaper.Name = "cbNoPaper";
            this.cbNoPaper.Size = new System.Drawing.Size(131, 17);
            this.cbNoPaper.TabIndex = 9;
            this.cbNoPaper.Text = "Закончилась бумага";
            this.cbNoPaper.UseVisualStyleBackColor = true;
            // 
            // cbLowPaper
            // 
            this.cbLowPaper.AutoSize = true;
            this.cbLowPaper.Location = new System.Drawing.Point(154, 140);
            this.cbLowPaper.Name = "cbLowPaper";
            this.cbLowPaper.Size = new System.Drawing.Size(92, 17);
            this.cbLowPaper.TabIndex = 8;
            this.cbLowPaper.Text = "Мало бумаги";
            this.cbLowPaper.UseVisualStyleBackColor = true;
            // 
            // cbFiscalOpened
            // 
            this.cbFiscalOpened.AutoSize = true;
            this.cbFiscalOpened.Location = new System.Drawing.Point(154, 117);
            this.cbFiscalOpened.Name = "cbFiscalOpened";
            this.cbFiscalOpened.Size = new System.Drawing.Size(152, 17);
            this.cbFiscalOpened.TabIndex = 7;
            this.cbFiscalOpened.Text = "Фискальный чек открыт";
            this.cbFiscalOpened.UseVisualStyleBackColor = true;
            // 
            // cbEJ4000
            // 
            this.cbEJ4000.AutoSize = true;
            this.cbEJ4000.Location = new System.Drawing.Point(8, 186);
            this.cbEJ4000.Name = "cbEJ4000";
            this.cbEJ4000.Size = new System.Drawing.Size(140, 17);
            this.cbEJ4000.TabIndex = 6;
            this.cbEJ4000.Text = "ЭЖ меньше 4000 байт";
            this.cbEJ4000.UseVisualStyleBackColor = true;
            // 
            // cbEJ3000
            // 
            this.cbEJ3000.AutoSize = true;
            this.cbEJ3000.Location = new System.Drawing.Point(8, 163);
            this.cbEJ3000.Name = "cbEJ3000";
            this.cbEJ3000.Size = new System.Drawing.Size(140, 17);
            this.cbEJ3000.TabIndex = 5;
            this.cbEJ3000.Text = "ЭЖ меньше 3000 байт";
            this.cbEJ3000.UseVisualStyleBackColor = true;
            // 
            // cbEJ2000
            // 
            this.cbEJ2000.AutoSize = true;
            this.cbEJ2000.Location = new System.Drawing.Point(8, 140);
            this.cbEJ2000.Name = "cbEJ2000";
            this.cbEJ2000.Size = new System.Drawing.Size(140, 17);
            this.cbEJ2000.TabIndex = 4;
            this.cbEJ2000.Text = "ЭЖ меньше 2000 байт";
            this.cbEJ2000.UseVisualStyleBackColor = true;
            // 
            // cbClockErr
            // 
            this.cbClockErr.AutoSize = true;
            this.cbClockErr.Location = new System.Drawing.Point(8, 117);
            this.cbClockErr.Name = "cbClockErr";
            this.cbClockErr.Size = new System.Drawing.Size(138, 17);
            this.cbClockErr.TabIndex = 3;
            this.cbClockErr.Text = "Часы не установлены";
            this.cbClockErr.UseVisualStyleBackColor = true;
            // 
            // cbDispError
            // 
            this.cbDispError.AutoSize = true;
            this.cbDispError.Location = new System.Drawing.Point(8, 94);
            this.cbDispError.Name = "cbDispError";
            this.cbDispError.Size = new System.Drawing.Size(129, 17);
            this.cbDispError.TabIndex = 2;
            this.cbDispError.Text = "Дисп. не подключен";
            this.cbDispError.UseVisualStyleBackColor = true;
            // 
            // cbMechErr
            // 
            this.cbMechErr.AutoSize = true;
            this.cbMechErr.Location = new System.Drawing.Point(8, 71);
            this.cbMechErr.Name = "cbMechErr";
            this.cbMechErr.Size = new System.Drawing.Size(102, 17);
            this.cbMechErr.TabIndex = 1;
            this.cbMechErr.Text = "Мех. проблема";
            this.cbMechErr.UseVisualStyleBackColor = true;
            // 
            // cbError
            // 
            this.cbError.AutoSize = true;
            this.cbError.Location = new System.Drawing.Point(8, 25);
            this.cbError.Name = "cbError";
            this.cbError.Size = new System.Drawing.Size(66, 17);
            this.cbError.TabIndex = 0;
            this.cbError.Text = "Ошибка";
            this.cbError.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(318, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 271);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Дополнительные фискальные функции";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnVoidFiscal);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.tbSaleAmount);
            this.groupBox5.Controls.Add(this.btnSale);
            this.groupBox5.Controls.Add(this.chbCashless);
            this.groupBox5.Controls.Add(this.chbReturn);
            this.groupBox5.Location = new System.Drawing.Point(7, 160);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(262, 103);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Продажа";
            // 
            // btnVoidFiscal
            // 
            this.btnVoidFiscal.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnVoidFiscal.Location = new System.Drawing.Point(157, 73);
            this.btnVoidFiscal.Name = "btnVoidFiscal";
            this.btnVoidFiscal.Size = new System.Drawing.Size(100, 23);
            this.btnVoidFiscal.TabIndex = 5;
            this.btnVoidFiscal.Text = "Отмена чека";
            this.btnVoidFiscal.UseVisualStyleBackColor = false;
            this.btnVoidFiscal.Click += new System.EventHandler(this.btnVoidFiscal_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Сумма:";
            // 
            // tbSaleAmount
            // 
            this.tbSaleAmount.Location = new System.Drawing.Point(157, 19);
            this.tbSaleAmount.Name = "tbSaleAmount";
            this.tbSaleAmount.Size = new System.Drawing.Size(100, 20);
            this.tbSaleAmount.TabIndex = 3;
            this.tbSaleAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSale
            // 
            this.btnSale.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSale.Location = new System.Drawing.Point(157, 44);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(100, 23);
            this.btnSale.TabIndex = 2;
            this.btnSale.Text = "Продажа";
            this.btnSale.UseVisualStyleBackColor = false;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // chbCashless
            // 
            this.chbCashless.AutoSize = true;
            this.chbCashless.Location = new System.Drawing.Point(8, 46);
            this.chbCashless.Name = "chbCashless";
            this.chbCashless.Size = new System.Drawing.Size(92, 17);
            this.chbCashless.TabIndex = 1;
            this.chbCashless.Text = "Безналичная";
            this.chbCashless.UseVisualStyleBackColor = true;
            // 
            // chbReturn
            // 
            this.chbReturn.AutoSize = true;
            this.chbReturn.Location = new System.Drawing.Point(8, 22);
            this.chbReturn.Name = "chbReturn";
            this.chbReturn.Size = new System.Drawing.Size(68, 17);
            this.chbReturn.TabIndex = 0;
            this.chbReturn.Text = "Возврат";
            this.chbReturn.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPrintEJ);
            this.groupBox4.Controls.Add(this.btnZRep);
            this.groupBox4.Controls.Add(this.btnXRep);
            this.groupBox4.Location = new System.Drawing.Point(7, 102);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(262, 52);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Отчеты";
            // 
            // btnPrintEJ
            // 
            this.btnPrintEJ.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnPrintEJ.Location = new System.Drawing.Point(174, 19);
            this.btnPrintEJ.Name = "btnPrintEJ";
            this.btnPrintEJ.Size = new System.Drawing.Size(80, 25);
            this.btnPrintEJ.TabIndex = 2;
            this.btnPrintEJ.Text = "Печать ЕЖ";
            this.btnPrintEJ.UseVisualStyleBackColor = false;
            this.btnPrintEJ.Click += new System.EventHandler(this.btnPrintEJ_Click);
            // 
            // btnZRep
            // 
            this.btnZRep.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnZRep.Location = new System.Drawing.Point(90, 19);
            this.btnZRep.Name = "btnZRep";
            this.btnZRep.Size = new System.Drawing.Size(80, 25);
            this.btnZRep.TabIndex = 1;
            this.btnZRep.Text = "Z-Отчет";
            this.btnZRep.UseVisualStyleBackColor = false;
            this.btnZRep.Click += new System.EventHandler(this.btnZRep_Click);
            // 
            // btnXRep
            // 
            this.btnXRep.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnXRep.Location = new System.Drawing.Point(6, 19);
            this.btnXRep.Name = "btnXRep";
            this.btnXRep.Size = new System.Drawing.Size(80, 25);
            this.btnXRep.TabIndex = 0;
            this.btnXRep.Text = "Х-Отчет";
            this.btnXRep.UseVisualStyleBackColor = false;
            this.btnXRep.Click += new System.EventHandler(this.btnXRep_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbCashOutAmount);
            this.groupBox3.Controls.Add(this.tbCashInAmount);
            this.groupBox3.Controls.Add(this.btnCashOut);
            this.groupBox3.Controls.Add(this.btnCashIn);
            this.groupBox3.Location = new System.Drawing.Point(7, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(262, 75);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Внесение/Вынесение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Служебный вынос:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Служебный внос:";
            // 
            // tbCashOutAmount
            // 
            this.tbCashOutAmount.Location = new System.Drawing.Point(114, 48);
            this.tbCashOutAmount.Name = "tbCashOutAmount";
            this.tbCashOutAmount.Size = new System.Drawing.Size(62, 20);
            this.tbCashOutAmount.TabIndex = 3;
            this.tbCashOutAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCashOutAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbCashOut_KeyPress);
            // 
            // tbCashInAmount
            // 
            this.tbCashInAmount.Location = new System.Drawing.Point(114, 19);
            this.tbCashInAmount.Name = "tbCashInAmount";
            this.tbCashInAmount.Size = new System.Drawing.Size(62, 20);
            this.tbCashInAmount.TabIndex = 2;
            this.tbCashInAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCashInAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbCashIN_KeyPress);
            // 
            // btnCashOut
            // 
            this.btnCashOut.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCashOut.Location = new System.Drawing.Point(182, 46);
            this.btnCashOut.Name = "btnCashOut";
            this.btnCashOut.Size = new System.Drawing.Size(75, 25);
            this.btnCashOut.TabIndex = 1;
            this.btnCashOut.Text = "Вынос";
            this.btnCashOut.UseVisualStyleBackColor = false;
            this.btnCashOut.Click += new System.EventHandler(this.btnCashOut_Click);
            // 
            // btnCashIn
            // 
            this.btnCashIn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCashIn.Location = new System.Drawing.Point(182, 16);
            this.btnCashIn.Name = "btnCashIn";
            this.btnCashIn.Size = new System.Drawing.Size(75, 25);
            this.btnCashIn.TabIndex = 0;
            this.btnCashIn.Text = "Внос";
            this.btnCashIn.UseVisualStyleBackColor = false;
            this.btnCashIn.Click += new System.EventHandler(this.btnCashIn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Системное время:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(147, 270);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(49, 13);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "00:00:00";
            // 
            // ClockTimer
            // 
            this.ClockTimer.Enabled = true;
            this.ClockTimer.Interval = 1000;
            this.ClockTimer.Tick += new System.EventHandler(this.Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MD_FDI_Exelio.Properties.Resources.periprotect;
            this.pictureBox1.Location = new System.Drawing.Point(4, 220);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 47);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ServiceForm_APM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(602, 290);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServiceForm_APM";
            this.Text = "Дополнительные операции";
            this.Load += new System.EventHandler(this.ServiceForm_APM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbBlocked24;
        private System.Windows.Forms.CheckBox cbCMDforbiden;
        private System.Windows.Forms.CheckBox cbAmountOverload;
        private System.Windows.Forms.CheckBox cbRAMReset;
        private System.Windows.Forms.CheckBox cbCoverOpened;
        private System.Windows.Forms.CheckBox cbNoPaper;
        private System.Windows.Forms.CheckBox cbLowPaper;
        private System.Windows.Forms.CheckBox cbFiscalOpened;
        private System.Windows.Forms.CheckBox cbEJ4000;
        private System.Windows.Forms.CheckBox cbEJ3000;
        private System.Windows.Forms.CheckBox cbEJ2000;
        private System.Windows.Forms.CheckBox cbClockErr;
        private System.Windows.Forms.CheckBox cbDispError;
        private System.Windows.Forms.CheckBox cbMechErr;
        private System.Windows.Forms.CheckBox cbError;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCashOutAmount;
        private System.Windows.Forms.TextBox tbCashInAmount;
        private System.Windows.Forms.Button btnCashOut;
        private System.Windows.Forms.Button btnCashIn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnVoidFiscal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSaleAmount;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.CheckBox chbCashless;
        private System.Windows.Forms.CheckBox chbReturn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPrintEJ;
        private System.Windows.Forms.Button btnZRep;
        private System.Windows.Forms.Button btnXRep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer ClockTimer;
    }
}