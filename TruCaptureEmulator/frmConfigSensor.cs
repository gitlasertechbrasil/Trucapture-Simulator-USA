using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TruCaptureEmulator
{
    public partial class frmConfigSensor : Form
    {
        private Trucaptureemulator sensor;

        public frmConfigSensor(Trucaptureemulator sensor)
        {
            InitializeComponent();
            this.sensor = sensor;
        }   

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if(!VerifySerialNumber(tbxSN.Text))
                {
                    throw new Exception("Check sensor serial number pattern.");
                }

                string[] temp = tbxMD.Text.Split(',');
                int min = Convert.ToInt32(temp[0]);
                int max = Convert.ToInt32(temp[1]);
                int med = Convert.ToInt32(temp[2]);
                if (min < 15 || min > 70 || med < 15 || med > 70 || max < 15 || max > 70 ||
                   (min >= max || min >= med) || (med >= max) || (min + 7 > med) || (med + 7 > max))
                {
                    MessageBox.Show("Check MD settings");
                }
                else
                {
                    Trucaptureemulator.MD md = new Trucaptureemulator.MD();
                    md.min = min;
                    md.max = max;
                    md.med = med;
                    if (nudVelmax.Value >= nudVelmin.Value && nudDistmax.Value >= nudDistmin.Value)
                    {
                        sensor.Set_Parameters(ckbBanner.Checked, ckbTime.Checked, ckbError.Checked, (Trucaptureemulator.MM)cbxMM.SelectedIndex,
                                             (Trucaptureemulator.DM)cbxDM.SelectedIndex, (int)nudVelmin.Value, (int)nudVelmax.Value, (int)nudDistmin.Value,
                                             (int)nudDistmax.Value, tbxSN.Text, (int)nupHT.Value, md,ckbFixaDecimal.Checked, (int)nudDecimalFixo.Value, (int)nupSpeedCapture.Value);

                        sensor.Setor4 = (Trucaptureemulator.Sector4Type) cmbSector4Type.SelectedIndex;
                        sensor.DGMode = (Trucaptureemulator.DG_mode)Enum.Parse(typeof(Trucaptureemulator.DG_mode), cmbDG.Text);

                        int minimomultiplas = Convert.ToInt32(nupMinMultLeituras.Value);
                        int maximomultiplas = Convert.ToInt32(nupMaxMultLeituras.Value);
                        if(minimomultiplas <= maximomultiplas)
                        {
                            sensor.Minstrings = Convert.ToInt32(nupMinMultLeituras.Value);
                            sensor.Maxstrings = Convert.ToInt32(nupMaxMultLeituras.Value);
                        }
                        else
                        {
                            throw new Exception("The minimum value of number of string should be smaller or equals than the maximum value.");
                        }
                        sensor.Distinfms = Convert.ToInt32(nupInfMultiLeituras.Value);
                        sensor.Distsupms = Convert.ToInt32(nupSupMultLeituras.Value);
                        int tempostringmin = Convert.ToInt32(nupTempoMinMultLeituras.Value);
                        int tempostringmax = Convert.ToInt32(nupTempoMaxMultLeituras.Value);
                        if(tempostringmin<=tempostringmax)
                        {
                        sensor.Tempostringsmin = tempostringmin;
                        sensor.Tempostringsmax = tempostringmax;
                        }
                        else
                        {
                            throw new Exception("Minimum interval between strings should be smaller or equals than the maximum value.");
                        }

                        sensor.StringDeInterferencia = tbxInterferenciaManual.Text;
                        sensor.ModoInterferencia = (Trucaptureemulator.InterferenceMode)Enum.Parse(typeof(Trucaptureemulator.InterferenceMode), cmbInterferencia.Text);

                        sensor.ID1 = cmbIDs.Text;

                        MessageBox.Show("Success saving parameters");
                    }
                    else
                    {
                        MessageBox.Show("Check speed and distance settings", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Attention",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
           
        }

        private bool VerifySerialNumber(string serial)
        {
            Regex regSerial = new Regex("^[A-Z]{2}[0-9]{6}$");
            serial = serial.ToUpper();
            if(regSerial.IsMatch(serial))
            {
                return true;
            }
                return false;
        }

        private void frmConfigSensor_Load(object sender, EventArgs e)
        {
            ckbBanner.Checked = sensor.Get_DB();
            ckbTime.Checked = sensor.Get_DT();
            ckbError.Checked = sensor.Get_DE();
            nudVelmax.Value = sensor.Get_Velmax();
            nudVelmin.Value = sensor.Get_Velmin();
            nudDistmax.Value = sensor.Get_Distmax();
            nudDistmin.Value = sensor.Get_Distmin();
            ckbFixaDecimal.Checked = sensor.Fixardecimal;
            nudDecimalFixo.Value = sensor.DecimalValue; 
            cbxMM.SelectedIndex = (int)sensor.Get_MM();
            cbxDM.SelectedIndex = (int)sensor.Get_DM();
            tbxSN.Text = sensor.SN1;
            nupHT.Value = sensor.Get_HT(); 
            Trucaptureemulator.MD md = sensor.Get_MD();
            tbxMD.Text = md.min + "," + md.max + "," + md.med;
            nupMinMultLeituras.Value = sensor.Minstrings;
            nupMaxMultLeituras.Value = sensor.Maxstrings;
            nupInfMultiLeituras.Value = sensor.Distinfms;
            nupSupMultLeituras.Value = sensor.Distsupms;
            nupTempoMinMultLeituras.Value = sensor.Tempostringsmin;
            nupTempoMaxMultLeituras.Value = sensor.Tempostringsmax;
            nupSpeedCapture.Value = sensor.VelocidadeCaptura;

            string[] DGstrings = Enum.GetNames(typeof(Trucaptureemulator.DG_mode));

            foreach(string s in DGstrings)
            {
                cmbDG.Items.Add(s);
            }
            cmbDG.Text = sensor.DGMode.ToString();

            string[] Sector4Types = Enum.GetNames(typeof(Trucaptureemulator.Sector4Type));

            foreach (string s in Sector4Types)
            {
                cmbSector4Type.Items.Add(s);
            }

            cmbSector4Type.Text = sensor.Setor4.ToString();

            string[] tiposInterferencia = Enum.GetNames(typeof(Trucaptureemulator.InterferenceMode));
            foreach (string s in tiposInterferencia)
            {
                cmbInterferencia.Items.Add(s);
            }
            cmbInterferencia.Text = sensor.ModoInterferencia.ToString();
            tbxInterferenciaManual.Text = sensor.StringDeInterferencia;

            cmbIDs.Items.AddRange(sensor.IDList);
            cmbIDs.Text = sensor.ID1;
        }

        private void btnCRC_Click(object sender, EventArgs e)
        {
            string calc = tbxInterferenciaManual.Text.Replace("$", string.Empty);
            calc = "$" + calc + "*" + sensor.Conc_checksum(calc);
            tbxInterferenciaManual.Text = calc;
        }
    }
}
