
namespace TestForm
{
    partial class Form1
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpenReceipt = new System.Windows.Forms.Button();
            this.btnSale = new System.Windows.Forms.Button();
            this.btnCloseReceipt = new System.Windows.Forms.Button();
            this.btnXRep = new System.Windows.Forms.Button();
            this.btnZRep = new System.Windows.Forms.Button();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.btnGetTime = new System.Windows.Forms.Button();
            this.btnRunPaper = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(13, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(96, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(13, 415);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpenReceipt
            // 
            this.btnOpenReceipt.Location = new System.Drawing.Point(13, 42);
            this.btnOpenReceipt.Name = "btnOpenReceipt";
            this.btnOpenReceipt.Size = new System.Drawing.Size(96, 23);
            this.btnOpenReceipt.TabIndex = 2;
            this.btnOpenReceipt.Text = "Open Receipt";
            this.btnOpenReceipt.UseVisualStyleBackColor = true;
            // 
            // btnSale
            // 
            this.btnSale.Location = new System.Drawing.Point(13, 71);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(96, 23);
            this.btnSale.TabIndex = 3;
            this.btnSale.Text = "Sale";
            this.btnSale.UseVisualStyleBackColor = true;
            // 
            // btnCloseReceipt
            // 
            this.btnCloseReceipt.Location = new System.Drawing.Point(13, 100);
            this.btnCloseReceipt.Name = "btnCloseReceipt";
            this.btnCloseReceipt.Size = new System.Drawing.Size(96, 23);
            this.btnCloseReceipt.TabIndex = 4;
            this.btnCloseReceipt.Text = "Close Receipt";
            this.btnCloseReceipt.UseVisualStyleBackColor = true;
            // 
            // btnXRep
            // 
            this.btnXRep.Location = new System.Drawing.Point(13, 129);
            this.btnXRep.Name = "btnXRep";
            this.btnXRep.Size = new System.Drawing.Size(96, 23);
            this.btnXRep.TabIndex = 5;
            this.btnXRep.Text = "X-Report";
            this.btnXRep.UseVisualStyleBackColor = true;
            // 
            // btnZRep
            // 
            this.btnZRep.Location = new System.Drawing.Point(13, 158);
            this.btnZRep.Name = "btnZRep";
            this.btnZRep.Size = new System.Drawing.Size(96, 23);
            this.btnZRep.TabIndex = 6;
            this.btnZRep.Text = "Z-Report";
            this.btnZRep.UseVisualStyleBackColor = true;
            // 
            // btnSetTime
            // 
            this.btnSetTime.Location = new System.Drawing.Point(13, 187);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(96, 23);
            this.btnSetTime.TabIndex = 7;
            this.btnSetTime.Text = "Set Time";
            this.btnSetTime.UseVisualStyleBackColor = true;
            // 
            // btnGetTime
            // 
            this.btnGetTime.Location = new System.Drawing.Point(13, 216);
            this.btnGetTime.Name = "btnGetTime";
            this.btnGetTime.Size = new System.Drawing.Size(96, 23);
            this.btnGetTime.TabIndex = 8;
            this.btnGetTime.Text = "Get Time";
            this.btnGetTime.UseVisualStyleBackColor = true;
            // 
            // btnRunPaper
            // 
            this.btnRunPaper.Location = new System.Drawing.Point(13, 246);
            this.btnRunPaper.Name = "btnRunPaper";
            this.btnRunPaper.Size = new System.Drawing.Size(96, 23);
            this.btnRunPaper.TabIndex = 9;
            this.btnRunPaper.Text = "Run Paper";
            this.btnRunPaper.UseVisualStyleBackColor = true;
            this.btnRunPaper.Click += new System.EventHandler(this.btnRunPaper_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 450);
            this.Controls.Add(this.btnRunPaper);
            this.Controls.Add(this.btnGetTime);
            this.Controls.Add(this.btnSetTime);
            this.Controls.Add(this.btnZRep);
            this.Controls.Add(this.btnXRep);
            this.Controls.Add(this.btnCloseReceipt);
            this.Controls.Add(this.btnSale);
            this.Controls.Add(this.btnOpenReceipt);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpenReceipt;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnCloseReceipt;
        private System.Windows.Forms.Button btnXRep;
        private System.Windows.Forms.Button btnZRep;
        private System.Windows.Forms.Button btnSetTime;
        private System.Windows.Forms.Button btnGetTime;
        private System.Windows.Forms.Button btnRunPaper;
    }
}

