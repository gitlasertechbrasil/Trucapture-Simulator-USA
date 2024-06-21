using br.com.ltb.Camera.Pumatronix;
using br.com.ltb.GUI;
using br.com.ltb.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TruCaptureEmulator
{
    public partial class frmTrucapture : Form
    {
        private Trucaptureemulator sensor;
        private Thread _thdRead;
        private int _triggerRead = 1;
        SerialPort sptSerial;
        private HTTPLog _frmEnviados;
        private HTTPLog _frmRecebidos;
        private Tag _xml;

        private List<string> sendedMessages = new List<string>();
        private int _currentSended = 0;
        //private configserial c;  

        public frmTrucapture()
        {
            InitializeComponent();

            if (!Directory.Exists(Application.StartupPath + "\\Parameters"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Parameters");
            }

            sptSerial = new SerialPort();
            sptSerial.PortName = "COM1";

            sfdPerfil.InitialDirectory = Application.StartupPath + "\\Parameters";
            ofdPerfil.InitialDirectory = Application.StartupPath + "\\Parameters";
            sptSerial.ReadTimeout = 3000;
            sptSerial.BaudRate = 115200;

            _thdRead = new Thread(new ThreadStart(lendo));
            _thdRead.Start();

           

           
            string[] ErrorCode = Enum.GetNames(typeof(Trucaptureemulator.ERROR));

            cbcISError.Items.Add("Random");
            
            foreach(string s in ErrorCode)
            {
                cbcISError.Items.Add(Convert.ToInt32(Enum.Parse(typeof(Trucaptureemulator.ERROR), s)) + " - " + s);
            }

            string[] directions = Enum.GetNames(typeof(Trucaptureemulator.MultiStringsDirection));

            foreach (string s in directions)
            {
                slfDirecao.Items.Add(s.ToUpper());
            }
        }

       

        private void FrmCamera_FormClosing(object sender, FormClosingEventArgs e)
        {          

        }

        private ServiceController GetServiceByName(string name)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();

                for (int i = 0; i < services.Length; i++)
                {
                    if (services[i].ServiceName.IndexOf(name) > -1)
                    {
                        return services[i];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


        private void lendo()
        {

            try
            {
                sptSerial.DiscardInBuffer();
                sptSerial.DiscardOutBuffer();
            }
            catch (Exception)
            {
            }

            while (Program.programarodando)
            {
                try
                {
                    if (sptSerial != null && sptSerial.IsOpen && sptSerial.BytesToRead >= _triggerRead)
                    {
                        string lido = sptSerial.ReadLine();

                        lido = lido.Replace("\r", string.Empty);

                        if (sensor.Board && lido.StartsWith(">PH"))
                        {
                            byte[] _ph = Encoding.ASCII.GetBytes(lido);
                            byte[] _horario = new byte[_ph.Length - 4];
                            Array.Copy(_ph, 4, _horario, 0, _horario.Length);

                            string horaprogramada = ">PH,";
                            for (int x = 0; x < _horario.Length; x++)
                            {
                                horaprogramada = horaprogramada + _horario[x].ToString("X2");
                            }

                            string header = string.Empty;

                            if (IsHandleCreated)
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    if (ckbData.Checked)
                                    {
                                        header = "[ " + DateTime.Now.ToString("HH:mm:ss-fff") + " ]   ";
                                    }
                                });
                            }

                            AppendText(MessageType.Recebida, header, horaprogramada, false);
                            sensor.PH(_ph);
                        }
                        else
                        {
                            received_data(lido);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                Thread.Sleep(1);
            }
        }

        private string CutTrash(string lido)
        {
            if (!lido.StartsWith("$") && !lido.StartsWith(">"))
            {
                if (lido.Contains("$"))
                {
                    int dolarposition = lido.IndexOf('$');
                    if (dolarposition > 0)
                    {
                        return lido.Substring(dolarposition, lido.Length - dolarposition);
                    }
                }
            }
            return lido;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            /*
            string lido = serialPort1.ReadLine();
            lido = lido.Replace("\r", string.Empty);
            if (sensor.Board && lido.StartsWith(">PH"))
            {
                byte[] _ph = Encoding.ASCII.GetBytes(lido);
                byte[] _horario = new byte[_ph.Length - 4];
                Array.Copy(_ph, 4, _horario, 0, _horario.Length);

                string horaprogramada = ">PH,";
                for (int x = 0; x < _horario.Length; x++)
                {
                    horaprogramada = horaprogramada + _horario[x].ToString("X2");
                }
                Invoke((MethodInvoker)delegate
                {
                    if (tbxrecebidos.Text.Length > 32000)
                    {
                        tbxrecebidos.Text = string.Empty;
                    }
                    string header = string.Empty;
                    if (ckbData.Checked)
                    {
                        header = "[ " + DateTime.Now.ToString("HH:mm:ss-fff") + " ]   ";
                    }
                        tbxrecebidos.AppendText(header + horaprogramada + Environment.NewLine);
                });
                sensor.PH(_ph);
            }
            else
            {
                received_data(lido);
            }
            */
        }


        private void received_data(string lido)
        {
            string resposta;
            lido = lido.ToUpper();
            string header = string.Empty;

            if (ckbData.Checked)
            {
                header = "[ " + DateTime.Now.ToString("HH:mm:ss-fff") + " ]   ";
            }

            if (lido.Contains('\0'))
            {
                lido = lido.Replace("\0", string.Empty);
                AppendText(MessageType.Recebida, header, "Received Null characters", false);

            }

            AppendText(MessageType.Recebida, header, lido, false);
            lido = CutTrash(lido);

            if (lido.StartsWith("$ST"))
            {
                ckbSimulaErros.Checked = false;
            }

            resposta = sensor.answer(lido);




            //if (!lido.StartsWith("$GO") && !lido.StartsWith("$SU"))
            if (!lido.StartsWith("$SU"))
            {
                if (!string.IsNullOrEmpty(resposta))
                {
                    sptSerial.Write(resposta + "\r\n");
                    sptSerial.BaseStream.Flush();
                }

                if (!string.IsNullOrEmpty(resposta))
                {
                    header = string.Empty;
                    if (ckbData.Checked)
                    {
                        header = "[ " + DateTime.Now.ToString("HH:mm:ss-fff") + " ]   ";
                    }
                    AppendText(MessageType.Enviada, header, resposta, false);
                }
            }
        }

        private void AppendText(MessageType tipo, string header, string text, Color color, bool trigger)
        {
            int inicio = rttTeste.TextLength;
            string stipo = tipo.ToString() + ": ";

            if (rttTeste.Text.Length > 0)
            {
                rttTeste.AppendText(Environment.NewLine);
            }
            rttTeste.AppendText(header + stipo + text);


            rttTeste.SelectionStart = inicio;
            rttTeste.SelectionLength = header.Length + stipo.Length + text.Length + 1;

            if (!trigger)
            {
                rttTeste.SelectionColor = color;
                rttTeste.SelectionFont = new Font("Verdana", 12, FontStyle.Regular);
            }
            else
            {
                rttTeste.SelectionColor = Color.Red;
                rttTeste.SelectionFont = new Font("Verdana", 12, FontStyle.Bold);
            }
            rttTeste.ScrollToCaret();
            rttTeste.SelectionStart = rttTeste.Text.Length;
        }

        private void AppendText(MessageType tipo, string header, string text, bool trigger)
        {

            Color c = Color.Blue;


            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    BeginInvoke((MethodInvoker)delegate
                    {

                        if (tipo == MessageType.Recebida)
                        {
                            _frmRecebidos?.SetText(header + text + Environment.NewLine);
                        }
                        else
                        {
                            c = Color.Black;
                            _frmEnviados?.SetText(header + text + Environment.NewLine);
                        }


                        if (rttTeste.Text.Length > 102400)
                        {
                            string error = string.Empty;

                            try
                            {
                                if (tgbLogAutomatico.Checked)
                                {
                                    if (!Directory.Exists(Environment.CurrentDirectory + @"\AutoLog"))
                                    {
                                        Directory.CreateDirectory(Environment.CurrentDirectory + @"\Autolog");
                                    }

                                    string _file = rttTeste.Text;

                                    if (File.Exists(Environment.CurrentDirectory + @"\AutoLog\" + DateTime.Now.ToString("yyyMMdd-HH") + "-trucapturelog.txt"))
                                    {
                                        _file = Environment.NewLine + Environment.NewLine + _file;
                                    }

                                    StreamWriter sw = File.AppendText(Environment.CurrentDirectory + @"\AutoLog\" + DateTime.Now.ToString("yyyMMdd-HH") + "-trucapturelog.txt");
                                    sw.Write(_file);
                                    sw.Flush();
                                    sw.Close();
                                    sw.Dispose();

                                }
                            }
                            catch (Exception ex)
                            {
                                error = ex.Message;
                            }

                            rttTeste.Text = string.Empty;

                            if (!string.IsNullOrEmpty(error))
                            {
                                rttTeste.Text = error;
                            }
                        }

                        AppendText(tipo, header, text, c, trigger);
                    });
                }
            }
            else
            {

                if (tipo == MessageType.Recebida)
                {
                    _frmRecebidos?.SetText(header + text + Environment.NewLine);
                }
                else
                {
                    c = Color.Black;
                    _frmEnviados?.SetText(header + text + Environment.NewLine);
                }

                if (rttTeste.Text.Length > 102400)
                {
                    string error = string.Empty;
                    try
                    {
                        if (tgbLogAutomatico.Checked)
                        {
                            if (!Directory.Exists(Environment.CurrentDirectory + @"\AutoLog"))
                            {
                                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Autolog");
                            }

                            string _file = rttTeste.Text.Trim();

                            if (File.Exists(Environment.CurrentDirectory + @"\AutoLog\" + DateTime.Now.ToString("yyyMMdd-HH") + "-trucapturelog.txt"))
                            {
                                _file = Environment.NewLine + Environment.NewLine + _file;
                            }

                            StreamWriter sw = File.AppendText(Environment.CurrentDirectory + @"\AutoLog\" + DateTime.Now.ToString("yyyMMdd-HH") + "-trucapturelog.txt");
                            sw.Write(_file);
                            sw.Flush();
                            sw.Close();
                            sw.Dispose();

                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        rttTeste.Text = error;
                    }
                }

                AppendText(tipo, header, text, c, trigger);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Trucapture Simulator - Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            sensor = new Trucaptureemulator(sptSerial);
            //pctSpeed.Image = GeneratePlate.GetDisabledImage(GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed));
            bool _saveXML = false;

            XmlManipulator _xmlManipulator = null;
            XmlReader _xmlReader = new XmlReader();

            Program.programarodando = true;


            string[] portnames = SerialPort.GetPortNames();
            Array.Sort(portnames);

            if (portnames.Length > 0)
            {
                if (!File.Exists(Application.StartupPath + "//Config//Config.xml"))
                {
                    SaveXML();
                }

                try
                {
                    _xml = _xmlReader.MtdReadXml(Application.StartupPath + "/Config/Config.xml");
                    _xmlReader.MtdClose();
                    _xmlManipulator = new XmlManipulator(_xml);

                    if (_xmlManipulator.MtdTagExists("PORT", "TRUCAPTURESIMULATOR"))
                    {
                        try
                        {
                            string _Portname = _xmlManipulator.MtdGetContentField("PORT", "TRUCAPTURESIMULATOR");

                            if (portnames.Contains(_Portname))
                            {
                                sptSerial.PortName = _Portname;
                            }
                            else
                            {
                                sptSerial.PortName = portnames[0];
                                _saveXML = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            sptSerial.PortName = portnames[0];
                            _saveXML = true;
                        }
                    }
                    else
                    {
                        sptSerial.PortName = portnames[0];
                        _saveXML = true;
                    }

                    if (_xmlManipulator.MtdTagExists("BAUDRATE", "TRUCAPTURESIMULATOR"))
                    {
                        try
                        {
                            sptSerial.BaudRate = Convert.ToInt32(_xmlManipulator.MtdGetContentField("BAUDRATE", "TRUCAPTURESIMULATOR"));
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception)
                {
                    sptSerial.PortName = portnames[0];
                    _saveXML = true;
                }


                //lblComport.Text = "PORT: " + sptSerial.PortName + "\r\nbaudrate: " + sptSerial.BaudRate;
                enable_all();

                if (_xmlManipulator.MtdTagExists("CABECALHO", "TRUCAPTURESIMULATOR"))
                {
                    try
                    {
                        ckbData.Checked = Convert.ToBoolean(_xmlManipulator.MtdGetContentField("CABECALHO", "TRUCAPTURESIMULATOR"));
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (_xmlManipulator.MtdTagExists("LOGAUTOMATICO", "TRUCAPTURESIMULATOR"))
                {
                    try
                    {
                        tgbLogAutomatico.Checked = Convert.ToBoolean(_xmlManipulator.MtdGetContentField("LOGAUTOMATICO", "TRUCAPTURESIMULATOR"));
                    }
                    catch (Exception ex)
                    {
                    }
                }

                try
                {
                    if (_saveXML)
                    {
                        _xmlManipulator.MtdChangeTagValue("PORT", sptSerial.PortName, "TRUCAPTURESIMULATOR");
                        _xmlManipulator.MtdChangeTagValue("BAUDRATE", sptSerial.BaudRate.ToString(), "TRUCAPTURESIMULATOR");
                        _xml = _xmlManipulator.PpdXml;
                        XmlWriter _writer = new XmlWriter(_xml, XmlWriter.WriteType.UTF8);
                        _writer.MtdSaveXmlFile(Application.StartupPath + "//Config//Config.xml");
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                MessageBox.Show("Não foi detectada nenhuma porta serial", "Serial não encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cbcISError.Text = "62 - APD_FAILED";
        }

        private void SaveXML()
        {
            try
            {
                string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
               Environment.NewLine +
               "<TRUCAPTURESIMULATOR>" +
               Environment.NewLine +
               "<PORT></PORT>" +
               Environment.NewLine +
               "<BAUDRATE></BAUDRATE>" +
               Environment.NewLine +
               "<CABECALHO>FALSE</CABECALHO>" +
               Environment.NewLine +
               "<LOGAUTOMATICO>FALSE</LOGAUTOMATICO>" +
               Environment.NewLine +
               "</TRUCAPTURESIMULATOR>";

                if (!Directory.Exists(Application.StartupPath + "/Config"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "/Config");
                }
                File.WriteAllText(Application.StartupPath + "/Config/Config.xml", xml);
            }
            catch (Exception ex)
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.programarodando = false;
            try
            {
                XmlReader _xmlReader = new XmlReader();
                _xml = _xmlReader.MtdReadXml(Application.StartupPath + "/Config/Config.xml");
                _xmlReader.MtdClose();
            }
            catch
            {
                SaveXML();
            }

            try
            {
                XmlManipulator _xmlManipulator = new XmlManipulator(_xml);
                XmlCreator _xmlCreator = new XmlCreator(_xml);

                if (!_xmlManipulator.MtdTagExists("CABECALHO", "TRUCAPTURESIMULATOR"))
                {
                    _xmlCreator.MtdAddTagContent("CABECALHO", ckbData.Checked.ToString(), "TRUCAPTURESIMULATOR");
                }

                if (!_xmlManipulator.MtdTagExists("LOGAUTOMATICO", "TRUCAPTURESIMULATOR"))
                {
                    TagContent t = new TagContent("LOGAUTOMATICO", "FALSE");
                    _xmlCreator.MtdAddTagContent("LOGAUTOMATICO", tgbLogAutomatico.Checked.ToString(), "TRUCAPTURESIMULATOR");
                }

                _xmlManipulator = new XmlManipulator(_xmlCreator.PpdXml);

                _xmlManipulator.MtdChangeTagValue("CABECALHO", ckbData.Checked.ToString(), "TRUCAPTURESIMULATOR");
                _xmlManipulator.MtdChangeTagValue("LOGAUTOMATICO", tgbLogAutomatico.Checked.ToString(), "TRUCAPTURESIMULATOR");
                _xml = _xmlManipulator.PpdXml;
                XmlWriter _writer = new XmlWriter(_xml, XmlWriter.WriteType.UTF8);
                _writer.MtdSaveXmlFile(Application.StartupPath + "//Config//Config.xml");
            }
            catch (Exception ex)
            {
            }


            
        }

        
        private void escreveserial(object sender, string e, bool trigger)
        {
            try
            {
                string header = string.Empty;
                if (IsHandleCreated)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        if (ckbData.Checked)
                        {
                            header = "[ " + DateTime.Now.ToString("HH:mm:ss-fff") + " ]   ";
                        }
                    });
                }
                AppendText(MessageType.Enviada, header, e, trigger);
            }
            catch (Exception)
            {
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            sensor.Time = Convert.ToInt32(numericUpDown1.Value);
        }

        private void tbxSend_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxSend_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void portaSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sptSerial.IsOpen)
                {
                    sptSerial.Close();
                }

                if (usbOnOff.Pressed)
                {
                    usbOnOff.Pressed = false;
                    sensor.Turnoff();
                }
                //else
                //{
                //    string[] portas = SerialPort.GetPortNames();
                //    sptSerial.PortName = portas[0];
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foram encontradas portas seriais disponíveis", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            try
            {
                frmConfig_serial config = new frmConfig_serial(sptSerial);
                config.ShowDialog();
                if (config.OK1)
                {
                    enable_all();

                    if (!File.Exists(Application.StartupPath + "/Config/Config.xml"))
                    {
                        SaveXML();
                    }

                    try
                    {
                        XmlManipulator _xmlManipulator = new XmlManipulator(_xml);

                        if (_xmlManipulator.MtdTagExists("PORT", "TRUCAPTURESIMULATOR"))
                        {
                            //_xmlManipulator.MtdChangeTagValue("")
                            _xmlManipulator.MtdChangeTagValue("PORT", sptSerial.PortName, "TRUCAPTURESIMULATOR");
                        }
                        if (_xmlManipulator.MtdTagExists("BAUDRATE", "TRUCAPTURESIMULATOR"))
                        {
                            _xmlManipulator.MtdChangeTagValue("BAUDRATE", sptSerial.BaudRate.ToString(), "TRUCAPTURESIMULATOR");
                        }

                        _xml = _xmlManipulator.PpdXml;
                        XmlWriter _writer = new XmlWriter(_xml, XmlWriter.WriteType.UTF8);
                        _writer.MtdSaveXmlFile(Application.StartupPath + "//Config//Config.xml");
                    }
                    catch (Exception ex)
                    {
                    }
                }
                sensor.Writeserial -= new Trucaptureemulator.CapturaEventHandler(escreveserial);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foram encontradas portas seriais disponíveis", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void enable_all()
        {
            //pbxTrucapture.Enabled = true;
            usbOnOff.Pressed = false;
            usbOnOff.Enabled = true;
            numericUpDown1.Enabled = true;
            label1.Enabled = true;
            label4.Enabled = true;
            tbxSend.Enabled = true;
            ckbInibe.Enabled = true;
            ckbIntervalVariavel.Enabled = true;
            //ckbBoard.Enabled = true;
            ckbMdlimit.Enabled = true;
            ckbSimulaErros.Enabled = true;
            ckbBootError.Enabled = true;
            pnlFunctions.Enabled = true;

           

            //if (ckbMulti.Checked)
            //{
            slfDirecao.Enabled = true;
            //}
            sensor.Turnoff();
            lblComport.Text = "PORT: " + sptSerial.PortName + Environment.NewLine + "baudrate: " + sptSerial.BaudRate;           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void sensorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigSensor config = new frmConfigSensor(sensor);
            config.FormClosing += Config_FormClosing;
            config.ShowDialog();
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (sensor.isON)
            //{
            //pctSpeed.Image = GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed);
            //}
            //else
            //{
            //    pctSpeed.Image = GeneratePlate.GetDisabledImage(GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed));
            //}
        }

        private void salvarParametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdPerfil.ShowDialog() == DialogResult.OK)
            {
                sfdPerfil.InitialDirectory = Path.GetDirectoryName(sfdPerfil.FileName);
                sensor.save_file(sfdPerfil.FileName);
                MessageBox.Show("Parametros salvos com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (!sensor.Ruido)
            {
                sensor.Ruido = true;
            }
            else
            {
                sensor.Ruido = false;
            }
        }

        private void carregarParametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdPerfil.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ofdPerfil.InitialDirectory = Path.GetDirectoryName(ofdPerfil.FileName);
                    sensor.load_file(ofdPerfil.FileName);
                    numericUpDown1.Value = sensor.Time;
                    ckbInibe.Checked = sensor.Inibe;
                    ckbMdlimit.Checked = sensor.Mdlimit;
                    ckbMulti.Checked = sensor.Multistrings;

                    slfDirecao.SelectedItem = sensor.Direction.ToString().ToUpper();

                    //ckbBoard.Checked = sensor.Board;
                    ckbBootError.Checked = sensor.PDError;
                    ckbSimulaErros.Checked = sensor.SimulaErros;
                    ckbIntervalVariavel.Checked = sensor.VariableTime;

                    tgbSimulador.Checked = sensor.ModoSimulador;

                    tgbISError.Checked = sensor.ISError;

                    if(sensor.ISRandom)
                    {
                        cbcISError.SelectedIndex = 0;
                    }
                    else
                    {
                        cbcISError.Text = Convert.ToInt32(sensor.ISERRORMessage) + " - " + sensor.ISERRORMessage.ToString();
                    }


                    //if (sensor.isON)
                    //{
                    //pctSpeed.Image = GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed);
                    //}
                    //else
                    //{
                    //    pctSpeed.Image = GeneratePlate.GetDisabledImage(GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed));
                    //}

                  
                    string[] directions = Enum.GetNames(typeof(Trucaptureemulator.MultiStringsDirection));

                    foreach (string s in directions)
                    {
                        slfDirecao.Items.Add(s.ToUpper());
                    }

                    MessageBox.Show("Parameters were loaded successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ckbInibe_CheckedChanged(object sender, EventArgs e)
        {
            sensor.Inibe = ckbInibe.Checked;
        }

        private void ckbMdlimit_CheckedChanged(object sender, EventArgs e)
        {
            sensor.Mdlimit = ckbMdlimit.Checked;

            if (sensor.Mdlimit && !sensor.Board)
            {
                ckbMulti.Enabled = true;
            }
            else
            {
                ckbMulti.Checked = false;
                ckbMulti.Enabled = false;
            }
        }

        private void ckbDisplay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void displayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDisplayConfig f = new frmDisplayConfig(sensor);
            f.Show();
        }

        private void ckbMulti_CheckedChanged(object sender, EventArgs e)
        {
            sensor.Multistrings = ckbMulti.Checked;

            if (ckbMulti.Checked)
            {
                tgbSimulador.Checked = false;
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            rttTeste.Text = string.Empty;
        }
       

        private void ckbIntervalVariavel_CheckedChanged(object sender, EventArgs e)
        {
            sensor.VariableTime = ckbIntervalVariavel.Checked;
            numericUpDown1.Enabled = !sensor.VariableTime;
        }

        private void ckbSimulaErros_CheckedChanged(object sender, EventArgs e)
        {
            sensor.SimulaErros = ckbSimulaErros.Checked;
        }

        private void ckbBootError_CheckedChanged(object sender, EventArgs e)
        {
            sensor.PDError = ckbBootError.Checked;
        }

       
        private Point CenterParentForm(Form frm)
        {
            return new Point(Location.X + (Width - frm.Width) / 2, Location.Y + (Height - frm.Height) / 2);
        }

        private void ubtStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (sptSerial.IsOpen && sensor.isON)
            {
                received_data("$GO,0");
            }
        }

        private void ubtStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (sptSerial.IsOpen && sensor.isON)
            {
                received_data("$ST");
            }
        }

        private void usbUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (sensor.isON && sensor.VelocidadeCaptura < 320)
            {
                sensor.VelocidadeCaptura++;
                //pctSpeed.Image = GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed);
            }
            //else if (!sensor.isON)
            //{
            //    pctSpeed.Image = GeneratePlate.GetDisabledImage(GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed));
            //}
        }

        private void usbDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (sensor.isON && sensor.VelocidadeCaptura > 0)
            {
                sensor.VelocidadeCaptura--;
                //pctSpeed.Image = GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed);
            }
            //else if (!sensor.isON)
            //{
            //    pctSpeed.Image = GeneratePlate.GetDisabledImage(GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed));
            //}
        }

        private void usbOnOff_MouseUp(object sender, MouseEventArgs e)
        {
            if (usbOnOff.Pressed)
            {
                try
                {
                    if (!sptSerial.IsOpen)
                    {
                        sptSerial.Open();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Falha ao tentar abrir porta Serial", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usbOnOff.Pressed = false;
                    return;
                }

                try
                {
                    sensor.Writeserial += new Trucaptureemulator.CapturaEventHandler(escreveserial);
                    sensor.CaptureChangedHandler += Sensor_CaptureChangedHandler;
                    //pbxTrucapture.Image = Properties.Resources.S200;
                    pnlFunctions.Enabled = true;
                    pnlDialogs.Enabled = true;
                    ubtStart.Enabled = true;
                    ubtStop.Enabled = true;
                    usbLimpar.Enabled = true;

                    sensor.Turnon();                 
                }
                catch { }

                //pctSpeed.Image = GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed);
                //spcSpeed.DisconnectedImage = GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed);
                //spcSpeed.Enabled = true;
            }
            else
            {
                sensor.Writeserial -= new Trucaptureemulator.CapturaEventHandler(escreveserial);
                sensor.CaptureChangedHandler -= Sensor_CaptureChangedHandler;
                //pnlFunctions.Enabled = false;
                pnlDialogs.Enabled = false;
                ubtStart.Enabled = false;
                ubtStop.Enabled = false;
                usbLimpar.Enabled = false;
                sensor.Turnoff();
                //pbxTrucapture.Image = Properties.Resources.S200_disable;
                rttTeste.Text = string.Empty;
                //pctSpeed.Image = GeneratePlate.GetDisabledImage(GeneratePlate.GetSpeedTrafficPlateImage(sensor.VelocidadeCaptura, GeneratePlate.TypeOfRoadSigns.Speed));
                //usbUp.Enabled = false;
                //usbDown.Enabled = false;
                _frmEnviados?.Limpar();
                _frmRecebidos?.Limpar();

                try
                {
                    sensor.Writeserial -= new Trucaptureemulator.CapturaEventHandler(escreveserial);
                    sptSerial.Close();
                }
                catch
                {
                }
            }
        }

        private void Sensor_CaptureChangedHandler(int capturespeed)
        {
           
        }

        private void lblEnviados_Click(object sender, EventArgs e)
        {
            if (lblEnviados.BackColor == Color.MidnightBlue)
            {
                lblEnviados.BackColor = Color.DeepSkyBlue;
                _frmEnviados = new HTTPLog();
                _frmEnviados.Width = Width / 2;
                _frmEnviados.FormClosing += _frmEnviados_FormClosing;


                if (Top < Screen.FromControl(this).WorkingArea.Height)
                {
                    if (Screen.FromControl(this).WorkingArea.Height > Top + Height + _frmEnviados.Height)
                    {
                        _frmEnviados.Top = Top + Height;
                        _frmEnviados.Left = Left;
                    }
                    else if (Top > _frmEnviados.Height)
                    {
                        _frmEnviados.Top = Top - _frmEnviados.Height;
                        _frmEnviados.Left = Left;
                    }
                    else
                    {
                        if (_frmRecebidos != null)
                        {
                            _frmEnviados.Top = _frmRecebidos.Top + 20;
                            _frmEnviados.Left = _frmRecebidos.Left + 20;
                        }
                    }
                }
                else
                {
                    if (Screen.FromControl(this).WorkingArea.Height > Top - Screen.FromControl(this).WorkingArea.Height + Height + _frmEnviados.Height)
                    {
                        _frmEnviados.Top = Top + Height;
                        _frmEnviados.Left = Left;
                    }
                    else if (Top - Screen.FromControl(this).WorkingArea.Height > _frmEnviados.Height)
                    {
                        _frmEnviados.Top = Top - _frmEnviados.Height;
                        _frmEnviados.Left = Left;
                    }
                    else
                    {
                        if (_frmRecebidos != null)
                        {
                            _frmEnviados.Top = _frmRecebidos.Top + 20;
                            _frmEnviados.Left = _frmRecebidos.Left + 20;
                        }
                    }
                }
                _frmEnviados.Text = "SENT";
                _frmEnviados.Show();
            }
            else
            {
                lblEnviados.BackColor = Color.MidnightBlue;
                _frmEnviados.Close();
            }
        }

        private void _frmEnviados_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblEnviados.BackColor = Color.MidnightBlue;
        }

        private void lblRecebidos_Click(object sender, EventArgs e)
        {
            if (lblRecebidos.BackColor == Color.MidnightBlue)
            {
                lblRecebidos.BackColor = Color.DeepSkyBlue;
                _frmRecebidos = new HTTPLog();
                _frmRecebidos.Width = Width / 2;
                _frmRecebidos.FormClosing += _frmRecebidos_FormClosing;

                if (Top < Screen.FromControl(this).WorkingArea.Height)
                {
                    if (Screen.FromControl(this).WorkingArea.Height > Top + Height + _frmRecebidos.Height)
                    {
                        _frmRecebidos.Top = Top + Height;
                        _frmRecebidos.Left = Left + _frmRecebidos.Width;
                    }
                    else if (Top > _frmRecebidos.Height)
                    {
                        _frmRecebidos.Top = Top - _frmRecebidos.Height;
                        _frmRecebidos.Left = Left + _frmRecebidos.Width;
                    }
                    else
                    {
                        if (_frmEnviados != null)
                        {
                            _frmRecebidos.Top = _frmEnviados.Top + 20;
                            _frmRecebidos.Left = _frmEnviados.Left + 20;
                        }
                    }
                }
                else
                {
                    if (Screen.FromControl(this).WorkingArea.Height > Top - Screen.FromControl(this).WorkingArea.Height + Height + _frmRecebidos.Height)
                    {
                        _frmRecebidos.Top = Top + Height;
                        _frmRecebidos.Left = Left + _frmRecebidos.Width;
                    }
                    else if (Top - Screen.FromControl(this).WorkingArea.Height > _frmRecebidos.Height)
                    {
                        _frmRecebidos.Top = Top - _frmRecebidos.Height;
                        _frmRecebidos.Left = Left + _frmRecebidos.Width;
                    }
                    else
                    {
                        if (_frmEnviados != null)
                        {
                            _frmRecebidos.Top = _frmEnviados.Top + 20;
                            _frmRecebidos.Left = _frmEnviados.Left + 20;
                        }
                    }
                }

                _frmRecebidos.Text = "RECEIVED";
                _frmRecebidos.Show();
            }
            else
            {
                lblRecebidos.BackColor = Color.MidnightBlue;
                _frmRecebidos.Close();
            }
        }

        private void _frmRecebidos_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblRecebidos.BackColor = Color.MidnightBlue;
        }

        private enum MessageType
        {
            Enviada,
            Recebida
        }

        private void slfDirecao_SelectedIndexChanged(object sender, SelectionFieldEventArgs args)
        {
            sensor.Direction = (Trucaptureemulator.MultiStringsDirection)Enum.Parse(typeof(Trucaptureemulator.MultiStringsDirection), slfDirecao.SelectedItem, true);
        }

      
        private void rttTeste_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ctxSalvarLog_Opening(object sender, CancelEventArgs e)
        {

        }

        private void salvarLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string error = string.Empty;

            if (!Directory.Exists(Environment.CurrentDirectory + @"\AutoLog"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Autolog");
            }

            string _file = rttTeste.Text;
            sfdLog.InitialDirectory = Environment.CurrentDirectory + @"\Autolog";
            sfdLog.FileName = Environment.CurrentDirectory + @"\AutoLog\" + DateTime.Now.ToString("yyyMMdd-HH") + "-trucapturelog.txt";

            if (sfdLog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = File.AppendText(sfdLog.FileName);
                    sw.Write(_file);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }

                if (!string.IsNullOrEmpty(error))
                {
                    rttTeste.Text = error;
                }
            }
        }

        private void tgbSimulador_CheckedChanged(object sender, EventArgs args)
        {
            try
            {
                sensor.ModoSimulador = tgbSimulador.Checked;
                slfDirecao.Enabled = !tgbSimulador.Checked;
                if (tgbSimulador.Checked)
                {
                    ckbMulti.Checked = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void tgbInterferencia_CheckedChanged(object sender, EventArgs args)
        {
            sensor.Interferencia = tgbInterferencia.Checked;
        }

        private void tbxSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && sptSerial.IsOpen && sensor.isON)
            {
                received_data(tbxSend.Text);
                if (!sendedMessages.Contains(tbxSend.Text))
                {
                    sendedMessages.Add(tbxSend.Text);
                    _currentSended = sendedMessages.Count - 1;
                }
                tbxSend.Text = String.Empty;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (sendedMessages.Count > 0)
                {

                    tbxSend.Text = sendedMessages[_currentSended].Replace("\r", string.Empty).Replace("\n", string.Empty);

                    if (_currentSended > 0)
                    {
                        _currentSended--;
                    }

                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (sendedMessages.Count > 0)
                {

                    tbxSend.Text = sendedMessages[_currentSended].Replace("\r", string.Empty).Replace("\n", string.Empty);
                }

                if (_currentSended < sendedMessages.Count - 1)
                {
                    _currentSended++;
                }

            }
        }

        private void usbDesinibe_MouseUp(object sender, MouseEventArgs e)
        {
            sensor.DesinibeTimes = (int)nupTempInibe.Value;
        }

        private void toggleButton1_CheckedChanged(object sender, EventArgs args)
        {
            sensor.ISError = tgbISError.Checked;
            cbcISError.Enabled = sensor.ISError;
        }

        private void cbcISError_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbcISError.SelectedIndex != 0)
            {
                sensor.ISRandom = false;
                sensor.ISERRORMessage = (Trucaptureemulator.ERROR)Convert.ToInt32(cbcISError.Text.Split('-')[0].Trim());
            }
            else
            {
                sensor.ISRandom = true;
            }
        }
    }
}

