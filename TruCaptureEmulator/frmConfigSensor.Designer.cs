namespace TruCaptureEmulator
{
    partial class frmConfigSensor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbError = new System.Windows.Forms.CheckBox();
            this.ckbTime = new System.Windows.Forms.CheckBox();
            this.ckbBanner = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDM = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxMM = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudVelmax = new System.Windows.Forms.NumericUpDown();
            this.nudVelmin = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.nudDecimalFixo = new System.Windows.Forms.NumericUpDown();
            this.ckbFixaDecimal = new System.Windows.Forms.CheckBox();
            this.nudDistmax = new System.Windows.Forms.NumericUpDown();
            this.nudDistmin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxSN = new System.Windows.Forms.TextBox();
            this.nupHT = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxMD = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nupMaxMultLeituras = new System.Windows.Forms.NumericUpDown();
            this.nupMinMultLeituras = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.nupSupMultLeituras = new System.Windows.Forms.NumericUpDown();
            this.nupInfMultiLeituras = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nupTempoMinMultLeituras = new System.Windows.Forms.NumericUpDown();
            this.nupTempoMaxMultLeituras = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmbSector4Type = new System.Windows.Forms.ComboBox();
            this.cmbDG = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnCRC = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.tbxInterferenciaManual = new System.Windows.Forms.TextBox();
            this.cmbInterferencia = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cmbIDs = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.nupSpeedCapture = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelmin)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalFixo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHT)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupMaxMultLeituras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinMultLeituras)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupSupMultLeituras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupInfMultiLeituras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTempoMinMultLeituras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTempoMaxMultLeituras)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupSpeedCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbError);
            this.groupBox1.Controls.Add(this.ckbTime);
            this.groupBox1.Controls.Add(this.ckbBanner);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 111);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output Format Settings:";
            // 
            // ckbError
            // 
            this.ckbError.AutoSize = true;
            this.ckbError.Location = new System.Drawing.Point(19, 73);
            this.ckbError.Name = "ckbError";
            this.ckbError.Size = new System.Drawing.Size(105, 17);
            this.ckbError.TabIndex = 2;
            this.ckbError.Text = "Error code ($DE)";
            this.ckbError.UseVisualStyleBackColor = true;
            // 
            // ckbTime
            // 
            this.ckbTime.AutoSize = true;
            this.ckbTime.Location = new System.Drawing.Point(19, 50);
            this.ckbTime.Name = "ckbTime";
            this.ckbTime.Size = new System.Drawing.Size(159, 17);
            this.ckbTime.TabIndex = 1;
            this.ckbTime.Text = "Time Since Power On ($DT)";
            this.ckbTime.UseVisualStyleBackColor = true;
            // 
            // ckbBanner
            // 
            this.ckbBanner.AutoSize = true;
            this.ckbBanner.Location = new System.Drawing.Point(19, 27);
            this.ckbBanner.Name = "ckbBanner";
            this.ckbBanner.Size = new System.Drawing.Size(90, 17);
            this.ckbBanner.TabIndex = 1;
            this.ckbBanner.Text = "Banner ($DB)";
            this.ckbBanner.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Display Mode ($DM):";
            // 
            // cbxDM
            // 
            this.cbxDM.FormattingEnabled = true;
            this.cbxDM.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cbxDM.Location = new System.Drawing.Point(319, 30);
            this.cbxDM.Name = "cbxDM";
            this.cbxDM.Size = new System.Drawing.Size(72, 21);
            this.cbxDM.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Set Mode ($MM):";
            // 
            // cbxMM
            // 
            this.cbxMM.FormattingEnabled = true;
            this.cbxMM.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.cbxMM.Location = new System.Drawing.Point(319, 2);
            this.cbxMM.Name = "cbxMM";
            this.cbxMM.Size = new System.Drawing.Size(72, 21);
            this.cbxMM.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nupSpeedCapture);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.nudVelmax);
            this.groupBox2.Controls.Add(this.nudVelmin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 85);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Speed Range";
            // 
            // nudVelmax
            // 
            this.nudVelmax.Location = new System.Drawing.Point(247, 20);
            this.nudVelmax.Maximum = new decimal(new int[] {
            322,
            0,
            0,
            0});
            this.nudVelmax.Name = "nudVelmax";
            this.nudVelmax.Size = new System.Drawing.Size(81, 20);
            this.nudVelmax.TabIndex = 6;
            // 
            // nudVelmin
            // 
            this.nudVelmin.Location = new System.Drawing.Point(88, 20);
            this.nudVelmin.Maximum = new decimal(new int[] {
            322,
            0,
            0,
            0});
            this.nudVelmin.Name = "nudVelmin";
            this.nudVelmin.Size = new System.Drawing.Size(81, 20);
            this.nudVelmin.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Max:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Min:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.nudDecimalFixo);
            this.groupBox3.Controls.Add(this.ckbFixaDecimal);
            this.groupBox3.Controls.Add(this.nudDistmax);
            this.groupBox3.Controls.Add(this.nudDistmin);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(13, 205);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(373, 93);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Distance Range";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(194, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 9;
            this.label18.Text = "Value:";
            // 
            // nudDecimalFixo
            // 
            this.nudDecimalFixo.Location = new System.Drawing.Point(246, 56);
            this.nudDecimalFixo.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudDecimalFixo.Name = "nudDecimalFixo";
            this.nudDecimalFixo.Size = new System.Drawing.Size(81, 20);
            this.nudDecimalFixo.TabIndex = 8;
            // 
            // ckbFixaDecimal
            // 
            this.ckbFixaDecimal.AutoSize = true;
            this.ckbFixaDecimal.Location = new System.Drawing.Point(49, 57);
            this.ckbFixaDecimal.Name = "ckbFixaDecimal";
            this.ckbFixaDecimal.Size = new System.Drawing.Size(104, 17);
            this.ckbFixaDecimal.TabIndex = 7;
            this.ckbFixaDecimal.Text = "Fix decimal point";
            this.ckbFixaDecimal.UseVisualStyleBackColor = true;
            // 
            // nudDistmax
            // 
            this.nudDistmax.Location = new System.Drawing.Point(247, 23);
            this.nudDistmax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDistmax.Name = "nudDistmax";
            this.nudDistmax.Size = new System.Drawing.Size(81, 20);
            this.nudDistmax.TabIndex = 6;
            // 
            // nudDistmin
            // 
            this.nudDistmin.Location = new System.Drawing.Point(88, 23);
            this.nudDistmin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDistmin.Name = "nudDistmin";
            this.nudDistmin.Size = new System.Drawing.Size(81, 20);
            this.nudDistmin.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Max:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Min:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(407, 164);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(112, 28);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "&Save";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(418, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Serial number:";
            // 
            // tbxSN
            // 
            this.tbxSN.Location = new System.Drawing.Point(409, 135);
            this.tbxSN.MaxLength = 8;
            this.tbxSN.Name = "tbxSN";
            this.tbxSN.Size = new System.Drawing.Size(107, 20);
            this.tbxSN.TabIndex = 10;
            // 
            // nupHT
            // 
            this.nupHT.Location = new System.Drawing.Point(319, 61);
            this.nupHT.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nupHT.Name = "nupHT";
            this.nupHT.Size = new System.Drawing.Size(72, 20);
            this.nupHT.TabIndex = 12;
            this.nupHT.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(252, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Height (HT):";
            // 
            // tbxMD
            // 
            this.tbxMD.Location = new System.Drawing.Point(319, 90);
            this.tbxMD.Name = "tbxMD";
            this.tbxMD.Size = new System.Drawing.Size(72, 20);
            this.tbxMD.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(219, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "MD(min,max,med):";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nupMaxMultLeituras);
            this.groupBox4.Controls.Add(this.nupMinMultLeituras);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(12, 301);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(373, 55);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Number of Measurements per cycle (multiple strings enabled):";
            // 
            // nupMaxMultLeituras
            // 
            this.nupMaxMultLeituras.Location = new System.Drawing.Point(247, 23);
            this.nupMaxMultLeituras.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nupMaxMultLeituras.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupMaxMultLeituras.Name = "nupMaxMultLeituras";
            this.nupMaxMultLeituras.Size = new System.Drawing.Size(81, 20);
            this.nupMaxMultLeituras.TabIndex = 6;
            this.nupMaxMultLeituras.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nupMinMultLeituras
            // 
            this.nupMinMultLeituras.Location = new System.Drawing.Point(88, 23);
            this.nupMinMultLeituras.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nupMinMultLeituras.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupMinMultLeituras.Name = "nupMinMultLeituras";
            this.nupMinMultLeituras.Size = new System.Drawing.Size(81, 20);
            this.nupMinMultLeituras.TabIndex = 5;
            this.nupMinMultLeituras.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(200, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Max:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(43, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Min:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.nupSupMultLeituras);
            this.groupBox5.Controls.Add(this.nupInfMultiLeituras);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(11, 359);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(375, 55);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Distance Range (multiple strings enabled):";
            // 
            // nupSupMultLeituras
            // 
            this.nupSupMultLeituras.Location = new System.Drawing.Point(247, 23);
            this.nupSupMultLeituras.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nupSupMultLeituras.Name = "nupSupMultLeituras";
            this.nupSupMultLeituras.Size = new System.Drawing.Size(81, 20);
            this.nupSupMultLeituras.TabIndex = 6;
            this.nupSupMultLeituras.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nupInfMultiLeituras
            // 
            this.nupInfMultiLeituras.Location = new System.Drawing.Point(88, 23);
            this.nupInfMultiLeituras.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nupInfMultiLeituras.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nupInfMultiLeituras.Name = "nupInfMultiLeituras";
            this.nupInfMultiLeituras.Size = new System.Drawing.Size(81, 20);
            this.nupInfMultiLeituras.TabIndex = 5;
            this.nupInfMultiLeituras.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(199, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Top:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(43, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "bottom:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TruCaptureEmulator.Properties.Resources.S200;
            this.pictureBox1.Location = new System.Drawing.Point(404, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 108);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(43, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Mínimo:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(200, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Máximo:";
            // 
            // nupTempoMinMultLeituras
            // 
            this.nupTempoMinMultLeituras.Location = new System.Drawing.Point(88, 23);
            this.nupTempoMinMultLeituras.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nupTempoMinMultLeituras.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nupTempoMinMultLeituras.Name = "nupTempoMinMultLeituras";
            this.nupTempoMinMultLeituras.Size = new System.Drawing.Size(81, 20);
            this.nupTempoMinMultLeituras.TabIndex = 5;
            this.nupTempoMinMultLeituras.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // nupTempoMaxMultLeituras
            // 
            this.nupTempoMaxMultLeituras.Location = new System.Drawing.Point(247, 23);
            this.nupTempoMaxMultLeituras.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nupTempoMaxMultLeituras.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nupTempoMaxMultLeituras.Name = "nupTempoMaxMultLeituras";
            this.nupTempoMaxMultLeituras.Size = new System.Drawing.Size(81, 20);
            this.nupTempoMaxMultLeituras.TabIndex = 6;
            this.nupTempoMaxMultLeituras.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(170, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "ms";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(328, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(20, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "ms";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmbSector4Type);
            this.groupBox7.Controls.Add(this.cmbDG);
            this.groupBox7.Location = new System.Drawing.Point(10, 413);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(376, 57);
            this.groupBox7.TabIndex = 21;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "DG State && Setor 4";
            // 
            // cmbSector4Type
            // 
            this.cmbSector4Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSector4Type.FormattingEnabled = true;
            this.cmbSector4Type.Location = new System.Drawing.Point(126, 23);
            this.cmbSector4Type.Name = "cmbSector4Type";
            this.cmbSector4Type.Size = new System.Drawing.Size(237, 21);
            this.cmbSector4Type.TabIndex = 21;
            // 
            // cmbDG
            // 
            this.cmbDG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDG.FormattingEnabled = true;
            this.cmbDG.Location = new System.Drawing.Point(8, 23);
            this.cmbDG.Name = "cmbDG";
            this.cmbDG.Size = new System.Drawing.Size(103, 21);
            this.cmbDG.TabIndex = 20;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.nupTempoMaxMultLeituras);
            this.groupBox6.Controls.Add(this.nupTempoMinMultLeituras);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Location = new System.Drawing.Point(11, 584);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(373, 55);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Tempo entre passagens (Intervalo Variável):";
            this.groupBox6.Visible = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnCRC);
            this.groupBox8.Controls.Add(this.label21);
            this.groupBox8.Controls.Add(this.tbxInterferenciaManual);
            this.groupBox8.Controls.Add(this.cmbInterferencia);
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Location = new System.Drawing.Point(10, 469);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(373, 55);
            this.groupBox8.TabIndex = 22;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "String Noise:";
            // 
            // btnCRC
            // 
            this.btnCRC.Location = new System.Drawing.Point(322, 20);
            this.btnCRC.Name = "btnCRC";
            this.btnCRC.Size = new System.Drawing.Size(45, 22);
            this.btnCRC.TabIndex = 16;
            this.btnCRC.Text = "&CRC";
            this.btnCRC.UseVisualStyleBackColor = true;
            this.btnCRC.Click += new System.EventHandler(this.btnCRC_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(127, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(45, 13);
            this.label21.TabIndex = 15;
            this.label21.Text = "Manual:";
            // 
            // tbxInterferenciaManual
            // 
            this.tbxInterferenciaManual.Location = new System.Drawing.Point(172, 22);
            this.tbxInterferenciaManual.Name = "tbxInterferenciaManual";
            this.tbxInterferenciaManual.Size = new System.Drawing.Size(140, 20);
            this.tbxInterferenciaManual.TabIndex = 14;
            // 
            // cmbInterferencia
            // 
            this.cmbInterferencia.FormattingEnabled = true;
            this.cmbInterferencia.Location = new System.Drawing.Point(34, 22);
            this.cmbInterferencia.Name = "cmbInterferencia";
            this.cmbInterferencia.Size = new System.Drawing.Size(90, 21);
            this.cmbInterferencia.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(4, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(34, 13);
            this.label20.TabIndex = 6;
            this.label20.Text = "Type:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cmbIDs);
            this.groupBox9.Controls.Add(this.label25);
            this.groupBox9.Location = new System.Drawing.Point(11, 525);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(373, 55);
            this.groupBox9.TabIndex = 23;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "ID Answer:";
            // 
            // cmbIDs
            // 
            this.cmbIDs.FormattingEnabled = true;
            this.cmbIDs.Location = new System.Drawing.Point(33, 24);
            this.cmbIDs.Name = "cmbIDs";
            this.cmbIDs.Size = new System.Drawing.Size(329, 21);
            this.cmbIDs.TabIndex = 8;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(21, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "ID:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(23, 59);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "Capture:";
            // 
            // nupSpeedCapture
            // 
            this.nupSpeedCapture.Location = new System.Drawing.Point(88, 57);
            this.nupSpeedCapture.Maximum = new decimal(new int[] {
            322,
            0,
            0,
            0});
            this.nupSpeedCapture.Name = "nupSpeedCapture";
            this.nupSpeedCapture.Size = new System.Drawing.Size(81, 20);
            this.nupSpeedCapture.TabIndex = 8;
            // 
            // frmConfigSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 583);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbxMD);
            this.Controls.Add(this.nupHT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbxSN);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbxMM);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbxDM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigSensor";
            this.Text = "Sensor Settings";
            this.Load += new System.EventHandler(this.frmConfigSensor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVelmin)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalFixo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHT)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupMaxMultLeituras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinMultLeituras)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupSupMultLeituras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupInfMultiLeituras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTempoMinMultLeituras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTempoMaxMultLeituras)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupSpeedCapture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbError;
        private System.Windows.Forms.CheckBox ckbTime;
        private System.Windows.Forms.CheckBox ckbBanner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDM;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxMM;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudVelmax;
        private System.Windows.Forms.NumericUpDown nudVelmin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown nudDistmax;
        private System.Windows.Forms.NumericUpDown nudDistmin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxSN;
        private System.Windows.Forms.NumericUpDown nupHT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxMD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nupMaxMultLeituras;
        private System.Windows.Forms.NumericUpDown nupMinMultLeituras;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown nupSupMultLeituras;
        private System.Windows.Forms.NumericUpDown nupInfMultiLeituras;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudDecimalFixo;
        private System.Windows.Forms.CheckBox ckbFixaDecimal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nupTempoMinMultLeituras;
        private System.Windows.Forms.NumericUpDown nupTempoMaxMultLeituras;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmbDG;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbxInterferenciaManual;
        private System.Windows.Forms.ComboBox cmbInterferencia;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnCRC;
        private System.Windows.Forms.ComboBox cmbSector4Type;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cmbIDs;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown nupSpeedCapture;
        private System.Windows.Forms.Label label19;
    }
}