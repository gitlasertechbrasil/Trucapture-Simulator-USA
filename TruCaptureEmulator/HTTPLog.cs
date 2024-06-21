using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TruCaptureEmulator
{
    public partial class HTTPLog : Form
    {
        public HTTPLog()
        {
            InitializeComponent();            
        }


        public void SetText(string text)
        {
            try
            {
                if (tbxHTTPServer.Text.Length + text.Length >= tbxHTTPServer.MaxLength)
                {
                    tbxHTTPServer.Text = "";
                }
                tbxHTTPServer.AppendText(text);
            }
            catch (Exception ex)
            {

            }           
        }

        private void usbLimpar_Click(object sender, EventArgs e)
        {
            tbxHTTPServer.Text = string.Empty;
        }

        public void Limpar()
        {
            tbxHTTPServer.Text = string.Empty;
        }
    }
}
