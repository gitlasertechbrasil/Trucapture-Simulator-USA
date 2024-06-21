namespace TruCaptureEmulator
{
    partial class frmDisplayConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplayConfig));
            this.btnEnviar = new System.Windows.Forms.Button();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nupVelocidade = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optDistancia = new System.Windows.Forms.RadioButton();
            this.optVelocidade = new System.Windows.Forms.RadioButton();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.tbxporta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbDisplay = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nupVelocidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(227, 216);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 1;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "HH";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(281, 30);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowUpDown = true;
            this.dtpStart.Size = new System.Drawing.Size(55, 20);
            this.dtpStart.TabIndex = 2;
            this.dtpStart.Value = new System.DateTime(2014, 8, 29, 18, 0, 0, 0);
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "HH";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(281, 59);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(55, 20);
            this.dtpEnd.TabIndex = 2;
            this.dtpEnd.Value = new System.DateTime(2014, 8, 29, 6, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "PeríodoNoturno:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hora de Início:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hora de Término:";
            // 
            // nupVelocidade
            // 
            this.nupVelocidade.Location = new System.Drawing.Point(281, 90);
            this.nupVelocidade.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nupVelocidade.Name = "nupVelocidade";
            this.nupVelocidade.Size = new System.Drawing.Size(55, 20);
            this.nupVelocidade.TabIndex = 5;
            this.nupVelocidade.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Velocidade:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(56, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optDistancia);
            this.groupBox1.Controls.Add(this.optVelocidade);
            this.groupBox1.Location = new System.Drawing.Point(186, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 93);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modo de operação:";
            // 
            // optDistancia
            // 
            this.optDistancia.AutoSize = true;
            this.optDistancia.Location = new System.Drawing.Point(13, 52);
            this.optDistancia.Name = "optDistancia";
            this.optDistancia.Size = new System.Drawing.Size(69, 17);
            this.optDistancia.TabIndex = 1;
            this.optDistancia.TabStop = true;
            this.optDistancia.Text = "Distância";
            this.optDistancia.UseVisualStyleBackColor = true;
            // 
            // optVelocidade
            // 
            this.optVelocidade.AutoSize = true;
            this.optVelocidade.Checked = true;
            this.optVelocidade.Location = new System.Drawing.Point(13, 19);
            this.optVelocidade.Name = "optVelocidade";
            this.optVelocidade.Size = new System.Drawing.Size(78, 17);
            this.optVelocidade.TabIndex = 0;
            this.optVelocidade.TabStop = true;
            this.optVelocidade.Text = "Velocidade";
            this.optVelocidade.UseVisualStyleBackColor = true;
            // 
            // tbxIP
            // 
            this.tbxIP.Location = new System.Drawing.Point(29, 117);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(130, 20);
            this.tbxIP.TabIndex = 7;
            this.tbxIP.Text = "192.168.50.149";
            // 
            // tbxporta
            // 
            this.tbxporta.Location = new System.Drawing.Point(29, 169);
            this.tbxporta.Name = "tbxporta";
            this.tbxporta.Size = new System.Drawing.Size(130, 20);
            this.tbxporta.TabIndex = 8;
            this.tbxporta.Text = "2101";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "IP:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Porta:";
            // 
            // ckbDisplay
            // 
            this.ckbDisplay.AutoSize = true;
            this.ckbDisplay.Enabled = false;
            this.ckbDisplay.Location = new System.Drawing.Point(29, 205);
            this.ckbDisplay.Name = "ckbDisplay";
            this.ckbDisplay.Size = new System.Drawing.Size(115, 17);
            this.ckbDisplay.TabIndex = 15;
            this.ckbDisplay.Text = "Enviar para display";
            this.ckbDisplay.UseVisualStyleBackColor = true;
            this.ckbDisplay.CheckedChanged += new System.EventHandler(this.ckbDisplay_CheckedChanged);
            // 
            // frmDisplayConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 251);
            this.Controls.Add(this.ckbDisplay);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxporta);
            this.Controls.Add(this.tbxIP);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nupVelocidade);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDisplayConfig";
            this.Text = "Configuração do Display";
            this.Load += new System.EventHandler(this.frmDisplayConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nupVelocidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nupVelocidade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optDistancia;
        private System.Windows.Forms.RadioButton optVelocidade;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.TextBox tbxporta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckbDisplay;
    }
}