namespace TruCaptureEmulator
{
    partial class HTTPLog
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
            this.tbxHTTPServer = new System.Windows.Forms.TextBox();
            this.usbLimpar = new br.com.ltb.GUI.UserButton();
            ((System.ComponentModel.ISupportInitialize)(this.usbLimpar)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxHTTPServer
            // 
            this.tbxHTTPServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxHTTPServer.Location = new System.Drawing.Point(1, 3);
            this.tbxHTTPServer.Multiline = true;
            this.tbxHTTPServer.Name = "tbxHTTPServer";
            this.tbxHTTPServer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxHTTPServer.Size = new System.Drawing.Size(695, 161);
            this.tbxHTTPServer.TabIndex = 4;
            // 
            // usbLimpar
            // 
            this.usbLimpar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.usbLimpar.BackColor = System.Drawing.Color.White;
            this.usbLimpar.CustomPressed = br.com.ltb.GUI.UserButton.CustomPressedType.WithoutRetention;
            this.usbLimpar.DeepEffect = br.com.ltb.GUI.UserButton.DepthType.Shallow;
            this.usbLimpar.Effect = br.com.ltb.GUI.UserButton.EffectType.ReziseDarkBorder;
            this.usbLimpar.Image = global::TruCaptureEmulator.Properties.Resources.limpar;
            this.usbLimpar.Location = new System.Drawing.Point(625, 4);
            this.usbLimpar.Name = "usbLimpar";
            this.usbLimpar.Pressed = false;
            this.usbLimpar.PressedUserImage = null;
            this.usbLimpar.Size = new System.Drawing.Size(50, 50);
            this.usbLimpar.TabIndex = 20;
            this.usbLimpar.TabStop = false;
            this.usbLimpar.Click += new System.EventHandler(this.usbLimpar_Click);
            // 
            // HTTPLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 166);
            this.Controls.Add(this.usbLimpar);
            this.Controls.Add(this.tbxHTTPServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HTTPLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HTTPLog";
            ((System.ComponentModel.ISupportInitialize)(this.usbLimpar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxHTTPServer;
        private br.com.ltb.GUI.UserButton usbLimpar;
    }
}