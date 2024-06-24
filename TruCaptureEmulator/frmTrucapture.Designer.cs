namespace TruCaptureEmulator
{
    partial class frmTrucapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrucapture));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portaSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarParametrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.carregarParametrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxSend = new System.Windows.Forms.TextBox();
            this.sfdPerfil = new System.Windows.Forms.SaveFileDialog();
            this.ofdPerfil = new System.Windows.Forms.OpenFileDialog();
            this.pnlFunctions = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbcISError = new System.Windows.Forms.ComboBox();
            this.tgbISError = new br.com.ltb.GUI.ToggleButton();
            this.label15 = new System.Windows.Forms.Label();
            this.usbDesinibe = new br.com.ltb.GUI.UserButton();
            this.nupTempInibe = new System.Windows.Forms.NumericUpDown();
            this.tgbInterferencia = new br.com.ltb.GUI.ToggleButton();
            this.label14 = new System.Windows.Forms.Label();
            this.tgbSimulador = new br.com.ltb.GUI.ToggleButton();
            this.lblSimulador = new System.Windows.Forms.Label();
            this.slfDirecao = new br.com.ltb.GUI.SelectionFieldComponent();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ckbMulti = new br.com.ltb.GUI.ToggleButton();
            this.ckbSimulaErros = new br.com.ltb.GUI.ToggleButton();
            this.ckbBootError = new br.com.ltb.GUI.ToggleButton();
            this.ckbMdlimit = new br.com.ltb.GUI.ToggleButton();
            this.ckbIntervalVariavel = new br.com.ltb.GUI.ToggleButton();
            this.ckbInibe = new br.com.ltb.GUI.ToggleButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblImagens = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pbxTrucapture = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ctxSalvarLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.salvarLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDialogs = new System.Windows.Forms.Panel();
            this.lblRecebidos = new System.Windows.Forms.Label();
            this.lblEnviados = new System.Windows.Forms.Label();
            this.lblComport = new System.Windows.Forms.Label();
            this.sfdLog = new System.Windows.Forms.SaveFileDialog();
            this.rttTeste = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tgbLogAutomatico = new br.com.ltb.GUI.ToggleButton();
            this.usbOnOff = new br.com.ltb.GUI.UserButton();
            this.ckbData = new br.com.ltb.GUI.ToggleButton();
            this.usbLimpar = new br.com.ltb.GUI.UserButton();
            this.ubtStop = new br.com.ltb.GUI.UserButton();
            this.ubtStart = new br.com.ltb.GUI.UserButton();
            this.menuStrip1.SuspendLayout();
            this.pnlFunctions.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usbDesinibe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTempInibe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTrucapture)).BeginInit();
            this.ctxSalvarLog.SuspendLayout();
            this.pnlDialogs.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usbOnOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usbLimpar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubtStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubtStart)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraçãoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(903, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuraçãoToolStripMenuItem
            // 
            this.configuraçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portaSerialToolStripMenuItem,
            this.sensorToolStripMenuItem,
            this.salvarParametrosToolStripMenuItem,
            this.carregarParametrosToolStripMenuItem});
            this.configuraçãoToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.configuraçãoToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.configuraçãoToolStripMenuItem.Name = "configuraçãoToolStripMenuItem";
            this.configuraçãoToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.configuraçãoToolStripMenuItem.Text = "Settings";
            // 
            // portaSerialToolStripMenuItem
            // 
            this.portaSerialToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.portaSerialToolStripMenuItem.Name = "portaSerialToolStripMenuItem";
            this.portaSerialToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.portaSerialToolStripMenuItem.Text = "Serial Port";
            this.portaSerialToolStripMenuItem.Click += new System.EventHandler(this.portaSerialToolStripMenuItem_Click);
            // 
            // sensorToolStripMenuItem
            // 
            this.sensorToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.sensorToolStripMenuItem.Name = "sensorToolStripMenuItem";
            this.sensorToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.sensorToolStripMenuItem.Text = "Sensor";
            this.sensorToolStripMenuItem.Click += new System.EventHandler(this.sensorToolStripMenuItem_Click);
            // 
            // salvarParametrosToolStripMenuItem
            // 
            this.salvarParametrosToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.salvarParametrosToolStripMenuItem.Name = "salvarParametrosToolStripMenuItem";
            this.salvarParametrosToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.salvarParametrosToolStripMenuItem.Text = "Save Parameters";
            this.salvarParametrosToolStripMenuItem.Click += new System.EventHandler(this.salvarParametrosToolStripMenuItem_Click);
            // 
            // carregarParametrosToolStripMenuItem
            // 
            this.carregarParametrosToolStripMenuItem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.carregarParametrosToolStripMenuItem.Name = "carregarParametrosToolStripMenuItem";
            this.carregarParametrosToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.carregarParametrosToolStripMenuItem.Text = "Load Parameters";
            this.carregarParametrosToolStripMenuItem.Click += new System.EventHandler(this.carregarParametrosToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(352, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "SOFTWARE SERIAL INPUT :";
            // 
            // tbxSend
            // 
            this.tbxSend.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbxSend.Enabled = false;
            this.tbxSend.Location = new System.Drawing.Point(569, 123);
            this.tbxSend.Name = "tbxSend";
            this.tbxSend.Size = new System.Drawing.Size(319, 20);
            this.tbxSend.TabIndex = 8;
            this.tbxSend.TextChanged += new System.EventHandler(this.tbxSend_TextChanged);
            this.tbxSend.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxSend_KeyDown);
            // 
            // sfdPerfil
            // 
            this.sfdPerfil.FileName = "parameters.tcc";
            this.sfdPerfil.Filter = "Arquivo TCC|*.tcc";
            // 
            // ofdPerfil
            // 
            this.ofdPerfil.Filter = "Arquivo TCC|*.tcc";
            // 
            // pnlFunctions
            // 
            this.pnlFunctions.BackColor = System.Drawing.Color.White;
            this.pnlFunctions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFunctions.Controls.Add(this.panel1);
            this.pnlFunctions.Controls.Add(this.usbDesinibe);
            this.pnlFunctions.Controls.Add(this.nupTempInibe);
            this.pnlFunctions.Controls.Add(this.tgbInterferencia);
            this.pnlFunctions.Controls.Add(this.label14);
            this.pnlFunctions.Controls.Add(this.tgbSimulador);
            this.pnlFunctions.Controls.Add(this.lblSimulador);
            this.pnlFunctions.Controls.Add(this.slfDirecao);
            this.pnlFunctions.Controls.Add(this.label2);
            this.pnlFunctions.Controls.Add(this.label1);
            this.pnlFunctions.Controls.Add(this.numericUpDown1);
            this.pnlFunctions.Controls.Add(this.ckbMulti);
            this.pnlFunctions.Controls.Add(this.ckbSimulaErros);
            this.pnlFunctions.Controls.Add(this.ckbBootError);
            this.pnlFunctions.Controls.Add(this.ckbMdlimit);
            this.pnlFunctions.Controls.Add(this.ckbIntervalVariavel);
            this.pnlFunctions.Controls.Add(this.ckbInibe);
            this.pnlFunctions.Controls.Add(this.label10);
            this.pnlFunctions.Controls.Add(this.label9);
            this.pnlFunctions.Controls.Add(this.label8);
            this.pnlFunctions.Controls.Add(this.label6);
            this.pnlFunctions.Controls.Add(this.label5);
            this.pnlFunctions.Controls.Add(this.lblImagens);
            this.pnlFunctions.Enabled = false;
            this.pnlFunctions.Location = new System.Drawing.Point(5, 116);
            this.pnlFunctions.Name = "pnlFunctions";
            this.pnlFunctions.Size = new System.Drawing.Size(337, 543);
            this.pnlFunctions.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.cbcISError);
            this.panel1.Controls.Add(this.tgbISError);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Location = new System.Drawing.Point(6, 256);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 58);
            this.panel1.TabIndex = 44;
            // 
            // cbcISError
            // 
            this.cbcISError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.cbcISError.ForeColor = System.Drawing.Color.MidnightBlue;
            this.cbcISError.FormattingEnabled = true;
            this.cbcISError.Location = new System.Drawing.Point(15, 31);
            this.cbcISError.Name = "cbcISError";
            this.cbcISError.Size = new System.Drawing.Size(293, 24);
            this.cbcISError.TabIndex = 43;
            this.cbcISError.SelectedIndexChanged += new System.EventHandler(this.cbcISError_SelectedIndexChanged);
            // 
            // tgbISError
            // 
            this.tgbISError.Checked = false;
            this.tgbISError.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tgbISError.LineColor = System.Drawing.Color.LightBlue;
            this.tgbISError.Location = new System.Drawing.Point(213, 5);
            this.tgbISError.Name = "tgbISError";
            this.tgbISError.Size = new System.Drawing.Size(48, 25);
            this.tgbISError.TabIndex = 41;
            this.tgbISError.Text = "toggleButton5";
            this.tgbISError.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.toggleButton1_CheckedChanged);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label15.Location = new System.Drawing.Point(2, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(205, 22);
            this.label15.TabIndex = 42;
            this.label15.Text = "$IS ERROR";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // usbDesinibe
            // 
            this.usbDesinibe.CustomPressed = br.com.ltb.GUI.UserButton.CustomPressedType.WithoutRetention;
            this.usbDesinibe.DeepEffect = br.com.ltb.GUI.UserButton.DepthType.Shallow;
            this.usbDesinibe.Effect = br.com.ltb.GUI.UserButton.EffectType.ReziseDarkBorder;
            this.usbDesinibe.Image = global::TruCaptureEmulator.Properties.Resources.play2;
            this.usbDesinibe.Location = new System.Drawing.Point(308, 117);
            this.usbDesinibe.Name = "usbDesinibe";
            this.usbDesinibe.Pressed = false;
            this.usbDesinibe.PressedUserImage = null;
            this.usbDesinibe.Size = new System.Drawing.Size(25, 25);
            this.usbDesinibe.TabIndex = 41;
            this.usbDesinibe.TabStop = false;
            this.usbDesinibe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.usbDesinibe_MouseUp);
            // 
            // nupTempInibe
            // 
            this.nupTempInibe.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nupTempInibe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.nupTempInibe.ForeColor = System.Drawing.Color.MidnightBlue;
            this.nupTempInibe.Location = new System.Drawing.Point(270, 119);
            this.nupTempInibe.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nupTempInibe.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupTempInibe.Name = "nupTempInibe";
            this.nupTempInibe.Size = new System.Drawing.Size(36, 22);
            this.nupTempInibe.TabIndex = 41;
            this.nupTempInibe.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tgbInterferencia
            // 
            this.tgbInterferencia.Checked = false;
            this.tgbInterferencia.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tgbInterferencia.LineColor = System.Drawing.Color.LightBlue;
            this.tgbInterferencia.Location = new System.Drawing.Point(219, 390);
            this.tgbInterferencia.Name = "tgbInterferencia";
            this.tgbInterferencia.Size = new System.Drawing.Size(48, 25);
            this.tgbInterferencia.TabIndex = 40;
            this.tgbInterferencia.Text = "toggleButton6";
            this.tgbInterferencia.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.tgbInterferencia_CheckedChanged);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label14.Location = new System.Drawing.Point(7, 391);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(205, 22);
            this.label14.TabIndex = 39;
            this.label14.Text = "STRING NOISE";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tgbSimulador
            // 
            this.tgbSimulador.Checked = false;
            this.tgbSimulador.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tgbSimulador.LineColor = System.Drawing.Color.LightBlue;
            this.tgbSimulador.Location = new System.Drawing.Point(219, 357);
            this.tgbSimulador.Name = "tgbSimulador";
            this.tgbSimulador.Size = new System.Drawing.Size(48, 25);
            this.tgbSimulador.TabIndex = 38;
            this.tgbSimulador.Text = "toggleButton6";
            this.tgbSimulador.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.tgbSimulador_CheckedChanged);
            // 
            // lblSimulador
            // 
            this.lblSimulador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimulador.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSimulador.Location = new System.Drawing.Point(7, 358);
            this.lblSimulador.Name = "lblSimulador";
            this.lblSimulador.Size = new System.Drawing.Size(205, 22);
            this.lblSimulador.TabIndex = 37;
            this.lblSimulador.Text = "SIMULATOR MODE";
            this.lblSimulador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // slfDirecao
            // 
            this.slfDirecao.ArrowColor = System.Drawing.Color.White;
            this.slfDirecao.BackColor = System.Drawing.Color.Transparent;
            this.slfDirecao.BorderclickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(230)))));
            this.slfDirecao.ClickColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(230)))));
            this.slfDirecao.Color = System.Drawing.Color.MidnightBlue;
            this.slfDirecao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.slfDirecao.ForeColor = System.Drawing.Color.MidnightBlue;
            this.slfDirecao.Format = br.com.ltb.GUI.SelectionFieldComponent.VisualFormat.Rounded;
            this.slfDirecao.LineArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.slfDirecao.Location = new System.Drawing.Point(54, 467);
            this.slfDirecao.Name = "slfDirecao";
            this.slfDirecao.ScrollDirection = br.com.ltb.GUI.SelectionFieldComponent.Direction.Horizontal;
            this.slfDirecao.SelectedIndex = -1;
            this.slfDirecao.SelectedItem = "";
            this.slfDirecao.Size = new System.Drawing.Size(200, 25);
            this.slfDirecao.TabIndex = 34;
            this.slfDirecao.Text = "selectionFieldComponent1";
            this.slfDirecao.TextBackColor = System.Drawing.Color.White;
            this.slfDirecao.SelectedIndexChanged += new br.com.ltb.GUI.SelectionFieldComponent.SelectedIndexChangedEventHandler(this.slfDirecao_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(5, 446);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 22);
            this.label2.TabIndex = 33;
            this.label2.Text = "STRING DIRECTION:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(154, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "interval (ms)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.numericUpDown1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.numericUpDown1.Location = new System.Drawing.Point(48, 22);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown1.TabIndex = 22;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // ckbMulti
            // 
            this.ckbMulti.Checked = false;
            this.ckbMulti.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ckbMulti.LineColor = System.Drawing.Color.LightBlue;
            this.ckbMulti.Location = new System.Drawing.Point(219, 323);
            this.ckbMulti.Name = "ckbMulti";
            this.ckbMulti.Size = new System.Drawing.Size(48, 25);
            this.ckbMulti.TabIndex = 20;
            this.ckbMulti.Text = "toggleButton6";
            this.ckbMulti.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.ckbMulti_CheckedChanged);
            // 
            // ckbSimulaErros
            // 
            this.ckbSimulaErros.Checked = false;
            this.ckbSimulaErros.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ckbSimulaErros.LineColor = System.Drawing.Color.LightBlue;
            this.ckbSimulaErros.Location = new System.Drawing.Point(219, 189);
            this.ckbSimulaErros.Name = "ckbSimulaErros";
            this.ckbSimulaErros.Size = new System.Drawing.Size(48, 25);
            this.ckbSimulaErros.TabIndex = 19;
            this.ckbSimulaErros.Text = "toggleButton4";
            this.ckbSimulaErros.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.ckbSimulaErros_CheckedChanged);
            // 
            // ckbBootError
            // 
            this.ckbBootError.Checked = false;
            this.ckbBootError.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ckbBootError.LineColor = System.Drawing.Color.LightBlue;
            this.ckbBootError.Location = new System.Drawing.Point(219, 224);
            this.ckbBootError.Name = "ckbBootError";
            this.ckbBootError.Size = new System.Drawing.Size(48, 25);
            this.ckbBootError.TabIndex = 18;
            this.ckbBootError.Text = "toggleButton5";
            this.ckbBootError.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.ckbBootError_CheckedChanged);
            // 
            // ckbMdlimit
            // 
            this.ckbMdlimit.Checked = false;
            this.ckbMdlimit.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ckbMdlimit.LineColor = System.Drawing.Color.LightBlue;
            this.ckbMdlimit.Location = new System.Drawing.Point(219, 152);
            this.ckbMdlimit.Name = "ckbMdlimit";
            this.ckbMdlimit.Size = new System.Drawing.Size(48, 25);
            this.ckbMdlimit.TabIndex = 17;
            this.ckbMdlimit.Text = "toggleButton2";
            this.ckbMdlimit.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.ckbMdlimit_CheckedChanged);
            // 
            // ckbIntervalVariavel
            // 
            this.ckbIntervalVariavel.Checked = false;
            this.ckbIntervalVariavel.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ckbIntervalVariavel.LineColor = System.Drawing.Color.LightBlue;
            this.ckbIntervalVariavel.Location = new System.Drawing.Point(219, 83);
            this.ckbIntervalVariavel.Name = "ckbIntervalVariavel";
            this.ckbIntervalVariavel.Size = new System.Drawing.Size(48, 25);
            this.ckbIntervalVariavel.TabIndex = 15;
            this.ckbIntervalVariavel.Text = "toggleButton1";
            this.ckbIntervalVariavel.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.ckbIntervalVariavel_CheckedChanged);
            // 
            // ckbInibe
            // 
            this.ckbInibe.Checked = false;
            this.ckbInibe.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ckbInibe.LineColor = System.Drawing.Color.LightBlue;
            this.ckbInibe.Location = new System.Drawing.Point(219, 117);
            this.ckbInibe.Name = "ckbInibe";
            this.ckbInibe.Size = new System.Drawing.Size(48, 25);
            this.ckbInibe.TabIndex = 14;
            this.ckbInibe.Text = "toggleButton7";
            this.ckbInibe.CheckedChanged += new br.com.ltb.GUI.ToggleButton.CheckedC(this.ckbInibe_CheckedChanged);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label10.Location = new System.Drawing.Point(7, 324);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(205, 22);
            this.label10.TabIndex = 13;
            this.label10.Text = "MULTIPLE $SP STRINGS";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label9.Location = new System.Drawing.Point(7, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(205, 22);
            this.label9.TabIndex = 11;
            this.label9.Text = "BOOT ERROR";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label8.Location = new System.Drawing.Point(7, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(208, 22);
            this.label8.TabIndex = 10;
            this.label8.Text = "ERROR SIMULATION";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label6.Location = new System.Drawing.Point(5, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 22);
            this.label6.TabIndex = 8;
            this.label6.Text = "FIX CAPTURE POINT";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label5.Location = new System.Drawing.Point(7, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 22);
            this.label5.TabIndex = 7;
            this.label5.Text = "BLOCK $SP";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblImagens
            // 
            this.lblImagens.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImagens.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblImagens.Location = new System.Drawing.Point(7, 84);
            this.lblImagens.Name = "lblImagens";
            this.lblImagens.Size = new System.Drawing.Size(208, 22);
            this.lblImagens.TabIndex = 6;
            this.lblImagens.Text = "DYNAMIC INTERVAL";
            this.lblImagens.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label12.Location = new System.Drawing.Point(727, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 22);
            this.label12.TabIndex = 28;
            this.label12.Text = "HEADER";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbxTrucapture
            // 
            this.pbxTrucapture.Enabled = false;
            this.pbxTrucapture.Image = global::TruCaptureEmulator.Properties.Resources.TRUCAPTURE;
            this.pbxTrucapture.Location = new System.Drawing.Point(93, 49);
            this.pbxTrucapture.Name = "pbxTrucapture";
            this.pbxTrucapture.Size = new System.Drawing.Size(60, 41);
            this.pbxTrucapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxTrucapture.TabIndex = 33;
            this.pbxTrucapture.TabStop = false;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label13.Location = new System.Drawing.Point(677, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(147, 22);
            this.label13.TabIndex = 36;
            this.label13.Text = "AUTOMATIC LOG";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctxSalvarLog
            // 
            this.ctxSalvarLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salvarLogToolStripMenuItem});
            this.ctxSalvarLog.Name = "ctxSalvarLog";
            this.ctxSalvarLog.Size = new System.Drawing.Size(129, 26);
            this.ctxSalvarLog.Opening += new System.ComponentModel.CancelEventHandler(this.ctxSalvarLog_Opening);
            // 
            // salvarLogToolStripMenuItem
            // 
            this.salvarLogToolStripMenuItem.Name = "salvarLogToolStripMenuItem";
            this.salvarLogToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.salvarLogToolStripMenuItem.Text = "Salvar Log";
            this.salvarLogToolStripMenuItem.Click += new System.EventHandler(this.salvarLogToolStripMenuItem_Click);
            // 
            // pnlDialogs
            // 
            this.pnlDialogs.BackColor = System.Drawing.Color.DarkBlue;
            this.pnlDialogs.Controls.Add(this.lblRecebidos);
            this.pnlDialogs.Controls.Add(this.lblEnviados);
            this.pnlDialogs.Enabled = false;
            this.pnlDialogs.Location = new System.Drawing.Point(350, 627);
            this.pnlDialogs.Name = "pnlDialogs";
            this.pnlDialogs.Size = new System.Drawing.Size(541, 31);
            this.pnlDialogs.TabIndex = 39;
            // 
            // lblRecebidos
            // 
            this.lblRecebidos.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblRecebidos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecebidos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblRecebidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecebidos.ForeColor = System.Drawing.Color.White;
            this.lblRecebidos.Location = new System.Drawing.Point(276, 3);
            this.lblRecebidos.Name = "lblRecebidos";
            this.lblRecebidos.Size = new System.Drawing.Size(250, 25);
            this.lblRecebidos.TabIndex = 15;
            this.lblRecebidos.Text = "RECEIVED";
            this.lblRecebidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRecebidos.Click += new System.EventHandler(this.lblRecebidos_Click);
            // 
            // lblEnviados
            // 
            this.lblEnviados.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblEnviados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEnviados.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblEnviados.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnviados.ForeColor = System.Drawing.Color.White;
            this.lblEnviados.Location = new System.Drawing.Point(12, 3);
            this.lblEnviados.Name = "lblEnviados";
            this.lblEnviados.Size = new System.Drawing.Size(250, 25);
            this.lblEnviados.TabIndex = 14;
            this.lblEnviados.Text = "SENT";
            this.lblEnviados.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEnviados.Click += new System.EventHandler(this.lblEnviados_Click);
            // 
            // lblComport
            // 
            this.lblComport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblComport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblComport.Location = new System.Drawing.Point(159, 37);
            this.lblComport.Name = "lblComport";
            this.lblComport.Size = new System.Drawing.Size(171, 60);
            this.lblComport.TabIndex = 40;
            this.lblComport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sfdLog
            // 
            this.sfdLog.Filter = "Arquivo TXT|*.txt";
            // 
            // rttTeste
            // 
            this.rttTeste.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rttTeste.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rttTeste.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rttTeste.ContextMenuStrip = this.ctxSalvarLog;
            this.rttTeste.Location = new System.Drawing.Point(-1, -1);
            this.rttTeste.Name = "rttTeste";
            this.rttTeste.Size = new System.Drawing.Size(541, 467);
            this.rttTeste.TabIndex = 40;
            this.rttTeste.Text = "";
            this.rttTeste.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rttTeste_MouseClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rttTeste);
            this.panel2.Location = new System.Drawing.Point(348, 157);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 468);
            this.panel2.TabIndex = 38;
            // 
            // tgbLogAutomatico
            // 
            this.tgbLogAutomatico.Checked = false;
            this.tgbLogAutomatico.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tgbLogAutomatico.LineColor = System.Drawing.Color.LightBlue;
            this.tgbLogAutomatico.Location = new System.Drawing.Point(840, 70);
            this.tgbLogAutomatico.Name = "tgbLogAutomatico";
            this.tgbLogAutomatico.Size = new System.Drawing.Size(51, 29);
            this.tgbLogAutomatico.TabIndex = 35;
            this.tgbLogAutomatico.Text = "toggleButton1";
            // 
            // usbOnOff
            // 
            this.usbOnOff.CustomPressed = br.com.ltb.GUI.UserButton.CustomPressedType.WithRetention;
            this.usbOnOff.DeepEffect = br.com.ltb.GUI.UserButton.DepthType.Shallow;
            this.usbOnOff.Effect = br.com.ltb.GUI.UserButton.EffectType.CustomPressedImage;
            this.usbOnOff.Enabled = false;
            this.usbOnOff.Image = global::TruCaptureEmulator.Properties.Resources.ButtonOff;
            this.usbOnOff.Location = new System.Drawing.Point(27, 37);
            this.usbOnOff.Name = "usbOnOff";
            this.usbOnOff.Pressed = false;
            this.usbOnOff.PressedUserImage = global::TruCaptureEmulator.Properties.Resources.ButtonOn;
            this.usbOnOff.Size = new System.Drawing.Size(60, 60);
            this.usbOnOff.TabIndex = 34;
            this.usbOnOff.TabStop = false;
            this.usbOnOff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.usbOnOff_MouseUp);
            // 
            // ckbData
            // 
            this.ckbData.Checked = false;
            this.ckbData.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ckbData.LineColor = System.Drawing.Color.LightBlue;
            this.ckbData.Location = new System.Drawing.Point(840, 39);
            this.ckbData.Name = "ckbData";
            this.ckbData.Size = new System.Drawing.Size(51, 29);
            this.ckbData.TabIndex = 27;
            this.ckbData.Text = "toggleButton1";
            // 
            // usbLimpar
            // 
            this.usbLimpar.BackColor = System.Drawing.Color.White;
            this.usbLimpar.CustomPressed = br.com.ltb.GUI.UserButton.CustomPressedType.WithoutRetention;
            this.usbLimpar.DeepEffect = br.com.ltb.GUI.UserButton.DepthType.Shallow;
            this.usbLimpar.Effect = br.com.ltb.GUI.UserButton.EffectType.ReziseDarkBorder;
            this.usbLimpar.Enabled = false;
            this.usbLimpar.Image = global::TruCaptureEmulator.Properties.Resources.limpar;
            this.usbLimpar.Location = new System.Drawing.Point(481, 37);
            this.usbLimpar.Name = "usbLimpar";
            this.usbLimpar.Pressed = false;
            this.usbLimpar.PressedUserImage = null;
            this.usbLimpar.Size = new System.Drawing.Size(60, 60);
            this.usbLimpar.TabIndex = 26;
            this.usbLimpar.TabStop = false;
            this.usbLimpar.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // ubtStop
            // 
            this.ubtStop.CustomPressed = br.com.ltb.GUI.UserButton.CustomPressedType.WithoutRetention;
            this.ubtStop.DeepEffect = br.com.ltb.GUI.UserButton.DepthType.Shallow;
            this.ubtStop.Effect = br.com.ltb.GUI.UserButton.EffectType.ReziseDarkBorder;
            this.ubtStop.Enabled = false;
            this.ubtStop.Image = global::TruCaptureEmulator.Properties.Resources.stop2;
            this.ubtStop.Location = new System.Drawing.Point(411, 37);
            this.ubtStop.Name = "ubtStop";
            this.ubtStop.Pressed = false;
            this.ubtStop.PressedUserImage = null;
            this.ubtStop.Size = new System.Drawing.Size(60, 60);
            this.ubtStop.TabIndex = 25;
            this.ubtStop.TabStop = false;
            this.ubtStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ubtStop_MouseUp);
            // 
            // ubtStart
            // 
            this.ubtStart.CustomPressed = br.com.ltb.GUI.UserButton.CustomPressedType.WithoutRetention;
            this.ubtStart.DeepEffect = br.com.ltb.GUI.UserButton.DepthType.Shallow;
            this.ubtStart.Effect = br.com.ltb.GUI.UserButton.EffectType.ReziseDarkBorder;
            this.ubtStart.Enabled = false;
            this.ubtStart.Image = global::TruCaptureEmulator.Properties.Resources.play2;
            this.ubtStart.Location = new System.Drawing.Point(345, 37);
            this.ubtStart.Name = "ubtStart";
            this.ubtStart.Pressed = false;
            this.ubtStart.PressedUserImage = null;
            this.ubtStart.Size = new System.Drawing.Size(60, 60);
            this.ubtStart.TabIndex = 24;
            this.ubtStart.TabStop = false;
            this.ubtStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ubtStart_MouseUp);
            // 
            // frmTrucapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(903, 671);
            this.Controls.Add(this.lblComport);
            this.Controls.Add(this.pnlDialogs);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tgbLogAutomatico);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.usbOnOff);
            this.Controls.Add(this.pbxTrucapture);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ckbData);
            this.Controls.Add(this.usbLimpar);
            this.Controls.Add(this.ubtStop);
            this.Controls.Add(this.ubtStart);
            this.Controls.Add(this.pnlFunctions);
            this.Controls.Add(this.tbxSend);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmTrucapture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TruCapture Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlFunctions.ResumeLayout(false);
            this.pnlFunctions.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usbDesinibe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTempInibe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTrucapture)).EndInit();
            this.ctxSalvarLog.ResumeLayout(false);
            this.pnlDialogs.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usbOnOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usbLimpar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubtStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ubtStart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuraçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portaSerialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarParametrosToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxSend;
        private System.Windows.Forms.ToolStripMenuItem carregarParametrosToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdPerfil;
        private System.Windows.Forms.OpenFileDialog ofdPerfil;
        private System.Windows.Forms.Panel pnlFunctions;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblImagens;
        private br.com.ltb.GUI.ToggleButton ckbMulti;
        private br.com.ltb.GUI.ToggleButton ckbSimulaErros;
        private br.com.ltb.GUI.ToggleButton ckbBootError;
        private br.com.ltb.GUI.ToggleButton ckbMdlimit;
        private br.com.ltb.GUI.ToggleButton ckbIntervalVariavel;
        private br.com.ltb.GUI.ToggleButton ckbInibe;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private br.com.ltb.GUI.UserButton ubtStop;
        private br.com.ltb.GUI.UserButton ubtStart;
        private br.com.ltb.GUI.UserButton usbLimpar;
        private br.com.ltb.GUI.ToggleButton ckbData;
        private System.Windows.Forms.Label label12;
        private br.com.ltb.GUI.UserButton usbOnOff;
        private System.Windows.Forms.PictureBox pbxTrucapture;
        private System.Windows.Forms.Label label13;
        private br.com.ltb.GUI.ToggleButton tgbLogAutomatico;
        private System.Windows.Forms.Panel pnlDialogs;
        private System.Windows.Forms.Label lblRecebidos;
        private System.Windows.Forms.Label lblEnviados;
        private System.Windows.Forms.Label lblComport;
        private System.Windows.Forms.SaveFileDialog sfdLog;
        private System.Windows.Forms.ContextMenuStrip ctxSalvarLog;
        private System.Windows.Forms.ToolStripMenuItem salvarLogToolStripMenuItem;
        private br.com.ltb.GUI.ToggleButton tgbSimulador;
        private System.Windows.Forms.Label lblSimulador;
        private System.Windows.Forms.RichTextBox rttTeste;
        private System.Windows.Forms.Panel panel2;
        private br.com.ltb.GUI.ToggleButton tgbInterferencia;
        private System.Windows.Forms.Label label14;
        private br.com.ltb.GUI.UserButton usbDesinibe;
        private System.Windows.Forms.NumericUpDown nupTempInibe;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbcISError;
        private br.com.ltb.GUI.ToggleButton tgbISError;
        private System.Windows.Forms.Label label15;
        private br.com.ltb.GUI.SelectionFieldComponent slfDirecao;
        private System.Windows.Forms.Label label2;
    }
}

