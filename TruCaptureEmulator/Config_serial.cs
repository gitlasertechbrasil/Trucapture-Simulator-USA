using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace TruCaptureEmulator
{
    public partial class frmConfig_serial : Form
    {

        private SerialPort serialPort1;
        //private frmTrucapture.configserial c;
        private bool OK = false;

        public bool OK1
        {
            get { return OK; }
            set { OK = value; }
        }

        public frmConfig_serial(SerialPort serialPort1)
        {
            InitializeComponent();
           this.serialPort1 = serialPort1;
            //this.c = c;
        }

        private void frmConfig_serial_Load(object sender, EventArgs e)
        {
            try
            {
                int indice = 0;
                int indsel = 0;
                string[] portas = SerialPort.GetPortNames();
                Array.Sort(portas);
                
                foreach (string x in portas)
                {
                    if (x.Equals(serialPort1.PortName))
                    {
                        indsel = indice;
                    }

                    cbxPortname.Items.Add(x);
                    indice++;
                }

                cbxPortname.SelectedIndex = indsel;
                cbxBaud.SelectedIndex = cbxBaud.FindString(serialPort1.BaudRate.ToString());
                cbxData.SelectedIndex = cbxData.FindString(serialPort1.DataBits.ToString());
                cbxParity.SelectedIndex = cbxParity.FindString(serialPort1.Parity.ToString());
                cbxStop.SelectedIndex = cbxStop.FindString(serialPort1.StopBits.ToString());
                cbxHandshake.SelectedIndex = cbxHandshake.FindString(serialPort1.Handshake.ToString());       
            }
            catch
            {
                MessageBox.Show("There is no serial port available", "Serial ports unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxPortname.SelectedIndex == -1)
                {
                    MessageBox.Show("Select desired serial port");
                    cbxPortname.Focus();
                    return;
                }
                else
                {
                    serialPort1.PortName = cbxPortname.Items[cbxPortname.SelectedIndex].ToString().Trim().ToUpper();
                    serialPort1.BaudRate = Convert.ToInt32(cbxBaud.SelectedItem.ToString());
                    serialPort1.DataBits = Convert.ToInt32(cbxData.SelectedItem.ToString());
                    switch (cbxParity.SelectedIndex)
                    {
                        case 0:
                            serialPort1.Parity = Parity.None;
                            break;
                        case 1:
                            serialPort1.Parity = Parity.Odd;
                            break;
                        case 2:
                            serialPort1.Parity = Parity.Even;
                            break;
                        case 3:
                            serialPort1.Parity = Parity.Mark;
                            break;
                        case 4:
                            serialPort1.Parity = Parity.Space;
                            break;
                    }
                    switch (cbxStop.SelectedIndex)
                    {
                        case 0:
                            serialPort1.StopBits = StopBits.None;
                            break;
                        case 1:
                            serialPort1.StopBits = StopBits.One;
                            break;
                        case 2:
                            serialPort1.StopBits = StopBits.Two;
                            break;
                        case 3:                            
                            serialPort1.StopBits = StopBits.OnePointFive;
                            break;
                    }
                    switch (cbxHandshake.SelectedIndex)
                    {
                        case 0:
                            serialPort1.Handshake = Handshake.None;
                            break;
                        case 1:
                            serialPort1.Handshake = Handshake.XOnXOff;
                            break;
                        case 2:
                            serialPort1.Handshake = Handshake.RequestToSend;
                            break;
                        case 3:
                            serialPort1.Handshake = Handshake.RequestToSendXOnXOff;
                            break;
                    }
                    //serialPort1.Open();
                    OK = true;
                    Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem when oppening the serial port.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cbxBaud_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
