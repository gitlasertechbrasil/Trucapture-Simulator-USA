namespace TruCaptureEmulator
{
    partial class frmConfig_serial
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
            this.cbxPortname = new System.Windows.Forms.ComboBox();
            this.cbxData = new System.Windows.Forms.ComboBox();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.cbxStop = new System.Windows.Forms.ComboBox();
            this.cbxHandshake = new System.Windows.Forms.ComboBox();
            this.cbxBaud = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxPortname
            // 
            this.cbxPortname.FormattingEnabled = true;
            this.cbxPortname.Location = new System.Drawing.Point(125, 12);
            this.cbxPortname.Name = "cbxPortname";
            this.cbxPortname.Size = new System.Drawing.Size(165, 21);
            this.cbxPortname.TabIndex = 0;
            // 
            // cbxData
            // 
            this.cbxData.FormattingEnabled = true;
            this.cbxData.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbxData.Location = new System.Drawing.Point(125, 66);
            this.cbxData.Name = "cbxData";
            this.cbxData.Size = new System.Drawing.Size(165, 21);
            this.cbxData.TabIndex = 1;
            // 
            // cbxParity
            // 
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbxParity.Location = new System.Drawing.Point(125, 93);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(165, 21);
            this.cbxParity.TabIndex = 2;
            // 
            // cbxStop
            // 
            this.cbxStop.FormattingEnabled = true;
            this.cbxStop.Items.AddRange(new object[] {
            "None",
            "One",
            "Two",
            "OnePointFive"});
            this.cbxStop.Location = new System.Drawing.Point(125, 120);
            this.cbxStop.Name = "cbxStop";
            this.cbxStop.Size = new System.Drawing.Size(165, 21);
            this.cbxStop.TabIndex = 3;
            // 
            // cbxHandshake
            // 
            this.cbxHandshake.FormattingEnabled = true;
            this.cbxHandshake.Items.AddRange(new object[] {
            "None",
            "XOnXOff",
            "RequestToSend",
            "RequestToSendXOnXOff"});
            this.cbxHandshake.Location = new System.Drawing.Point(125, 147);
            this.cbxHandshake.Name = "cbxHandshake";
            this.cbxHandshake.Size = new System.Drawing.Size(165, 21);
            this.cbxHandshake.TabIndex = 4;
            // 
            // cbxBaud
            // 
            this.cbxBaud.FormattingEnabled = true;
            this.cbxBaud.Items.AddRange(new object[] {
            "100",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cbxBaud.Location = new System.Drawing.Point(125, 39);
            this.cbxBaud.Name = "cbxBaud";
            this.cbxBaud.Size = new System.Drawing.Size(165, 21);
            this.cbxBaud.TabIndex = 0;
            this.cbxBaud.SelectedIndexChanged += new System.EventHandler(this.cbxBaud_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "PORT NAME:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "BAUD RATE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "DATA BITS:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "PARITY:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "STOP BITS:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "HANDSHAKE:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(107, 174);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(116, 32);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "&Save";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmConfig_serial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 216);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxHandshake);
            this.Controls.Add(this.cbxStop);
            this.Controls.Add(this.cbxParity);
            this.Controls.Add(this.cbxData);
            this.Controls.Add(this.cbxBaud);
            this.Controls.Add(this.cbxPortname);
            this.Name = "frmConfig_serial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Serial port settings";
            this.Load += new System.EventHandler(this.frmConfig_serial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxPortname;
        private System.Windows.Forms.ComboBox cbxData;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.ComboBox cbxStop;
        private System.Windows.Forms.ComboBox cbxHandshake;
        private System.Windows.Forms.ComboBox cbxBaud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOk;
    }
}