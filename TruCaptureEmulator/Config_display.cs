using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TruCaptureEmulator
{
    public partial class frmDisplayConfig : Form
    {

         private Trucaptureemulator sensor;

        
        public frmDisplayConfig(Trucaptureemulator sensor)
        {
            InitializeComponent();
            this.sensor = sensor;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                sensor.New_Display(tbxIP.Text, Convert.ToInt32(tbxporta.Text), (int)nupVelocidade.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                sensor.Set_Period(dtpStart.Value, dtpEnd.Value);
                sensor.Display_mode= optVelocidade.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Dados enviados com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ckbDisplay.Enabled = true;
        }

        private void ckbDisplay_CheckedChanged(object sender, EventArgs e)
        {
            sensor.Display = ckbDisplay.Checked;
        }

        private void frmDisplayConfig_Load(object sender, EventArgs e)
        {
            if (sensor.Display_exists)
            {
                ckbDisplay.Enabled = true;
                ckbDisplay.Checked = sensor.Display;
                tbxIP.Text = sensor.IP_display1;
                tbxporta.Text = sensor.Porta.ToString();
                dtpStart.Value = sensor.Periodo.begin;
                dtpEnd.Value = sensor.Periodo.end;
                optVelocidade.Checked = sensor.Display_mode;
                optDistancia.Checked = !sensor.Display_mode;
                nupVelocidade.Value = sensor.Velocidade;
            }
        }
    }
}
