using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TruCaptureEmulator
{
    /*   
                         ^     ^                       
                        / \\  / \                     
                       /.  \\/   \      |\___/|      ________________________________________________                                
    *----*           / / |  \\    \  __/  O O\      |                                                |
    |   /          /  /  |   \\    \_\/  \     \    |                                                |
   / /\/         /   /   |    \\   _\/    '@___@   /    Cuidado!! Código Inseguro e Incompreensível  |                              
  /  /         /    /    |     \\ _\/       |U     \                                                 |
  |  |       /     /     |      \\\/        |       |                                                |
  \  |     /_     /      |       \\  )   \ _|_      |________________________________________________|                                   
  \   \       ~-./_ _    |    .- ; (  \_ _ _,\'                                         
  ~    ~.           .-~-.|.-* _                                                          
{-,                                                                                      
   \      ~-. _ .-~                 \      /\'
    \                   }            {   .*
     ~.                 '-/        /.-~----.
       ~- _             /        >..----.\\\
           ~ - - - - ^}_ _ _ _ _ _ _.-\\\  
         */


    public class Trucaptureemulator
    {
        private Crc16 CRC = new Crc16();
        private SerialPort com;
        private bool DB = false;
        private bool DT = false;
        private bool DE = false;
        private bool PW = false;
        private bool MA = false;
        private string ID = "$ID,DS-300,TruSense S200-1.14-62,JUL 15 2013,219DE3CA*97E0";//"$ID,DS-200,TruSense S200-1.14-36,JAN 24 2012,640CB51F*65EA";
        private string SN = "DS000375";
        private bool GO = false;
        private string OK = "$OK*0774";
        private Thread write;
        private int count;
        private string banner = "TruSense S200,DS-300-1.14 PRF[1000/2800] [CP-WP-3-UL]" + "\r\n" + "(c) 2010-2015 Laser Technology Inc. All rights reserved." + "\r\n\r\n" + "$READY\r";
        private int time = 1000;
        private string erro = "ERRO";
        private CM Modo_CM = CM.TruCapture;
        private MM Modo_MM = (MM)4;
        private int SL_limit = 0;
        private SW SW_value;
        private MD MD_value;
        private bool NX = false;
        private bool ND = false;        
        private int velmin = 0;
        private int velmax = 255;
        private int distmin = 0;
        private int distmax = 255;
        private int HT = 400;
        private DM Modo_DM = (DM)3;
        private DateTime timesince;
        private DateTime intervalo = new DateTime();
        public delegate void CapturaEventHandler(object sender, string e, bool trigger);
        public event CapturaEventHandler Writeserial;
        private string atbLog = Application.StartupPath + "\\Parameters\\";
        private FileStream atbFileStream;
        private bool ON = false;
        private bool ruido = false;
        private string retorno;
        private MU MU_value;
        private bool inibe;
        private bool mdlimit;
        private Display d;
        private bool display = false;
        private Display.Period periodo;
        private bool display_exists = false;
        private string IP_display;
        private int porta;
        private int velocidade;
        private bool display_mode;
        private List<string> strings;
        private List<int> timesBetweenStrings = new List<int>();
        private bool aproximando = false;

        private ManualResetEvent _mrePeriod = new ManualResetEvent(false);

        private int dtpw;
        private int minstrings = 3;
        private int maxstrings = 15;
        private int distinfms = -10;
        private int distsupms = 3;
        private int tempostringsmin = 1000;
        private int tempostringsmax = 10000;
        private bool multistrings = false;
        private bool fixardecimal = false;
        private int decimalValue = 0;
        private bool _transition = true;
        private string _velplaca = "40,48";
        private string _serialSensorPlaca = "DS000375";
        private string _boardHeight = "400";
        private MultiStringsDirection _direction = MultiStringsDirection.Departing;
        private double _timeDiference = 0;
        private bool _ligaRouter = false;
        private bool _ligaCamera = false;
        private Thread _SendEv;
        //private double _timeToSendEV = 3000000; //50min
        private int _timeToSendEV = 50; //50min
        private DateTime _EVControl;
        private bool EG = false;
        private bool _variableTime = false;
        private bool _board = false;
        private bool _laserOn = false;
        private bool _PDError = false;
        public bool ISError { get; set; }
        private bool _sendBanner = false;
        private DG_mode _DGMode = DG_mode.soft_locked;

        private AccessLevel accessLevel = AccessLevel.None;
        private string _passwordlevel1 = "ADMIN";
        private string _passwordlevel2 = "wjdwldbsvotmdnjwm917";
        private string _passwordLock = "862171138";
        private string _passwordLTI = "0ZIvcg.eSV1bicwrdh";
        //DS003493 = OzatqKgX.R7VuqiHTr
        private string _boardSerial = "PC0001";
        private string _PC = "20,50,30";
        private string _RF = "3.0";
        private OperationMode _boardMode = OperationMode.Nenhum;
        private bool _simulaErros = false;

        private Sector4Type setor4 = Sector4Type.CURRENT_temperature_and_capture;


        private bool PSdoubleCheck = false;
        private string tempPassword = string.Empty;

        public delegate void Trigger();
        public event Trigger triggerhandler;

        public delegate void CaptureChanged(int capturespeed);
        public event CaptureChanged CaptureChangedHandler;

        private int _velocidadeDeCaptura = 5;

        private bool _modoTeste = false;
        private bool _modoSimulador = false;

        private InterferenceMode _modoInterferencia = InterferenceMode.Automatico;
        private bool _interferencia = false;
        private string _stringDeInterferencia = "#§↨";

        private int _desinibeTimes = 0;


        Random rf = new Random();

        public Trucaptureemulator(SerialPort conexao)
        {
            strings = new List<string>();
            com = conexao;
            write = new Thread(new ThreadStart(EscreveSerial));
            _EVControl = DateTime.Now;
            _SendEv = new Thread(new ThreadStart(SENDEV));
            _SendEv.Start();
            write.Priority = ThreadPriority.Highest;
            write.IsBackground = true;
            write.SetApartmentState(ApartmentState.STA);
            write.Start();
            Load_MU();
            Load_MD();
            save_parameters();
            /*try
            {
                d = new Display("192.168.50.149", 2101, 40);
                d.Start();
                d.exceptions_display += new Display.Exceptions_Display(exceptions_display);
            }
            catch
            { 
            }*/
        }

        private void SENDEV()
        {
            while (Program.programarodando)
            {
                if (_board && !EG)
                {
                    if (DateTime.Now.Subtract(_EVControl).TotalMinutes > _timeToSendEV)
                    {
                        try
                        {
                            enviapacote(this, "Enviado EV", false);
                            com.WriteLine(">EV\r");
                            _EVControl = DateTime.Now;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                else
                {
                    _EVControl = DateTime.Now;
                }
                Thread.Sleep(1000);
            }
        }

        public int Minstrings
        {
            get { return minstrings; }
            set { minstrings = value; }
        }


        public int Maxstrings
        {
            get { return maxstrings; }
            set { maxstrings = value; }
        }


        public int Distinfms
        {
            get { return distinfms; }
            set { distinfms = value; }
        }


        public int Distsupms
        {
            get { return distsupms; }
            set { distsupms = value; }
        }

        private string GetDGResponse()
        {
            int DG = (int)_DGMode;
            string retorno = DG.ToString();
            retorno = retorno.PadLeft(2, '0');
            retorno = retorno.Insert(1, ",");
            return retorno;
        }

        public int Tempostringsmin
        {
            get { return tempostringsmin; }
            set { tempostringsmin = value; }
        }

        public int Tempostringsmax
        {
            get { return tempostringsmax; }
            set { tempostringsmax = value; }
        }

        public bool Display_mode
        {
            get { return display_mode; }
            set
            {
                display_mode = value;
                if (display_mode)
                {
                    d.Mode = TruCaptureEmulator.Display.OperationMode.SPEED;
                }
                else
                {
                    d.Mode = TruCaptureEmulator.Display.OperationMode.DISTANCE;
                }
            }
        }

        public int Velocidade
        {
            get { return velocidade; }
            set { velocidade = value; }
        }

        public int Porta
        {
            get { return porta; }
            set { porta = value; }
        }

        public string IP_display1
        {
            get { return IP_display; }
            set { IP_display = value; }
        }

        public Display.Period Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }

        public bool Display_exists
        {
            get { return display_exists; }
        }

        public bool Display
        {
            get { return display; }
            set { display = value; }
        }

        public void New_Display(string IP, int porta, int velocidade)
        {
            try
            {
                this.IP_display = IP;
                this.porta = porta;
                this.velocidade = velocidade;
                d = new Display(IP, porta, velocidade);
                d.Start();
                d.exceptions_display += new Display.Exceptions_Display(exceptions_display);
                display_exists = true;
            }
            catch
            {
                throw new Exception("Não foi possível conectar-se ao display");
            }
        }

        void exceptions_display(object sender, string text)
        {
            this.enviapacote(this, text, false);
        }

        public void Set_Period(DateTime begin, DateTime end)
        {
            periodo.begin = begin;
            periodo.end = end;
            d.Interval = periodo;
        }

        public void Set_Speed(int speed)
        {
            d.Speed = speed;
        }

        public void Set_Mode(bool mode)
        {

        }

        public string Conc_checksum(string text)
        {
            string retorno = string.Empty;
            retorno = CRC.ComputeChecksum(Encoding.Default.GetBytes(text)).ToString("X").PadLeft(4, '0');
            return retorno;
        }

        public void Set_Parameters(bool DB, bool DT, bool DE, MM Modo_MM, DM Modo_DM, int velmin, int velmax, int distmin, int distmax, string SN, int HT, MD md, bool FixDec, int decvalue, int captura)
        {
            this.DB = DB;
            this.DT = DT;
            this.DE = DE;
            this.Modo_MM = Modo_MM;
            this.Modo_DM = Modo_DM;
            if (!_board)
            {
                this.velmin = velmin;
                this.velmax = velmax;
            }
            else
            {
                this.velmin = 5;
                this.velmax = 255;
            }
            this.distmin = distmin;
            this.distmax = distmax;
            if (!string.IsNullOrEmpty(SN))
            {
                this.SN = SN;
                _serialSensorPlaca = SN;
            }
            this.HT = HT;
            this.MD_value.min = md.min;
            this.MD_value.max = md.max;
            this.MD_value.med = md.med;
            fixardecimal = FixDec;
            decimalValue = decvalue;
            _velocidadeDeCaptura = captura;
            save_parameters();
        }

        private void Load_MU()
        {
            MU_value.distunit = (UNIT)1;
            MU_value.decdist = 1;
            MU_value.speedunit = "K";
            MU_value.decspeed = 0;
        }

        private void Load_MD()
        {
            MD_value.max = 50;
            MD_value.med = 30;
            MD_value.min = 20;
        }

        public bool Get_DB()
        {
            return DB;
        }

        public bool Get_DT()
        {
            return DT;
        }

        public bool Get_DE()
        {
            return DE;
        }

        public MM Get_MM()
        {
            return Modo_MM;
        }

        public DM Get_DM()
        {
            return Modo_DM;
        }

        public MD Get_MD()
        {
            return MD_value;
        }

        public int Get_Velmax()
        {
            return velmax;
        }

        public int Get_Velmin()
        {
            return velmin;
        }

        public int Get_Distmax()
        {
            return distmax;
        }

        public int Get_Distmin()
        {
            return distmin;
        }

        public int Get_HT()
        {
            return HT;
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public bool Ruido
        {
            get { return ruido; }
            set { ruido = value; }
        }

        public string SN1
        {
            get { return SN; }
            set { SN = value; }
        }

        public bool Inibe
        {
            get { return inibe; }
            set
            {
                _desinibeTimes = 0;
                inibe = value; 
            }
        }

        public bool Mdlimit
        {
            get { return mdlimit; }
            set { mdlimit = value; }
        }

        public bool Multistrings
        {
            get
            {
                return multistrings;
            }

            set
            {
                multistrings = value;
            }
        }

        public MultiStringsDirection Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;
            }
        }

        public bool Fixardecimal
        {
            get
            {
                return fixardecimal;
            }

            set
            {
                fixardecimal = value;
            }
        }

        public int DecimalValue
        {
            get
            {
                return decimalValue;
            }

            set
            {
                decimalValue = value;
            }
        }

        public bool Board
        {
            get
            {
                return _board;
            }

            set
            {
                if (!value)
                {
                    _transition = true;
                    _laserOn = true;
                }
                else
                {
                    GO = false;
                    velmin = 5;
                    velmax = 255;
                }
                _board = value;
            }
        }

        public bool VariableTime
        {
            get
            {
                return _variableTime;
            }

            set
            {
                _variableTime = value;
            }
        }

        public bool SimulaErros
        {
            get
            {
                return _simulaErros;
            }

            set
            {
                if (value)
                {
                    strings.Clear();
                    GO = true;
                }
                else
                {
                    GO = false;
                }
                _simulaErros = value;
            }
        }

        public bool PDError { get => _PDError; set => _PDError = value; }
        public int VelocidadeCaptura { get => _velocidadeDeCaptura; set => _velocidadeDeCaptura = value; }
        public bool isON
        {
            get
            {
                return ON;
            }
        }

        public DG_mode DGMode { get => _DGMode; set => _DGMode = value; }
        public Sector4Type Setor4 { get => setor4; set => setor4 = value; }
        public bool ModoTeste { get => _modoTeste; set => _modoTeste = value; }
        public bool ModoSimulador { get => _modoSimulador; set => _modoSimulador = value; }
        public InterferenceMode ModoInterferencia { get => _modoInterferencia; set => _modoInterferencia = value; }
        public bool Interferencia { get => _interferencia; set => _interferencia = value; }
        public string StringDeInterferencia { get => _stringDeInterferencia; set => _stringDeInterferencia = value; }
        public string ID1 { get => ID; set => ID = value; }

        public ERROR ISERRORMessage { get; set; } = ERROR.APD_FAILED;
        public bool ISRandom { get; set; } = false;

        public string[] IDList { get; } = { "$ID,DS-300,TruSense S200-1.14-62,JUL 15 2013,219DE3CA*97E0",
                                            "$ID,DS-300,TruSense S200-1.14-69,FEB 28 2014,F182ACC0*ED9E",
                                            "$ID,DS-200,TruSense S200-1.14-36,JAN 24 2012,640CB51F*65EA"};
        public int DesinibeTimes
        {
            get => _desinibeTimes;
            set
            {
                if (Inibe && GO && value > 0 && value < 11)
                {
                    _desinibeTimes = value;
                }
            }
        }

        public void Start_time()
        {
            timesince = DateTime.Now;
        }

        public string GetIFResponse()
        {
            /*
                SEM SETOR 4
                # STAMP: v1.14-69 km/h BR May 14 2015
                # Result ID: 2
                # Cal_Dat1: TruCalibration CS v2.4.7.258
                # Cal_Dat2: 
                # Code Area: From 0x8000 to 0x21087 [1]= 0x1CBD0 OK
                # CODE      CHECKSUM= 0xF182ACC0, In Table= 0xF182ACC0 OK SIZE = 102535B
                # DATA CHECKSUM SYS2= 0x90E76ED1, In Table= 0x90E76ED1 OK
                # DATA CHECKSUM USER= 0x756E8BE7, In Table= 0x756E8BE7 OK
                # CAL1 DATA CHECKSUM= 0x8F5E70E5, In Table= 0x8F5E70E5 OK VER=MATCHED
                # CAL2 DATA CHECKSUM= 0xBABF4245, In Table= 0xBABF4245 OK VER=MATCHED
                # MAXIMUM DISTANCE=3000 M


                COM SETOR 4
                #STAMP: NOT PROPER SETTING. CALL LTI
                #Result ID: 2
                #Cal_Dat1: TruCalibration CS v2.4.7.270
                #Cal_Dat2: 
                #Code Area: From 0x8000 to 0x21087 [1]= 0x1CBD0 OK
                #CODE      CHECKSUM= 0xF182ACC0, In Table= 0xF182ACC0 OK SIZE = 102535B
                #DATA CHECKSUM SYS2= 0x98E403C5, In Table= 0x98E403C5 OK
                #DATA CHECKSUM USER= 0xC4532AE4, In Table= 0xC4532AE4 OK
                #CAL1 DATA CHECKSUM= 0xB6A2A1B9, In Table= 0xB6A2A1B9 OK VER=MATCHED
                #CAL2 DATA CHECKSUM= 0xAA9035D8, In Table= 0xAA9035D8 OK VER=MATCHED
                #MAXIMUM DISTANCE=3000 M

                COM SETOR 4 MIKE
                #STAMP: v1.14-69 km/h BR July 24 2018
                #Result ID: 1
                #Cal_Dat1: TruCalibration CS v2.4.7.258
                #Cal_Dat2: 
                #Code Area: From 0x8000 to 0x21087 [1]= 0x1CBD0 OK
                #CODE      CHECKSUM= 0xF182ACC0, In Table= 0xF182ACC0 OK SIZE = 102535B
                #DATA CHECKSUM SYS2= 0x93E86F04, In Table= 0x93E86F04 OK
                #DATA CHECKSUM USER= 0x41C92323, In Table= 0x41C92323 OK
                #CAL1 DATA CHECKSUM= 0x63F4466B, In Table= 0x63F4466B OK VER=MATCHED
                #CAL2 DATA CHECKSUM= 0xEC0C997D, In Table= 0xEC0C997D OK VER=MATCHED
                #MAXIMUM DISTANCE=3000 M
            */

            string retorno = string.Empty;
            if (setor4 == Sector4Type.SECOND_highTemperature_wrongCapture)
            {
                retorno = "#STAMP: v1.14-69 km/h BR May 14 2015" + Environment.NewLine;
                retorno = retorno + "#Result ID: 2" + Environment.NewLine;
                retorno = retorno + "#Cal_Dat1: TruCalibration CS v2.4.7.270" + Environment.NewLine;
                retorno = retorno + "#Cal_Dat2:" + Environment.NewLine;
                retorno = retorno + "#Code Area: From 0x8000 to 0x21087 [1]= 0x1CBD0 OK" + Environment.NewLine;
                retorno = retorno + "#CODE      CHECKSUM= 0xF182ACC0, In Table= 0xF182ACC0 OK SIZE = 102535B" + Environment.NewLine;
                retorno = retorno + "#DATA CHECKSUM SYS2= 0x90E76ED1, In Table= 0x90E76ED1 OK" + Environment.NewLine;
                retorno = retorno + "#DATA CHECKSUM USER= 0x756E8BE7, In Table= 0x756E8BE7 OK" + Environment.NewLine;
                retorno = retorno + "#CAL1 DATA CHECKSUM= 0x8F5E70E5, In Table= 0x8F5E70E5 OK VER=MATCHED" + Environment.NewLine;
                retorno = retorno + "#CAL2 DATA CHECKSUM= 0xBABF4245, In Table= 0xBABF4245 OK VER=MATCHED" + Environment.NewLine;
                retorno = retorno + "#MAXIMUM DISTANCE=3000 M";
            }
            else if(setor4 == Sector4Type.FIRST_lowTemperature)
            {
                retorno = "#STAMP: NOT PROPER SETTING. CALL LTI" + Environment.NewLine;
                retorno = retorno + "#Result ID: 2" + Environment.NewLine;
                retorno = retorno + "#Cal_Dat1: TruCalibration CS v2.4.7.270" + Environment.NewLine;
                retorno = retorno + "#Cal_Dat2:" + Environment.NewLine;
                retorno = retorno + "#Code Area: From 0x8000 to 0x21087 [1]= 0x1CBD0 OK" + Environment.NewLine;
                retorno = retorno + "#CODE      CHECKSUM= 0xF182ACC0, In Table= 0xF182ACC0 OK SIZE = 102535B" + Environment.NewLine;
                retorno = retorno + "#DATA CHECKSUM SYS2= 0x98E403C5, In Table= 0x98E403C5 OK" + Environment.NewLine;
                retorno = retorno + "#DATA CHECKSUM USER= 0xC4532AE4, In Table= 0xC4532AE4 OK" + Environment.NewLine;
                retorno = retorno + "#CAL1 DATA CHECKSUM= 0xB6A2A1B9, In Table= 0xB6A2A1B9 OK VER=MATCHED" + Environment.NewLine;
                retorno = retorno + "#CAL2 DATA CHECKSUM= 0xAA9035D8, In Table= 0xAA9035D8 OK VER=MATCHED" + Environment.NewLine;
                retorno = retorno + "#MAXIMUM DISTANCE=3000 M";
            }
            else if (setor4 == Sector4Type.CURRENT_temperature_and_capture)
            {
                retorno = "#STAMP: v1.14-69 km/h BR July 24 2018" + Environment.NewLine;
                retorno = retorno + "#Result ID: 1" + Environment.NewLine;
                retorno = retorno + "#Cal_Dat1: TruCalibration CS v2.4.7.258" + Environment.NewLine;
                retorno = retorno + "#Cal_Dat2: " + Environment.NewLine;
                retorno = retorno + "#Code Area: From 0x8000 to 0x21087 [1]= 0x1CBD0 OK" + Environment.NewLine;
                retorno = retorno + "#CODE      CHECKSUM= 0xF182ACC0, In Table= 0xF182ACC0 OK SIZE = 102535B" + Environment.NewLine;
                retorno = retorno + "#DATA CHECKSUM SYS2= 0x93E86F04, In Table= 0x93E86F04 OK" + Environment.NewLine;
                retorno = retorno + "#DATA CHECKSUM USER= 0x41C92323, In Table= 0x41C92323 OK" + Environment.NewLine;
                retorno = retorno + "#CAL1 DATA CHECKSUM= 0x63F4466B, In Table= 0x63F4466B OK VER=MATCHED" + Environment.NewLine;
                retorno = retorno + "#CAL2 DATA CHECKSUM= 0xEC0C997D, In Table= 0xEC0C997D OK VER=MATCHED" + Environment.NewLine;
                retorno = retorno + "#MAXIMUM DISTANCE=3000 M";
            }
            return retorno;
        }

        public string answer(string lido)
        {
            retorno = string.Empty;
            if (ON)
            {
                if (lido.Length >= 3)
                {
                    #region $ST
                    if ((!_board || (_board && _laserOn && !EG)) && lido.StartsWith("$ST"))
                    {
                        _mrePeriod.Set();
                        retorno = OK;
                        GO = false;
                    }
                    #endregion
                    #region $GO
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$GO")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            count = 0;
                            retorno = OK;                            
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            try
                            {
                                int index = lido.IndexOf(',') + 1;
                                index = lido.Length - index;
                                count = Convert.ToInt32(lido.Substring(4, index));
                                retorno = OK;
                            }
                            catch
                            {
                                count = 0;
                                retorno = OK;
                            }
                        }
                        if (retorno == OK)
                        {
                            GO = true;
                        }
                        else
                        {
                            count = 0;
                            GO = false;
                        }
                    }
                    #endregion
                    #region $ID
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.StartsWith("$IA"))
                    {
                        if (lido == "$IA," + _passwordLock)
                        {
                            _DGMode = DG_mode.soft_locked;
                            retorno = OK;
                        }
                        else
                        {
                            if (!DE)
                            {
                                try
                                {
                                    string temp = "$ER,35," + lido.Split(',')[1];
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                catch (Exception)
                                {

                                }
                            }
                            else
                            {
                                string temp = "ER,35," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ") + "," + lido.Split(',')[1];
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                    }
                    #endregion
                    #region $UL
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.StartsWith("$UL"))
                    {
                        if (_DGMode == DG_mode.soft_locked || (_DGMode == DG_mode.locked && accessLevel == AccessLevel.FullAccess))
                        {
                            if (lido == "$UL," + _passwordLock)
                            {
                                _DGMode = DG_mode.unlocked;
                            }
                            else
                            {
                                if (!DE)
                                {
                                    try
                                    {
                                        string temp = "$ER,35," + lido.Split(',')[1];
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                                else
                                {
                                    string temp = "ER,35," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ") + "," + lido.Split(',')[1];
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                    }
                    #endregion
                    #region $IF
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.StartsWith("$IF"))
                    {
                        retorno = GetIFResponse();
                    }
                    #endregion
                    #region $IS
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.StartsWith("$IS"))
                    {
                        ERROR tempError = ISRandom ? GeraErroSemUndefined() : ISERRORMessage;
                        string iserrorstring = (ISError ? Convert.ToInt32(tempError).ToString() : "0");
                        string temp = $"IS,{(GO ? "1": "0")},{iserrorstring}," + Convert.ToInt32(accessLevel).ToString();
                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                    }
#endregion
                    #region $ID
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.StartsWith("$ID"))
                    {
                        //retorno = "$" + "ID,DS-300,TruSense S200-1.14-62,JUL 15 2013,219DE3CA" + "*" + Conc_checksum("ID,DS-300,TruSense S200-1.14-62,JUL 15 2013,219DE3CA");
                        retorno = ID;
                    }
                    #endregion
                    #region $SN
                    else if ((!_board || (_board && _laserOn && !EG)) && lido == "$SN")
                    {
                        string temp = "SN," + SN;
                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                    }
                    #endregion
                    #region $BP
                    else if ((!_board || (_board && _laserOn && !EG)) && lido == "$BP")
                    {
                        string temp = "BP";
                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                    }
                    #endregion
                    #region $DB
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$DB")
                    {
                        if (lido.Length == 3)
                        {
                            if (DB)
                            {
                                string temp = "DB,1";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                            else
                            {
                                string temp = "DB,0";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                        else if (lido.Length == 5)
                        {
                            if (PW)
                            {
                                if (lido.Substring(4, 1) == "0")
                                {
                                    string temp = "DB,0";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    DB = false;
                                }
                                else if (lido.Substring(4, 1) == "1")
                                {
                                    string temp = "DB,1";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    DB = true;
                                }
                                else
                                {
                                    try
                                    {
                                        Convert.ToInt32(lido.Substring(4, 1));
                                        string temp = "DB,1";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                        DB = true;
                                    }
                                    catch (Exception)
                                    {
                                        string temp = "DB,0";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                        DB = false;
                                    }
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                        else
                        {
                            if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                            {
                                if (DB)
                                {
                                    string temp = "DB,1";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                else
                                {
                                    string temp = "DB,0";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                            else
                            {
                                if (DB)
                                {
                                    string temp = "DB,1";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                else
                                {
                                    string temp = "DB,0";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                    }
                    #endregion
                    #region $PS
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$PS")
                    {
                        try
                        {
                            string[] cmd = lido.Split(',');
                            if (PW)
                            {
                                if (!PSdoubleCheck)
                                {
                                    retorno = "$OK,PS AGAIN*7774";
                                    PSdoubleCheck = true;
                                    tempPassword = cmd[1];
                                }
                                else
                                {
                                    PSdoubleCheck = false;
                                    if (cmd[1] == tempPassword)
                                    {
                                        _passwordlevel1 = tempPassword;
                                        retorno = OK;
                                    }
                                    else
                                    {
                                        if (DE)
                                        {
                                            string temp = "ER,24,INCORRECT PASSWORD";
                                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                                        }
                                        else
                                        {
                                            string temp = "ER,24";
                                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                                        }
                                    }
                                    tempPassword = string.Empty;
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            if (!DE)
                            {
                                string temp = "ER,22";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                            else
                            {
                                string temp = "ER,22," + ERROR.SYNTAX_ERROR.ToString().Replace("_", " ");
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }

                    }
                    #endregion
                    #region $CM
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$CM")
                    {

                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            string temp = "CM," + Convert.ToInt32(Modo_CM).ToString();
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            if (PW)
                            {
                                try
                                {
                                    Modo_CM = (CM)Convert.ToInt32(lido.Substring(4, 1));
                                    string temp = "CM," + lido.Substring(4, 1);
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                catch
                                {
                                    Modo_CM = CM.Truspeed;
                                    string temp = "CM," + (int)Modo_CM;
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                    }
                    #endregion
                    #region $PW
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$PW")
                    {
                        try
                        {
                            if (lido.Length == 3)
                            {
                                PW = false;
                                retorno = "$PW,0*04BC";
                            }
                            else
                            {
                                string[] cmd = lido.Split(',');
                                if (cmd[1] == _passwordlevel1.ToUpper())
                                {
                                    string temp = "PW,1";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    PW = true;
                                    accessLevel = AccessLevel.Level1;
                                }
                                else if (cmd[1] == _passwordlevel2.ToUpper())
                                {
                                    string temp = "PW,2";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    PW = true;
                                    accessLevel = AccessLevel.Level2;
                                }
                                else if (cmd[1] == _passwordLTI.ToUpper())
                                {
                                    string temp = "PW,3";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    PW = true;
                                    accessLevel = AccessLevel.FullAccess;
                                }
                                else
                                {
                                    if (DE)
                                    {
                                        string temp = "ER,24,INCORRECT PASSWORD";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                    else
                                    {
                                        string temp = "ER,24";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            if (DE)
                            {
                                string temp = "ER,24,INCORRECT PASSWORD";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                            else
                            {
                                string temp = "ER,24";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                    }
                    #endregion
                    #region $DT
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$DT")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            if (DT)
                            {
                                string temp = "DT,1";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                            else
                            {
                                string temp = "DT,0";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            if (PW)
                            {

                                try
                                {
                                    int value = Convert.ToInt32(lido.Substring(4, 1));
                                    if (value == 0)
                                    {
                                        DT = false;
                                        string temp = "DT,0";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                    else
                                    {
                                        DT = true;
                                        string temp = "DT,1";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }

                                }
                                catch (Exception)
                                {
                                    if (DT)
                                    {
                                        string temp = "DT,1";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                    else
                                    {
                                        string temp = "DT,0";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                }                               
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }                        
                    }
                    #endregion
                    #region $MA
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$MA")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            if (MA)
                            {
                                string temp = "MA,2";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                            else
                            {
                                string temp = "MA,0";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {

                            try
                            {
                                if (lido.Substring(4, 1) == "0")
                                {
                                    MA = false;
                                    string temp = "MA,0";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                else
                                {
                                    MA = true;
                                    string temp = "MA,2";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                            catch
                            {
                                string temp = "MA,0";
                                if (MA)
                                {
                                    temp = "MA,2";
                                }
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                    }
                    #endregion
                    #region $MM
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$MM")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            string temp = "MM," + Convert.ToInt32(Modo_MM).ToString();
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            if (lido.Contains(","))
                            {
                                if (PW)
                                {
                                    try
                                    {
                                        Modo_MM = (MM)Convert.ToInt32(lido.Substring(4, 1));
                                        string temp = "MM," + Convert.ToInt32(Modo_MM).ToString();
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                    catch (Exception ex)
                                    {
                                        Modo_MM = MM.standard_mode;
                                        string temp = "MM," + Convert.ToInt32(Modo_MM).ToString();
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                }
                                else
                                {
                                    if (!DE)
                                    {
                                        retorno = "$ER,25*C9C9";
                                    }
                                    else
                                    {
                                        string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                }
                            }
                            else
                            {
                                string temp = "MM," + Convert.ToInt32(Modo_MM).ToString();
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                    }
                    #endregion
                    #region $SL
                    else if ((!_board || (_board && _laserOn && !EG)) && (lido.Substring(0, 3) == "$SL"))
                    {
                        if (lido.Length == 3)
                        {
                            string temp = "SL," + SL_limit.ToString();
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else
                        {
                            if (PW)
                            {
                                try
                                {
                                    SL_limit = Convert.ToInt32(lido.Substring(4, (lido.Length - 4)));
                                    string temp = "SL," + SL_limit.ToString();
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                catch (Exception ex)
                                {
                                    retorno = erro;
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                    }
                    #endregion
                    #region $SW
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$SW")
                    {
                        if (lido.Length == 3)
                        {
                            string temp = "SW," + SW_value.sw_0.ToString() + "," + SW_value.sw_1.ToString();
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else
                        {
                            if (PW)
                            {
                                try
                                {
                                    string[] s = lido.Split(',');
                                    SW_value.sw_0 = Convert.ToInt32(s[1]);
                                    SW_value.sw_1 = Convert.ToInt32(s[2]);
                                    string temp = "SW," + SW_value.sw_0.ToString() + "," + SW_value.sw_1.ToString();
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                catch (Exception ex)
                                {
                                    retorno = erro;
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                    }
                    #endregion
                    #region $MD
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$MD")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            string temp = "MD," + MD_value.min.ToString() + "," + MD_value.max.ToString() + "," + MD_value.med.ToString() + "," + MU_value.distunit;
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            if (PW)
                            {
                                try
                                {
                                    string[] s = lido.Split(',');
                                    //TODO: Validar entrada MD
                                    int min = Convert.ToInt32(s[1]);
                                    int max = Convert.ToInt32(s[2]);
                                    int med = Convert.ToInt32(s[3]);

                                    if (min < 12 || min > 150 || med < 12 || med > 150 || max < 12 || max > 150 ||
                                    (min >= max || min >= med) || (med >= max) || (min + 7 > med) || (med + 7 > max))
                                    {
                                        string temp = "ER,35";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                    else
                                    {
                                        MD_value.min = min;
                                        MD_value.max = max;
                                        MD_value.med = med;
                                        string temp = "MD," + MD_value.min.ToString() + "," + MD_value.max.ToString() + "," + MD_value.med.ToString();
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string temp = "MD," + MD_value.min.ToString() + "," + MD_value.max.ToString() + "," + MD_value.med.ToString() + "," + MU_value.distunit;
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                    }
                    #endregion
                    #region $SU
                    else if ((!_board || (_board && _laserOn && !EG)) && lido == "$SU")
                    {
                        //retorno = "$READY";
                        PW = false;
                        PSdoubleCheck = false;
                        accessLevel = AccessLevel.None;
                        save_parameters();
                        //PW = true;
                    }
                    #endregion
                    #region $PD
                    else if ((!_board || (_board && _laserOn && !EG)) && lido == "$PD")
                    {
                        retorno = "$PD,BY COMMAND*7BB1";
                        _sendBanner = true;

                        PW = false;
                        PSdoubleCheck = false;
                        accessLevel = AccessLevel.None;

                        if (!MA)
                        {
                            GO = false;
                        }
                        save_parameters();
                        //PW = true;
                    }
                    #endregion
                    #region $FD
                    else if ((!_board || (_board && _laserOn && !EG)) && lido == "$FD")
                    {
                        FactoryDefault();

                        _sendBanner = true;
                        PW = false;
                        PSdoubleCheck = false;
                        accessLevel = AccessLevel.None;
                        GO = false;
                    }
                    #endregion
                    #region $NX
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$NX")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            if (NX)
                            {
                                retorno = "$NX,16384*81CB";
                            }
                            else
                            {
                                retorno = "$NX,0*2F8A";
                            }
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            int value;

                            try
                            {
                                value = Convert.ToInt32(lido.Substring(4, 1));
                                if (value == 0)
                                {
                                    NX = false;
                                    retorno = "$NX,0*2F8A";
                                }
                                else
                                {
                                    NX = true;
                                    retorno = "$NX,16384*81CB";
                                }
                            }
                            catch (Exception)
                            {
                                if (NX)
                                {
                                    retorno = "$NX,16384*81CB";
                                }
                                else
                                {
                                    retorno = "$NX,0*2F8A";
                                }
                            }  
                        }
                    }
                    #endregion
                    #region $ND
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$ND")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                           
                            if (ND)
                            {
                                retorno = "$ND,32768*142B";
                            }
                            else
                            {
                                retorno = "$ND,0*2F8A";
                            }
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            int value;

                            try
                            {
                                value = Convert.ToInt32(lido.Substring(4, 1));
                                if (value == 0)
                                {
                                    ND = false;
                                    retorno = "$ND,0*2F8A";
                                }
                                else
                                {
                                    ND = true;
                                    retorno = "$ND,32768*142B";
                                }
                            }
                            catch
                            {
                                if (ND)
                                {
                                    retorno = "$ND,32768*142B";
                                }
                                else
                                {
                                    retorno = "$ND,0*2F8A";
                                }
                            }
                        }
                    }
                    #endregion
                    #region $DG
                    else if ((!_board || (_board && _laserOn && !EG)) && lido == "$DG")
                    {
                        try
                        {
                            if (lido.Length == 3)
                            {
                                string temp = "DG," + GetDGResponse();
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                            else
                            {
                                string[] cmd = lido.Split(',');
                                if (cmd[1] == _passwordLock)
                                {
                                    _DGMode = DG_mode.unlocked;
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    #endregion
                    #region $DM
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$DM")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            string temp = "DM," + Convert.ToInt32(Modo_DM).ToString();
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            if (PW)
                            {
                                try
                                {
                                    Modo_DM = (DM)Convert.ToInt32(lido.Substring(4, 1));
                                    string temp = "DM," + Convert.ToInt32(Modo_DM).ToString();
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                catch (Exception ex)
                                {
                                    Modo_DM = DM.NO_OUTPUT;
                                    string temp = "DM," + (int)Modo_DM;
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                        else
                        {
                            string temp = "DM," + Convert.ToInt32(Modo_DM).ToString();
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                    }
                    #endregion
                    #region $DE
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$DE")
                    {
                        if (lido.Length == 3)
                        {
                            if (DE)
                            {
                                string temp = "DE,4";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                            else
                            {
                                string temp = "DE,0";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                        else if (lido.Length == 5)
                        {
                            if (PW)
                            {
                                if (lido.Substring(4, 1) == "0")
                                {
                                    string temp = "DE,0";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    DE = false;
                                }
                                else if (lido.Substring(4, 1) == "1")
                                {
                                    string temp = "DE,4";
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    DE = true;
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                        else
                        {
                            if (DE)
                            {
                                retorno = "$ER,20,UNDEFINED COMMAND,JJ*E9F0";
                            }
                            else
                            {
                                retorno = "$ER,20,JJ*FCF1";
                            }
                        }
                    }
                    #endregion
                    #region $MU
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$MU")
                    {
                        if (lido.Length == 3)
                        {
                            string temp = "MU," + MU_value.distunit + "," + MU_value.decdist + "," + MU_value.speedunit + "," + MU_value.decspeed;
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else
                        {
                            if (PW)
                            {
                                try
                                {
                                    string[] s = lido.Split(',');
                                    MU_value.distunit = (UNIT)Enum.Parse(typeof(UNIT), s[1]);
                                    MU_value.decdist = Convert.ToInt32(s[2]);
                                    if (s[3].Equals("K"))
                                    {
                                        MU_value.speedunit = s[3];
                                        MU_value.decspeed = Convert.ToInt32(s[4]);
                                    }
                                    else
                                    {
                                        throw new Exception("Falha ao programar unidade de velocidade");
                                    }
                                    string temp = "MU," + MU_value.distunit + "," + MU_value.decdist + "," + MU_value.speedunit + "," + MU_value.decspeed;
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                                catch (Exception ex)
                                {
                                    retorno = erro;
                                }
                            }
                            else
                            {
                                if (!DE)
                                {
                                    retorno = "$ER,25*C9C9";
                                }
                                else
                                {
                                    string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                    retorno = "$" + temp + "*" + Conc_checksum(temp);
                                }
                            }
                        }
                    }
                    #endregion
                    #region $HT
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$HT")
                    {
                        if (lido.Length == 3 || (lido.Length > 3 && lido.Substring(3, 1) != ","))
                        {
                            string temp = "HT," + HT + ",CM";
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                        else if (lido.Length > 3 && lido.Substring(3, 1) == ",")
                        {
                            try
                            {
                                int valorht = 0;

                                if (PW)
                                {
                                    try
                                    {
                                        valorht = Convert.ToInt32(lido.Substring(4, lido.Length - 4));
                                    }
                                    catch (Exception)
                                    {
                                        valorht = 0;
                                    }

                                    if (valorht >= 0 && valorht < 2001)
                                    {
                                        HT = valorht;
                                        string temp = "HT," + HT + ",CM";
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                    else
                                    {
                                        string temp = "ER,35," + valorht;
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                }
                                else
                                {
                                    if (!DE)
                                    {
                                        retorno = "$ER,25*C9C9";
                                    }
                                    else
                                    {
                                        string temp = "ER,25," + ERROR.AUTHORIZATION_REQUIRED.ToString().Replace("_", " ");
                                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                string temp = "HT," + HT + ",CM";
                                retorno = "$" + temp + "*" + Conc_checksum(temp);
                            }
                        }
                        else
                        {
                            string temp = "HT," + HT + ",CM";
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                    }
                    #endregion
                    #region $OZ
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.Substring(0, 3) == "$OZ")
                    {
                        Random random = new Random();
                        int temp = random.Next(0, 99);
                        int dec = random.Next(0, 9);
                        string gcelsius = "OZ," + temp.ToString() + "." + dec.ToString();
                        retorno = "$" + gcelsius + "*" + Conc_checksum(gcelsius);
                    }
                    #endregion
                    #region $SM
                    else if ((!_board || (_board && _laserOn && !EG)) && lido.StartsWith("$SM"))
                    {

                        string resp = "SM,1";
                        retorno = "$" + resp + "*" + Conc_checksum(resp);
                    }
                    #endregion
                    #region >placa
                    else if (_board && !EG && lido.Equals(">RH"))
                    {
                        DateTime _local = DateTime.Now.AddSeconds(_timeDiference);
                        byte[] response = FormatarCommandoRTC(_local);
                        retorno = string.Empty;
                        try
                        {
                            enviapacote(this, "Retornada Hora: " + _local.ToLongDateString() + " " + _local.ToLongTimeString(), false);
                            com.Write(response, 0, response.Length);
                        }
                        catch (Exception)
                        {
                            //TODO:                                    
                        }
                    }
                    else if (_board && !EG && lido == ">LL")
                    {
                        LL();
                        //_laserOn = true;
                        //retorno = string.Empty;
                        //Thread.Sleep(5000);
                    }
                    else if (_board && !EG && lido.Equals(">SR"))
                    {
                        if (_ligaRouter)
                        {
                            retorno = ">SR,1";
                        }
                        else
                        {
                            retorno = ">SR,0";
                        }
                    }
                    else if (_board && !EG && lido.Equals(">SL"))
                    {
                        if (_laserOn)
                        {
                            retorno = ">SL,1";
                        }
                        else
                        {
                            retorno = ">SL,0";
                        }
                    }
                    else if (_board && !EG && lido.Equals(">SC"))
                    {
                        if (_ligaCamera)
                        {
                            retorno = ">SC,1";
                        }
                        else
                        {
                            retorno = ">SC,0";
                        }
                    }
                    else if (_board && !EG && (lido.Equals(">AI") || lido.Equals(">DI") || lido.Equals(">LB") || lido.Equals(">DB")))
                    {
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">LM"))
                    {
                        _boardMode = OperationMode.Manutenção;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">DM"))
                    {
                        _boardMode = OperationMode.Nenhum;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">LA"))
                    {
                        _boardMode = OperationMode.Ajuste;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">LT"))
                    {
                        _boardMode = OperationMode.Teste;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">RN"))
                    {
                        _boardMode = OperationMode.Normal;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">DL"))
                    {
                        _laserOn = false;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">LC"))
                    {
                        _ligaCamera = true;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">DC"))
                    {
                        _ligaCamera = false;
                        retorno = ">OK";
                    }

                    else if (_board && !EG && lido.Equals(">DR"))
                    {
                        _ligaRouter = false;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">LR"))
                    {
                        _ligaRouter = true;
                        retorno = ">OK";
                    }

                    else if (_board && !EG && lido.Equals(">TR"))
                    {
                        _transition = true;
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.Equals(">RA"))
                    {
                        retorno = ">RA," + HT.ToString();
                    }
                    else if (_board && !EG && lido.Equals(">RS"))
                    {
                        retorno = ">RS," + _boardSerial;
                    }
                    else if (_board && !EG && lido.Equals(">RL"))
                    {
                        retorno = ">RL," + _serialSensorPlaca;
                    }
                    else if (_board && !EG && lido.StartsWith(">PL,"))
                    {
                        retorno = ">OK";
                        _serialSensorPlaca = lido.Replace(">PL,", string.Empty);
                    }
                    else if (_board && !EG && lido.Equals(">RV"))
                    {
                        retorno = ">RV," + _velplaca;
                    }
                    else if (_board && !EG && lido.StartsWith(">PV,"))
                    {
                        retorno = ">OK";
                        _velplaca = lido.Replace(">PV,", string.Empty);
                        try
                        {
                            _velocidadeDeCaptura = Convert.ToInt32(_velplaca.Split(',')[1].Trim());
                            CaptureChangedHandler?.Invoke(_velocidadeDeCaptura);
                        }
                        catch (Exception)
                        {

                        }

                    }
                    else if (_board && !EG && lido.StartsWith(">PA,"))
                    {
                        retorno = ">OK";
                        _boardHeight = lido.Replace(">PA,", string.Empty);
                    }
                    else if (_board && !EG && lido.StartsWith(">RA"))
                    {
                        retorno = ">RA," + _boardHeight;
                    }
                    else if (_board && !EG && lido.StartsWith(">RC"))
                    {
                        retorno = ">RC," + _PC;
                    }
                    else if (_board && !EG && lido.StartsWith(">PC,"))
                    {
                        _PC = lido.Replace(">PC,", string.Empty);
                        retorno = ">OK";
                    }
                    else if (_board && !EG && lido.StartsWith(">RF"))
                    {
                        retorno = ">RF," + _RF;
                    }
                    else if (_board && !EG && lido.StartsWith(">EG"))
                    {
                        EG = true;
                        GO = false;
                        retorno = ">OK";
                        enviapacote(this, "ERRO GRAVE ARM", false);
                    }
                    else if (_board && lido.StartsWith(">RT"))
                    {
                        retorno = ">OK";
                        RT();
                        enviapacote(this, "REINICIADA BOARD", false);
                    }
                    #endregion
                    #region ERRO
                    //else if (!_board || (_board && !EG && lido.StartsWith("$")))//Verificar
                    else if (_board && !EG && lido.StartsWith("$"))//Verificar
                    {
                        if (!DE)
                        {
                            retorno = "$ER,20*" + Conc_checksum("ER,20");
                        }
                        else
                        {
                            retorno = "$ER,20*" + Conc_checksum("ER,20");
                            string temp = "ER,20," + ERROR.INVALID_COMMAND.ToString().Replace("_", " ");
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                    }
                    else if (!_board && lido.StartsWith("$"))//Verificar
                    {
                        if (!DE)
                        {
                            retorno = "$ER,20*" + Conc_checksum("ER,20");
                        }
                        else
                        {
                            retorno = "$ER,20*" + Conc_checksum("ER,20");
                            string temp = "ER,20," + ERROR.INVALID_COMMAND.ToString().Replace("_", " ");
                            retorno = "$" + temp + "*" + Conc_checksum(temp);
                        }
                    }
                    else if (_board && !EG && !_laserOn && lido.StartsWith("$"))
                    {
                        retorno = string.Empty;
                    }
                    else if (_board && !EG)
                    {
                        retorno = ">ER,05";
                    }
                    #endregion
                }
                else if ((!_board && lido.StartsWith("$")) || (_board && !EG && _laserOn && lido.StartsWith("$")))
                {
                    if (!DE)
                    {
                        retorno = "$ER,20*" + Conc_checksum("ER,20");
                    }
                    else
                    {
                        retorno = "$ER,20*" + Conc_checksum("ER,20");
                        string temp = "ER,20," + ERROR.INVALID_COMMAND.ToString().Replace("_", " ");
                        retorno = "$" + temp + "*" + Conc_checksum(temp);
                    }
                }
                else if (_board && !EG && !_laserOn && lido.StartsWith("$"))
                {
                    retorno = string.Empty;
                }
                else if (_board && !EG)
                {
                    retorno = ">ER,05";
                }
            }

            return AplicaInterferencia(retorno);            
        }

        public void PH(byte[] dados)
        {
            if (_board && !EG)
            {
                string _localretorno = string.Empty;
                try
                {
                    string data = niblehextoint(dados[8]).ToString("00") + "/" +
                        niblehextoint(dados[9]).ToString("00") + "/" +
                        "20" + niblehextoint(dados[10]).ToString("00") + " " +
                        niblehextoint(dados[6]).ToString("00") + ":" +
                        niblehextoint(dados[5]).ToString("00") + ":" +
                        (niblehextoint(dados[4]) + 1).ToString("00"); // correção de 1s

                    // ano, mes, dia, hora, min, seg
                    DateTime dt = Convert.ToDateTime(data, new System.Globalization.CultureInfo("pt-BR"));
                    _timeDiference = dt.Subtract(DateTime.Now).TotalSeconds;   //DateTime.Now.Subtract(dt).TotalSeconds;
                    _localretorno = dt.ToLongDateString() + " " + dt.ToLongTimeString();
                    enviapacote(this, "Programada Hora: " + _localretorno, false);
                    com.WriteLine(">OK\r");
                }
                catch (Exception ex)
                {
                    //TODO:
                }
            }
        }

        public static int niblehextoint(byte b)
        {
            int Return = 0;
            int mais_sig = Convert.ToInt32(b) & 0xf0;
            int menos_sig = Convert.ToInt32(b) & 0x0f;
            mais_sig = mais_sig >> 4;
            string value = mais_sig.ToString() + menos_sig.ToString();
            Return = Convert.ToInt32(value);
            return Return;
        }

        private byte[] FormatarCommandoRTC(DateTime date)
        {
            byte[] dados = new byte[]
            {
                (byte)'>',
                (byte)'P',
                (byte)'H',
                (byte)',',
                inttoniblehex(date.Second),
                inttoniblehex(date.Minute),
                inttoniblehex(date.Hour),
                0x06,
                inttoniblehex(date.Day),
                inttoniblehex(date.Month),
                inttoniblehex(date.Year - 2000),
                0x10,
                (byte)'\r',
                (byte)'\n'
            };
            return dados;
        }

        public byte inttoniblehex(int num)
        {
            string text = num.ToString().PadLeft(2, '0');
            byte Return = new byte();
            byte[] b = new byte[1];
            b = StringToHexByteArray(text);
            Return = b[0];
            return Return;
        }

        public void LL()
        {
            Thread t = new Thread(() =>
            {
                Thread.Sleep(5000);
                _laserOn = true;
                enviapacote(this, ">OK", false);
                com.WriteLine(">OK\r");
            });
            t.Start();
        }

        public void Write_banner()
        {
            if (DB)
            {
                string send = banner;
                if (_PDError)
                {
                    send = send + ",ER62";
                }
                com.WriteLine(banner);
                this.enviapacote(this, banner, false);
            }
            else
            {
                if (_PDError)
                {
                    Thread.Sleep(1000);
                    enviapacote(this, "$ERROR,62", false);
                    com.WriteLine("$ERROR,62\r");
                }
            }
        }

        private void resetgo()
        {
            if (count > 1)
            {
                count--;
            }
            else if (count == 1)
            {
                GO = false;
                count = 0;
            }
        }

        public void Turnoff()
        {
            if (!MA)
                GO = false;
            PW = false;
            PSdoubleCheck = false;
            accessLevel = AccessLevel.None;
            ON = false;
            _sendBanner = false;
        }

        public void Turnon()
        {
            RT();
        }

        private void StartThread()
        {
            try
            {
                write.Start();
            }
            catch
            {

            }
        }

        private void RT()
        {
            _sendBanner = false;
            _ligaCamera = false;
            _ligaRouter = false;
            _laserOn = false;
            load_parameters();
            Write_banner();
            Start_time();
            PW = false;
            PSdoubleCheck = false;
            accessLevel = AccessLevel.None;
            _mrePeriod.Reset();
            ON = true;
            EG = false;
        }


        private bool NeedTrigger(string speedstring)
        {
            if(_modoSimulador)
            {
                return true;
            }

            string[] data = speedstring.Split(',');
            int velocidade = Convert.ToInt32(data[1].Replace("-", ""));
            double distancia = Convert.ToDouble(data[2]);

            int _compareSpeed = _velocidadeDeCaptura;

            if (_modoTeste)
            {
                _compareSpeed = 5;
            }

            if (distancia / 10 <= (MD_value.med + 1) && distancia / 10 >= (MD_value.med - 1) && velocidade >= _compareSpeed)
            {
                return true;
            }
            return false;
        }

        private void EscreveSerial()
        {
            while (Program.programarodando)
            {
                if (_sendBanner)
                {
                    _sendBanner = false;
                    Thread.Sleep(1000);

                    if (DB)
                    {
                        string send = Environment.NewLine + Environment.NewLine + banner;
                        if (_PDError)
                        {
                            send = send + ",ER62";
                        }
                        try
                        {
                            enviapacote(this, send, false);
                            com.WriteLine(send);
                        }
                        catch (Exception ex)
                        {
                            enviapacote(this, ex.Message, false);
                        }
                    }
                    else
                    {
                        if (_PDError)
                        {
                            enviapacote(this, "$ERROR,62", false);
                            com.WriteLine("$ERROR,62\r");
                        }
                    }
                }

                if (com != null && com.IsOpen && GO && Program.programarodando)
                {
                    _mrePeriod.Reset();

                    string write = string.Empty;
                    strings = new List<string>();

                    if ((multistrings || ModoSimulador) && count == 0)
                    {
                        if (!_simulaErros)
                        {
                            if (!ModoSimulador)
                            {
                                strings = Gera_multi_strings();
                            }
                            else
                            {
                                strings = Gera_multi_strings_simulador();
                            }
                        }
                        else
                        {
                            strings.Add(GeraErro());
                        }
                    }
                    else
                    {
                        if (!_simulaErros)
                        {
                            strings.Add(Gera_string());
                        }
                        else
                        {
                            strings.Add(GeraErro());
                        }
                    }

                    Stopwatch timeout = new Stopwatch();


                    int localTime = time;

                    //enviapacote(this, "time variavel: " + time, true);
                    //enviapacote(this, "timeout: " + timeout.ElapsedMilliseconds, true);

                    if (_variableTime)
                    {
                        Random r = new Random();
                        localTime = r.Next(tempostringsmin, tempostringsmax);
                    }

                    //timeout.Start();
                    // enviapacote(this, "timeout: " + timeout.ElapsedMilliseconds, true);
                    // enviapacote(this, "time: " + localTime, true);

                    if (_mrePeriod.WaitOne(localTime))
                    {
                        _mrePeriod.Reset();
                    }

                    //enviapacote(this, "timeout: " + timeout.ElapsedMilliseconds, true);
                    //timeout.Stop();

                    if (GO)
                    {
                        if (_simulaErros || (!Inibe && !_board) ||
                            (!Inibe && _transition && _board && _boardMode != OperationMode.Ajuste && _boardMode != OperationMode.Nenhum) ||
                            (Inibe && _desinibeTimes > 0))
                        {
                            _desinibeTimes = _desinibeTimes > 0 ? _desinibeTimes - 1 : 0;

                            int index_time = 0;
                            _transition = false;
                            bool alreadyTrigger = false;

                            Stopwatch _executionTime = new Stopwatch();
                            _executionTime.Start();
                            foreach (string s in strings)
                            {
                                if (!GO)
                                {
                                    break;
                                }
                                write = s;
                                try
                                {
                                    bool trigger = false;
                                    if (!_interferencia && !alreadyTrigger && s.StartsWith("$SP") && NeedTrigger(s))
                                    {
                                        trigger = true;
                                    }                                


                                    enviapacote(this, write, trigger);
                                    com.Write(write + "\r\n");
                                   

                                    if (trigger)
                                    {
                                        if (triggerhandler != null)
                                        {
                                            triggerhandler();
                                        }
                                        alreadyTrigger = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    enviapacote(this, ex.Message, false);
                                }
                                try
                                {
                                    if (display && d.Is_running)
                                    {
                                        d.Send(write + "\r\n");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    enviapacote(this, ex.Message, false);
                                }
                                if (multistrings || _modoSimulador)
                                {
                                    if (timesBetweenStrings != null && timesBetweenStrings.Count == strings.Count)
                                    {
                                        int sleep = timesBetweenStrings[index_time] - (int)_executionTime.ElapsedMilliseconds;
                                        index_time++;
                                        if (sleep >= 0)
                                        {
                                            Thread.Sleep(sleep);
                                        }
                                        _executionTime.Restart();
                                    }
                                }
                            }
                            resetgo();
                        }
                        intervalo = DateTime.Now;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }

        public void enviapacote(object sender, string e, bool trigger)
        {
            Writeserial?.Invoke(this, e, trigger);
        }

        private string GeraErro()
        {
            string retorno = string.Empty;
            Random r = new Random();
            int num = r.Next(1, 99);
            ERROR er;
            if (Enum.IsDefined(typeof(ERROR), num))
            {
                er = (ERROR)num;
            }
            else
            {
                er = ERROR.UNDEFINED_ERROR;
            }

            string temp = string.Empty;
            if (!DE)
            {
                temp = "ER," + num.ToString();
                retorno = "$" + temp + "*" + Conc_checksum(temp);
            }
            else
            {
                //if(er.ToString().Length==2)
                //{
                //    er = ERROR.UNDEFINED_ERROR;
                //}
                temp = "ER," + num.ToString() + ", " + er.ToString().Replace("___", "/").Replace("__", ": ").Replace("_", " ");
                retorno = "$" + temp + "*" + Conc_checksum(temp);
            }
            return AplicaInterferencia(retorno);
        }

        private ERROR GeraErroSemUndefined()
        {
            string[] erros = Enum.GetNames(typeof(ERROR));
            Random rnd = new Random();
            return (ERROR)Enum.Parse(typeof(ERROR), erros[rnd.Next(0, erros.Length - 2)]);
        }

        private string Gera_string()
        {
            string retorno = string.Empty;
            string now = string.Empty;
            Random random = new Random();
            bool range = true;
            int x;
            int vel = random.Next(velmin, velmax);

            if (_direction == MultiStringsDirection.Departing)
            {
                vel = vel * (-1);
            }
            else if (_direction == MultiStringsDirection.Both)
            {
                if (random.Next(2) == 0)
                {
                    vel = vel * (-1);
                }
            }           

            int dist;
            if (!Mdlimit)
            {                
                dist = random.Next(distmin, distmax);
                if (dist == distmax)
                    range = false;
            }
            else
            {
                dist = MD_value.med;
            }

            string speed = vel.ToString();
            string distance = dist.ToString();
            #region gera_decimais
            string dec = string.Empty;
            if (!fixardecimal)
            {
                if (MU_value.decspeed > 0)
                {
                    if (MU_value.decspeed.Equals(1))
                    {
                        x = random.Next(0, 9);
                        dec = x.ToString();
                    }
                    else if (MU_value.decspeed.Equals(2))
                    {
                        x = random.Next(0, 99);
                        if (x < 10)
                            dec = "0" + x.ToString();
                    }
                    else
                    {
                        x = random.Next(0, 999);
                        if (x < 10)
                            dec = "00" + x.ToString();
                        else if (x < 100)
                            dec = "0" + x.ToString();
                        else
                            dec = x.ToString();
                    }
                }

                if (MU_value.decdist > 0)
                {
                    if (MU_value.decdist.Equals(1))
                    {
                        x = random.Next(0, 9);
                        dec = x.ToString();
                    }
                    else if (MU_value.decdist.Equals(2))
                    {
                        x = random.Next(0, 99);
                        if (x < 10)
                            dec = "0" + x.ToString();
                    }
                    else
                    {
                        x = random.Next(0, 999);
                        if (x < 10)
                            dec = "00" + x.ToString();
                        else if (x < 100)
                            dec = "0" + x.ToString();
                        else
                            dec = x.ToString();
                    }
                    if (range)
                    {
                        //???
                        //distance = dist.ToString() + "." + x.ToString();
                        distance = dist.ToString() + "." + dec;
                    }
                    else
                    {
                        distance = dist.ToString() + ".0";
                    }
                }
            }
            else
            {
                if (MU_value.decdist.Equals(1))
                {
                    dec = decimalValue.ToString();
                }
                else if (MU_value.decdist.Equals(2))
                {
                    dec = decimalValue.ToString() + "0";
                }
                else
                {
                    dec = decimalValue.ToString() + "00";
                }
                distance = dist.ToString() + "." + dec;
            }
            #endregion

            if (DT)
            {
                now = "," + Gera_timesince();
            }

            if (Modo_MM == (MM)4)
            {
                //string temp = "SP,00," + distance + "," + MU_value.speedunit + "," + MU_value.distunit + now;
                string temp = "SP," + speed + "," + distance + "," + MU_value.speedunit + "," + MU_value.distunit + now;
                //if (!ruido)
                //{
                retorno = "$" + temp + "*" + Conc_checksum(temp);
                //retorno = "SP," + speed + "," + distance + "," + MU_value.speedunit + "," + MU_value.distunit + now + "*" + checksum;
                //}
                //else
                //{
                #region ruido SP
                //retorno = "$" + temp + "*" + "1234";
                /*int y = random.Next(1, 3); int rand = random.Next(1, 255);
                int c1 = random.Next(1, 255); int c2 = random.Next(1, 255); int c3 = random.Next(1, 255);
                int c4 = random.Next(1, 255); int c5 = random.Next(1, 255); int c6 = random.Next(1, 255);

                if (c4 > rand) c4 = c4 - rand; else c4 = c4 / 2;
                if (c5 > rand) c5 = c5 - rand; else c5 = c5 / 2;
                if (c6 > rand) c6 = c6 - rand; else c6 = c6 / 2;

                char cc1 = Convert.ToChar(c1); char cc2 = Convert.ToChar(c2); char cc3 = Convert.ToChar(c3);
                char cc4 = Convert.ToChar(c4); char cc5 = Convert.ToChar(c5); char cc6 = Convert.ToChar(c6);

                if (y == 1)
                    retorno = "$SP," + cc1 + cc2 + cc3 + "," + cc4 + cc5 + cc6 + "," + MU_value.speedunit + "," + MU_value.distunit + now + "*" + checksum;
                else if (y == 2)
                    retorno = "$SP," + speed + "," + cc4 + cc5 + cc6 + "," + MU_value.speedunit + "," + MU_value.distunit + now + "*" + checksum;
                else if (y == 3)
                    retorno = "$SP," + cc1 + cc2 + cc3 + "," + distance + "," + MU_value.speedunit + "," + MU_value.distunit +  now + "*" + checksum;*/
                #endregion
                //}
            }
            else if (Modo_MM == (MM)0)
            {
                int oi = random.Next(900, 1001);
                string temp = "DM,S," + distance + "," + MU_value.distunit + ",0,4-" + oi + now;
                //if (!ruido)
                //{
                retorno = "$" + temp + "*" + Conc_checksum(temp);
                //}
                //else
                //{
                //    retorno = "$" + temp + "*" + "1234";
                //}
            }

            return AplicaInterferencia(retorno);
        }

        private string AplicaInterferencia(string entrada)
        {
            if (!_interferencia)
            {
                return entrada;
            }
            else
            {
                if (_modoInterferencia == InterferenceMode.Manual)
                {
                    return _stringDeInterferencia;
                }
                else if (_modoInterferencia == InterferenceMode.Automatico)
                {
                    byte[] b = Encoding.Default.GetBytes(entrada);
                    Random r = new Random(DateTime.Now.Millisecond);
                    byte[] inter = new byte[b.Length];
                    r.NextBytes(inter);
                    int numberOfCharacters = r.Next(1, b.Length);

                    for (int y = 0; y < numberOfCharacters; y++)
                    {
                        b[r.Next(0, b.Length - 1)] = inter[r.Next(0, b.Length - 1)];
                    }

                    return Encoding.Default.GetString(b);
                }
                else //Checksum
                {

                    int b = BitConverter.ToInt16(Encoding.Default.GetBytes(entrada), 0) + 1;
                    return entrada.Substring(0, entrada.Length - 4) + BitConverter.ToString(BitConverter.GetBytes(b)).Replace("-", string.Empty).Substring(0, 4);                   
                }
            }
        }

        private List<string> Gera_multi_strings()
        {
            if (_direction == MultiStringsDirection.Both)
            {
                int directiontemp = 0;
                directiontemp = rf.Next(1, 100);
                if (directiontemp < 60)
                {
                    aproximando = false;
                }
                else
                {
                    aproximando = true;
                }
            }
            else if (_direction == MultiStringsDirection.Aproaching)
            {
                aproximando = true;
            }
            else
            {
                aproximando = false;
            }


            try
            {
                timesBetweenStrings.Clear();
            }
            catch (Exception ex)
            {
              
            }
            List<string> retorno = new List<string>();
            string now = string.Empty;
            Random random = new Random();
            bool range = true;
            int tempDec;
            int vel = random.Next(velmin, velmax);
            vel = vel * (-1);
            int dist;
            if (!Mdlimit)
            {
                dist = random.Next(distmin, distmax);
                if (dist == distmax)
                    range = false;
            }
            else
            {
                dist = MD_value.med;
            }

            string speed = vel.ToString();
            string distance = dist.ToString();
            #region gera_decimais
            string dec = string.Empty;
            if (!fixardecimal)
            {
                if (MU_value.decspeed > 0)
                {
                    if (MU_value.decspeed.Equals(1))
                    {
                        tempDec = random.Next(0, 9);
                        dec = tempDec.ToString();
                    }
                    else if (MU_value.decspeed.Equals(2))
                    {
                        tempDec = random.Next(0, 99);
                        if (tempDec < 10)
                            dec = "0" + tempDec.ToString();
                    }
                    else
                    {
                        tempDec = random.Next(0, 999);
                        if (tempDec < 10)
                            dec = "00" + tempDec.ToString();
                        else if (tempDec < 100)
                            dec = "0" + tempDec.ToString();
                        else
                            dec = tempDec.ToString();
                    }
                }

                if (MU_value.decdist > 0)
                {
                    if (MU_value.decdist.Equals(1))
                    {
                        tempDec = random.Next(0, 9);
                        dec = tempDec.ToString();
                    }
                    else if (MU_value.decdist.Equals(2))
                    {
                        tempDec = random.Next(0, 99);
                        if (tempDec < 10)
                            dec = "0" + tempDec.ToString();
                    }
                    else
                    {
                        tempDec = random.Next(0, 999);
                        if (tempDec < 10)
                            dec = "00" + tempDec.ToString();
                        else if (tempDec < 100)
                            dec = "0" + tempDec.ToString();
                        else
                            dec = tempDec.ToString();
                    }
                    if (range)
                    {
                        distance = dist.ToString() + "." + tempDec.ToString();
                    }
                    else
                    {
                        distance = dist.ToString() + ".0";
                    }
                }
            }
            else
            {
                if (MU_value.decdist.Equals(1))
                {
                    dec = decimalValue.ToString();
                }
                else if (MU_value.decdist.Equals(2))
                {
                    dec = decimalValue.ToString() + "0";
                }
                else
                {
                    dec = decimalValue.ToString() + "00";
                }
                distance = dist.ToString() + "." + dec;
            }
            #endregion

            if (DT)
            {
                now = "," + Gera_timesince();
            }

            if (Modo_MM == (MM)4)
            {
                string leitura;
                leitura = "SP," + speed + "," + distance + "," + MU_value.speedunit + "," + MU_value.distunit;
                int num_strings = random.Next(minstrings, maxstrings);
                int num_strings_above = 0;
                int num_strings_bellow = 0;

                if (num_strings > 1)
                {
                    num_strings_bellow = random.Next(minstrings, maxstrings) - 1;
                    num_strings_above = random.Next(0, maxstrings - num_strings_bellow);
                    if (num_strings_above > 2)
                    {
                        num_strings_above = 2;
                    }
                }

                num_strings = num_strings_bellow + num_strings_above + 1;
                List<int> timetemp = new List<int>();
                Random rnd = new Random();
                for (int z = 0; z < num_strings; z++)
                {
                    int sleep = rnd.Next(30, 100);
                    timetemp.Add(sleep);
                }

                decimal dist_calc = Convert.ToDecimal(distance, CultureInfo.CreateSpecificCulture("en-US"));
                decimal dist_first;
                decimal dist_int = Convert.ToDecimal(distance, CultureInfo.CreateSpecificCulture("en-US"));
                decimal velocidademetrosporsegundo = Decimal.Divide(Math.Abs(vel), 3.6m);
                decimal result;
                string strings_adicionais;

                //******************************************leituras com distância abaixo da desejada***********************************************
                List<string> rettemp = new List<string>();
                dist_first = dist_int;
                string datasincetemp = string.Empty;
                dist_first = dist_int;
                for (int num = num_strings_bellow; num > 0; num--)
                {
                    result = (decimal)(velocidademetrosporsegundo * timetemp[num - 1] / 1000);
                    dist_first = dist_first - result;
                    if (dist_first >= (MD_value.med + distinfms))
                    {
                        strings_adicionais = "SP," + speed + "," + dist_first.ToString("F1", CultureInfo.InvariantCulture) + "," + MU_value.speedunit + "," + MU_value.distunit;
                        rettemp.Add(strings_adicionais);
                        timesBetweenStrings.Add(timetemp[num]);
                    }
                }
                rettemp.Reverse();
                retorno = rettemp;


                //leitura na faixa desejada
                retorno.Add(leitura);
                timesBetweenStrings.Add(timetemp[num_strings_bellow]);
                decimal dist_post = dist_int;

                //leituras acima da faixa desejada
                for (int num = 0; num < num_strings_above; num++)
                {
                    dist_post = dist_post + (velocidademetrosporsegundo * timetemp[num_strings_bellow + num] / 1000);
                    if (dist_post <= (MD_value.med + distsupms))
                    {
                        strings_adicionais = "SP," + speed + "," + dist_post.ToString("F1", CultureInfo.InvariantCulture) + "," + MU_value.speedunit + "," + MU_value.distunit;
                        retorno.Add(strings_adicionais);
                        timesBetweenStrings.Add(timetemp[num_strings_bellow + 1 + num]);
                    }
                }


                List<string> stringcomplete = new List<string>();
                if (DT)
                {
                    dtpw = Convert.ToInt32(now.Replace(",", string.Empty).Replace(".", string.Empty));
                    datasincetemp = "," + dtpw.ToString().Substring(0, dtpw.ToString().Length - 2) + "." + dtpw.ToString().Substring(dtpw.ToString().Length - 2, 2);
                }

                if (aproximando)
                {
                    for (int z = 0; z < retorno.Count; z++)
                    {
                        retorno[z] = retorno[z].Replace("-", string.Empty);
                    }
                    retorno.Reverse();
                    timesBetweenStrings.Reverse();
                    List<string> ret = new List<string>();
                    List<int> timebetweentemp = new List<int>();
                    for (int z = 0; z < 3; z++)
                    {
                        ret.Add(retorno[z]);
                        timebetweentemp.Add(timesBetweenStrings[z]);
                        if (retorno[z].Equals(leitura.Replace("-", string.Empty)))
                        {
                            break;
                        }
                    }
                    retorno = ret;
                    timesBetweenStrings = timebetweentemp;
                }
                for (int z = 0; z < retorno.Count; z++)
                {
                    string temp = "$" + retorno[z] + datasincetemp + "*" + Conc_checksum(retorno[z] + datasincetemp);

                    stringcomplete.Add(AplicaInterferencia(temp));                  

                    if (DT)
                    {
                        dtpw = dtpw + timesBetweenStrings[z];
                        datasincetemp = "," + dtpw.ToString().Substring(0, dtpw.ToString().Length - 2) + "." + dtpw.ToString().Substring(dtpw.ToString().Length - 2, 2);
                    }
                }

                retorno = stringcomplete;
            }
            return retorno;
        }

        private List<string> Gera_multi_strings_simulador()
        {
           aproximando = false;

            try
            {
                timesBetweenStrings.Clear();
            }
            catch (Exception ex)
            {
                
            }
            List<string> retorno = new List<string>();
            string now = string.Empty;
            Random random = new Random();
           
            int tempDec;
            int vel = random.Next(velmin, velmax);

            int dist;
            dist = 500 - (vel / 10 + 10);

            vel = vel * (-1);

            string speed = vel.ToString();
            string distance = dist.ToString();

            #region gera_decimais
            string dec = string.Empty;
            if (!fixardecimal)
            {
                if (MU_value.decspeed > 0)
                {
                    if (MU_value.decspeed.Equals(1))
                    {
                        tempDec = random.Next(0, 9);
                        dec = tempDec.ToString();
                    }
                    else if (MU_value.decspeed.Equals(2))
                    {
                        tempDec = random.Next(0, 99);
                        if (tempDec < 10)
                            dec = "0" + tempDec.ToString();
                    }
                    else
                    {
                        tempDec = random.Next(0, 999);
                        if (tempDec < 10)
                            dec = "00" + tempDec.ToString();
                        else if (tempDec < 100)
                            dec = "0" + tempDec.ToString();
                        else
                            dec = tempDec.ToString();
                    }
                }

                if (MU_value.decdist > 0)
                {
                    if (MU_value.decdist.Equals(1))
                    {
                        tempDec = random.Next(0, 9);
                        dec = tempDec.ToString();
                    }
                    else if (MU_value.decdist.Equals(2))
                    {
                        tempDec = random.Next(0, 99);
                        if (tempDec < 10)
                            dec = "0" + tempDec.ToString();
                    }
                    else
                    {
                        tempDec = random.Next(0, 999);
                        if (tempDec < 10)
                            dec = "00" + tempDec.ToString();
                        else if (tempDec < 100)
                            dec = "0" + tempDec.ToString();
                        else
                            dec = tempDec.ToString();
                    }                    
                    distance = dist.ToString() + "." + tempDec.ToString();
                   
                }
            }
            else
            {
                if (MU_value.decdist.Equals(1))
                {
                    dec = decimalValue.ToString();
                }
                else if (MU_value.decdist.Equals(2))
                {
                    dec = decimalValue.ToString() + "0";
                }
                else
                {
                    dec = decimalValue.ToString() + "00";
                }
                distance = dist.ToString() + "." + dec;
            }
            #endregion

            if (DT)
            {
                now = "," + Gera_timesince();
            }

            if (Modo_MM == (MM)4)
            {
                decimal dist_calc = Convert.ToDecimal(distance, CultureInfo.CreateSpecificCulture("en-US"));              
                decimal velocidademetrosporsegundo = Decimal.Divide(Math.Abs(vel), 3.6m);
                decimal result;

                string leitura = "SP," + speed + "," + distance + "," + MU_value.speedunit + "," + MU_value.distunit;


                List<int> timetemp = new List<int>();
                Random rnd = new Random();
                for (int z = 0; z < 35; z++)
                {                       
                    retorno.Add(leitura);
                    timetemp.Add(40);
                    timesBetweenStrings.Add(40);
                    result = (decimal)(velocidademetrosporsegundo * 40 / 1000);
                    dist_calc = dist_calc + result;
                    leitura = "SP," + speed + "," + dist_calc.ToString("F1", CultureInfo.InvariantCulture) + "," + MU_value.speedunit + "," + MU_value.distunit;
                }   
                
                string datasincetemp = string.Empty;

                List<string> stringcomplete = new List<string>();
                if (DT)
                {
                    dtpw = Convert.ToInt32(now.Replace(",", string.Empty).Replace(".", string.Empty));
                    datasincetemp = "," + dtpw.ToString().Substring(0, dtpw.ToString().Length - 2) + "." + dtpw.ToString().Substring(dtpw.ToString().Length - 2, 2);
                }

                for (int z = 0; z < retorno.Count; z++)
                {

                    stringcomplete.Add(AplicaInterferencia("$" + retorno[z] + datasincetemp + "*" + Conc_checksum(retorno[z] + datasincetemp)));

                    if (DT)
                    {
                        dtpw = dtpw + timesBetweenStrings[z];
                        datasincetemp = "," + dtpw.ToString().Substring(0, dtpw.ToString().Length - 2) + "." + dtpw.ToString().Substring(dtpw.ToString().Length - 2, 2);
                    }
                }
                retorno = stringcomplete;
            }
            return retorno;
        }

        private string Gera_timesince()
        {
            string retorno = string.Empty;
            DateTime date = System.DateTime.Now;
            int data = Convert.ToInt32((date.Subtract(timesince).TotalMilliseconds));
            //retorno = data.ToString().Substring(0, data.ToString().Length - 3) + "." + data.ToString().Substring(data.ToString().Length - 2, 2);
            retorno = data.ToString().Substring(0, data.ToString().Length - 2) + "." + data.ToString().Substring(data.ToString().Length - 2, 2);
            return retorno;
        }

        public void save_parameters()
        {
            Program.parametros.DB = DB;
            Program.parametros.DT = DT;
            Program.parametros.DE = DE;
            Program.parametros.MA = MA;
            Program.parametros.Modo_CM = Modo_CM;
            Program.parametros.Modo_MM = Modo_MM;
            Program.parametros.Modo_DM = Modo_DM;
            Program.parametros.SL_limit = SL_limit;
            Program.parametros.SW_value.sw_0 = SW_value.sw_0;
            Program.parametros.SW_value.sw_1 = SW_value.sw_1;
            Program.parametros.MD_value.max = MD_value.max;
            Program.parametros.MD_value.med = MD_value.med;
            Program.parametros.MD_value.min = MD_value.min;
            Program.parametros.NX = NX;
            Program.parametros.SN = SN;
            _serialSensorPlaca = SN;
            Program.parametros.MU_value = MU_value;
            Program.parametros.HT = HT;
            Program.parametros.CaptureSpeed = _velocidadeDeCaptura;
            Program.parametros.IS = ISError;
            Program.parametros.ISERRORMessage = ISERRORMessage.ToString();
            Program.parametros.ISRANDOM = ISRandom;
        }

        public void save_file(string filename)
        {
            save_parameters();

            string save = "DB " + DB.ToString() + ";\r\n" + "DT " + DT.ToString() + ";\r\n" + "DE " + DE.ToString() + ";\r\n" + "MA " + MA.ToString() + ";\r\n" +
                           "MM " + Modo_MM.ToString() + ";\r\n" + "DM " + Modo_DM.ToString() + ";\r\n" + "SL " + SL_limit.ToString() + ";\r\n" +
                           "SW " + SW_value.sw_0.ToString() + ", " + SW_value.sw_1.ToString() + ";\r\n" + "MD " + MD_value.min.ToString() + ", "
                           + MD_value.max.ToString() + ", " + MD_value.med.ToString() + ";\r\n" + "NX " + NX.ToString() + ";\r\n" + "SN " + SN + ";\r\n"
                           + "MU " + MU_value.distunit + ", " + MU_value.decdist + ", " + MU_value.speedunit + ", " + MU_value.decspeed + ";\r\n" + "HT " + HT + ";\r\n" +
                           "VMin " + velmin.ToString() + ";\r\n" + "VMax " + velmax.ToString() + ";\r\n" + "Dmin " + distmin.ToString() + ";\r\n" + "Dmax " + distmax.ToString() + ";\r\n" +
                           "MinLeituras " + minstrings + ";\r\n" + "MaxLeituras " + maxstrings + ";\r\n" + "DistInf " + distinfms.ToString() + ";\r\n" + "DistSup " + distsupms.ToString() + ";\r\n" +
                           "TempoMinStrings " + tempostringsmin + ";\r\n" + "TempoMaxStrings " + tempostringsmax + ";\r\n" + "FixarDecimal " + fixardecimal.ToString() + ";\r\n" + "ValordoDecimal " + decimalValue.ToString() + ";\r\n" +
                           "Tempo " + time + ";\r\n" + "Inibe " + inibe + ";\r\n" + "Fixar " + mdlimit + ";\r\n" + "Multistrings " + multistrings + ";\r\n" + "Direction " + Direction + ";\r\n" + "IntervaloVariavel " + _variableTime + ";\r\n" +
                            "SimulaErros " + _simulaErros + ";\r\n" + "ErroBoot " + _PDError + ";\r\nHabilitaBoard " + _board + ";\r\nVelocidadeCaptura " + _velocidadeDeCaptura + ";\r\nModo Teste " + _modoTeste.ToString() + 
                            ";\r\nModo Simulador " + _modoSimulador.ToString() + ";\r\nInterferencia " + _interferencia.ToString() + ";\r\nModo Interferencia " + _modoInterferencia.ToString() + 
                            ";\r\nInterferencia Manual "  + _stringDeInterferencia + ";\r\n" + "ID " + ID + ";\r\nModelo Camera " + Program._currentCameraModel + ";\r\nFwVCamera " + Program.version + ";\r\nsenhaCamera  " + Program.senha + 
                            ";\r\nsenhaAPI " + Program.senhaAPI + ";\r\n" + "HasDS " + Program.HasDS + ";\r\n" + "DSDefault " + Program.DSDefault + ";\r\n" + "IS " + ISError.ToString() + ";\r\n" +
                            "ISERRORMessage " + ISERRORMessage.ToString() + ";\r\n" + "ISRandom " + ISRandom.ToString() + ";";

            if (!Directory.Exists(atbLog))
            {
                Directory.CreateDirectory(atbLog);
            }
            atbFileStream = new FileStream(filename, FileMode.Create);

            byte[] b = new byte[save.Length];
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            b = encoding.GetBytes(save);
            atbFileStream.Write(b, 0, b.Length);
            atbFileStream.Flush();
            atbFileStream.Close();
            atbFileStream.Dispose();
        }

        public void load_file(string filename)
        {
            string arquivo = string.Empty;
            string param = string.Empty;
            int index;
            int index_final;
            bool erro = false;

            //if (!File.Exists(atbLog + "parameters" + ".tcc"))
            if (!File.Exists(filename))
            {
                throw new Exception("Não existe nenhuma configuração salva");
            }
            else
            {
                StreamReader parameter = null;
                try
                {
                    parameter = new StreamReader(filename);
                    ArrayList arrText = new ArrayList();
                    arquivo = parameter.ReadToEnd();
                    //carrega DB
                    try
                    {
                        param = ReadParameter("DB", arquivo);
                        Program.parametros.DB = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        param = ReadParameter("DT", arquivo);
                        Program.parametros.DT = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        param = ReadParameter("DE", arquivo);
                        Program.parametros.DE = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        param = ReadParameter("MA", arquivo);
                        Program.parametros.MA = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }


                    //carrega MM
                    index = arquivo.IndexOf("MM ") + 3;
                    if (index > 2)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        if (index_final > -1)
                        {
                            param = arquivo.Substring(index, index_final - index);
                            Program.parametros.Modo_MM = (MM)Enum.Parse(typeof(MM), param);
                        }
                    }
                    //fim carrega MM

                    //carrega DM
                    index = arquivo.IndexOf("DM ") + 3;
                    if (index > 2)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        if (index_final > -1)
                        {
                            param = arquivo.Substring(index, index_final - index);
                            Program.parametros.Modo_DM = (DM)Enum.Parse(typeof(DM), param);
                        }
                    }
                    //fim carrega DM

                    //carrega SL
                    index = arquivo.IndexOf("SL ") + 3;
                    if (index > 2)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        if (index_final > -1)
                        {
                            index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                            Program.parametros.SL_limit = index;
                        }
                    }
                    //fim carrega SL

                    //carrega SW
                    index = arquivo.IndexOf("SW ") + 3;
                    if (index > 2)
                    {
                        //ahhh tá bom assim, achou o SW tá valendo
                        index_final = arquivo.IndexOf(",", index);
                        index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                        Program.parametros.SW_value.sw_0 = index;
                        index = index_final + 2;
                        index_final = arquivo.IndexOf(";", index);
                        index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                        Program.parametros.SW_value.sw_1 = index;
                    }
                    //fim carrega SW

                    //carrega MD
                    index = arquivo.IndexOf("MD ") + 3;
                    if (index > 2)
                    {
                        // tratamento 1/2 boca
                        index_final = arquivo.IndexOf(",", index);
                        index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                        Program.parametros.MD_value.min = index;
                        index = index_final + 2;
                        index_final = arquivo.IndexOf(",", index);
                        index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                        Program.parametros.MD_value.max = index;
                        index = index_final + 2;
                        index_final = arquivo.IndexOf(";", index);
                        index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                        Program.parametros.MD_value.med = index;
                    }
                    //fim carrega MD


                    //carrega NX
                    index = arquivo.IndexOf("NX ") + 3;
                    if (index > 2)
                    {
                        param = arquivo.Substring(index, 5);
                        param = param.Replace(";", string.Empty);
                        Program.parametros.NX = Convert.ToBoolean(param);
                    }
                    //fim carrega NX

                    //carrega SN
                    index = arquivo.IndexOf("SN ") + 3;
                    if (index > 2)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        Program.parametros.SN = param;
                        _serialSensorPlaca = param;
                    }
                    //fim carrega SN


                    //carrega MU
                    index = arquivo.IndexOf("MU ") + 3;
                    if (index > 2)
                    {
                        index_final = arquivo.IndexOf(",", index);
                        param = arquivo.Substring(index, index_final - index);
                        Program.parametros.MU_value.distunit = (UNIT)Enum.Parse(typeof(UNIT), param); ;
                        index = index_final + 2;
                        index_final = arquivo.IndexOf(",", index);
                        index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                        Program.parametros.MU_value.decdist = index;
                        index = index_final + 2;
                        index_final = arquivo.IndexOf(",", index);
                        param = arquivo.Substring(index, index_final - index);
                        Program.parametros.MU_value.speedunit = param;
                        index = index_final + 2;
                        index_final = arquivo.IndexOf(";", index);
                        index = Convert.ToInt32(arquivo.Substring(index, index_final - index));
                        Program.parametros.MU_value.decspeed = index;
                    }
                    //fim carrega MU

                    //carrega HT
                    index = arquivo.IndexOf("HT ") + 3;
                    if (index > 2)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        Program.parametros.HT = Convert.ToInt32(param);
                    }
                    //fim carrega HT

                    //carrega Velocidade Minima
                    index = arquivo.IndexOf("VMin ") + 5;
                    if (index > 4)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        velmin = Convert.ToInt32(param);
                    }
                    //fim carrega Velocidade Minima

                    //carrega Velocidade Maxima
                    index = arquivo.IndexOf("VMax ") + 5;
                    if (index > 4)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        velmax = Convert.ToInt32(param);
                    }
                    //fim carrega Velocidade Maxima

                    //carrega Distancia Minima
                    index = arquivo.IndexOf("Dmin ") + 5;
                    if (index > 4)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        distmin = Convert.ToInt32(param);
                    }
                    //fim carrega Distancia Minima

                    //carrega Distancia Maxima
                    index = arquivo.IndexOf("Dmax ") + 5;
                    if (index > 4)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        distmax = Convert.ToInt32(param);
                    }
                    //fim carrega Distancia Maxima

                    //carrega Minimo de leituras
                    index = arquivo.IndexOf("MinLeituras ") + 12;
                    if (index > 11)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        minstrings = Convert.ToInt32(param);
                    }
                    //fim carrega Minimo de leituras

                    //carrega Maximo de leituras
                    index = arquivo.IndexOf("MaxLeituras ") + 12;
                    if (index > 11)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        maxstrings = Convert.ToInt32(param);
                    }
                    //fim carrega Maximo de leituras

                    //carrega Distancia inferior multiplas strings
                    index = arquivo.IndexOf("DistInf ") + 8;
                    if (index > 7)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        distinfms = Convert.ToInt32(param);
                    }
                    //fim carrega Distancia inferior multiplas strings


                    //carrega Distancia superior multiplas strings
                    index = arquivo.IndexOf("DistSup ") + 8;
                    if (index > 7)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        distsupms = Convert.ToInt32(param);
                    }
                    //fim carrega Distancia superior multiplas strings


                    //carrega tempo minimo entre strings
                    index = arquivo.IndexOf("TempoMinStrings ") + 16;
                    if (index > 15)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        tempostringsmin = Convert.ToInt32(param);
                    }
                    //fim carrega tempo minimo entre strings

                    //carrega tempo máximo entre strings
                    index = arquivo.IndexOf("TempoMaxStrings ") + 16;
                    if (index > 15)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        tempostringsmax = Convert.ToInt32(param);
                    }
                    //fim carrega tempo máximo entre strings

                    //carrega FixarDecimal
                    index = arquivo.IndexOf("FixarDecimal ") + 13;
                    if (index > 12)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        fixardecimal = Convert.ToBoolean(param);
                    }
                    //fim carrega FixarDecimal

                    //carrega ValordoDecimal
                    index = arquivo.IndexOf("ValordoDecimal ") + 15;
                    if (index > 14)
                    {
                        index_final = arquivo.IndexOf(";", index);
                        param = arquivo.Substring(index, index_final - index);
                        decimalValue = Convert.ToInt32(param);
                    }
                    //fim carrega ValordoDecimal

                    //"Tempo " +  time + ";\r\n" + "Inibe " + inibe + ";\r\n" + "Fixar " + mdlimit + ";\r\n" + "Multistrings " + multistrings + ";\r\n" + "Direction " + Direction + ";";

                    //carrega Tempo
                    index = arquivo.IndexOf("Tempo ") + 6;
                    if (index > 5)
                    {
                        int ind = arquivo.IndexOf(";", index);
                        int tamanho = ind - index;
                        param = arquivo.Substring(index, tamanho);
                        time = Convert.ToInt32(param);
                    }
                    //fim carrega Tempo

                    //carrega inibe
                    index = arquivo.IndexOf("Inibe ") + 6;
                    if (index > 5)
                    {
                        param = arquivo.Substring(index, 5);
                        param = param.Replace(";", string.Empty);
                        inibe = Convert.ToBoolean(param);
                    }
                    //fim carrega inibe

                    //carrega FIXAR
                    index = arquivo.IndexOf("Fixar ") + 6;
                    if (index > 5)
                    {
                        param = arquivo.Substring(index, 5);
                        param = param.Replace(";", string.Empty);
                        mdlimit = Convert.ToBoolean(param);
                    }
                    //fim carrega FIXAR

                    //carrega MULTISTRINGS
                    index = arquivo.IndexOf("Multistrings ") + 13;
                    if (index > 12)
                    {
                        param = arquivo.Substring(index, 5);
                        param = param.Replace(";", string.Empty);
                        multistrings = Convert.ToBoolean(param);
                    }
                    //fim carrega MULTISTRINGS

                    //carrega MULTISTRINGS
                    index = arquivo.IndexOf("Direction ") + 10;
                    if (index > 9)
                    {
                        int ind = arquivo.IndexOf(";", index);
                        int tamanho = ind - index;
                        param = arquivo.Substring(index, tamanho);
                        param = param.Replace(";", string.Empty);
                        Direction = (MultiStringsDirection)Enum.Parse(typeof(MultiStringsDirection), param);
                    }
                    //fim carrega MULTISTRINGS

                    //carrega IntervaloVariavel
                    index = arquivo.IndexOf("IntervaloVariavel ") + 18;
                    if (index > 17)
                    {
                        int ind = arquivo.IndexOf(";", index);
                        int tamanho = ind - index;
                        param = arquivo.Substring(index, tamanho);
                        param = param.Replace(";", string.Empty);
                        _variableTime = Convert.ToBoolean(param);
                    }
                    //fim carrega IntervaloVariavel

                    //carrega IntervaloVariavel
                    index = arquivo.IndexOf("SimulaErros ") + 12;
                    if (index > 11)
                    {
                        int ind = arquivo.IndexOf(";", index);
                        int tamanho = ind - index;
                        param = arquivo.Substring(index, tamanho);
                        param = param.Replace(";", string.Empty);
                        _simulaErros = Convert.ToBoolean(param);
                    }
                    //fim carrega IntervaloVariavel

                    //carrega ErroBoot
                    index = arquivo.IndexOf("ErroBoot ") + 9;
                    if (index > 8)
                    {
                        int ind = arquivo.IndexOf(";", index);
                        int tamanho = ind - index;
                        param = arquivo.Substring(index, tamanho);
                        param = param.Replace(";", string.Empty);
                        _PDError = Convert.ToBoolean(param);
                    }
                    //fim carrega ErroBoot 

                    //carrega Habilita Board
                    try
                    {
                        param = ReadParameter("HabilitaBoard", arquivo);
                        _board = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }
                    //fim carrega Habilita Board                  

                    //carrega Velocidade Captura
                    try
                    {
                        param = ReadParameter("VelocidadeCaptura", arquivo);
                        Program.parametros.CaptureSpeed = Convert.ToInt32(param);
                    }
                    catch (Exception)
                    {
                    }
                    //fim Velocidade Captura  

                    //carrega Modo Teste
                    try
                    {
                        param = ReadParameter("Modo Teste", arquivo);
                        _modoTeste = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }
                    //fim carrega Modo Teste

                    //carrega Modo Simulador
                    try
                    {
                        param = ReadParameter("Modo Simulador", arquivo);
                        _modoSimulador = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }
                    //fim carrega Modo Simulador

                    try
                    {
                        param = ReadParameter("Interferencia", arquivo);
                        _interferencia = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        param = ReadParameter("Modo Interferencia", arquivo);
                        _modoInterferencia = (InterferenceMode)Enum.Parse(typeof(InterferenceMode), param);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        param = ReadParameter("Interferencia Manual", arquivo);
                        _stringDeInterferencia = param;
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        param = ReadParameter("ID", arquivo);
                        ID = param;
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        param = ReadParameter("Modelo Camera", arquivo);
                        Program._currentCameraModel = param;
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        param = ReadParameter("FwVCamera", arquivo);
                        Program.version = param;
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        param = ReadParameter("senhaCamera", arquivo);
                        Program.senha = param;
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        param = ReadParameter("senhaAPI", arquivo);
                        Program.senhaAPI = Convert.ToBoolean(param);
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        param = ReadParameter("HasDS", arquivo);
                        Program.HasDS = Convert.ToBoolean(param);

                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        param = ReadParameter("DSDefault", arquivo);
                        Program.DSDefault = Convert.ToBoolean(param);

                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        param = ReadParameter("IS", arquivo);
                        Program.parametros.IS = Convert.ToBoolean(param);
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        param = ReadParameter("ISRandom", arquivo);
                        Program.parametros.ISRANDOM = Convert.ToBoolean(param);
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        param = ReadParameter("ISERRORMessage", arquivo);
                        Program.parametros.ISERRORMessage = param;
                    }
                    catch (Exception ex)
                    {

                    }

                    load_parameters();
                }
                catch (Exception ex)
                {
                    erro = true;
                }
                finally
                {
                    if (parameter != null)
                    {
                        parameter.Close();
                    }
                    if (erro)
                    {
                        throw new Exception("Falha ao ler arquivo de configurações, o arquivo pode ter sido lido parcialmente devido à versão incompatível ou arquivo adulterado ou corrompido");
                    }
                }
            }
        }

        private string ReadParameter(string parameter, string arquivo)
        {
            string param = string.Empty;
            int index = arquivo.IndexOf(parameter + " ") + parameter.Length + 1;
            if (index > parameter.Length)
            {
                int ind = arquivo.IndexOf(";", index);
                int tamanho = ind - index;
                param = arquivo.Substring(index, tamanho);
                param = param.Replace(";", string.Empty);
            }

            if (string.IsNullOrEmpty(param))
            {
                throw new Exception("Parameter not found");
            }

            return param;
        }

        public void load_parameters()
        {
            DB = Program.parametros.DB;
            DT = Program.parametros.DT;
            DE = Program.parametros.DE;
            MA = Program.parametros.MA;
            Modo_CM = Program.parametros.Modo_CM;
            Modo_MM = Program.parametros.Modo_MM;
            Modo_DM = Program.parametros.Modo_DM;
            SL_limit = Program.parametros.SL_limit;
            SW_value.sw_0 = Program.parametros.SW_value.sw_0;
            SW_value.sw_1 = Program.parametros.SW_value.sw_1;
            MD_value.max = Program.parametros.MD_value.max;
            MD_value.med = Program.parametros.MD_value.med;
            MD_value.min = Program.parametros.MD_value.min;
            NX = Program.parametros.NX;
            SN = Program.parametros.SN;
            _serialSensorPlaca = Program.parametros.SN;
            MU_value = Program.parametros.MU_value;
            HT = Program.parametros.HT;
            _velocidadeDeCaptura = Program.parametros.CaptureSpeed;
            ISError = Program.parametros.IS;
            ISERRORMessage = (ERROR)Enum.Parse(typeof(ERROR),Program.parametros.ISERRORMessage);
            ISRandom = Program.parametros.ISRANDOM;
        }

        public static byte[] StringToHexByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);

            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));

            return sb.ToString().ToUpper();
        }

        private void FactoryDefault()
        {
            DB = true;
            MA = false;
            DT = true;
            Modo_DM = DM.STRONGEST_TARGET_ONLY;
            PW = false;
            SL_limit = 0;
            MD_value.min = 15;
            MD_value.max = 70;
            MD_value.med = 30;
            NX = false;
            HT = 450;
            Modo_CM = CM.TruCapture;
            Modo_MM = (MM)4;
        }


        public enum CM
        {
            Truspeed = 0,
            TruCapture = 1,
            Trumonitor = 2
        }

        public enum MM
        {
            standard_mode = 0,
            extended_range_mode = 1,
            intelligent_long_range_mode = 2,
            reserved_for_short_range_high_accuracy_mode = 3,
            speed_reading_mode = 4
        }

        public enum ERROR
        {
            // _ SUBSTITUI PO ESPACO
            // __ (DOIS ESPACOS) SUBSTITUI POR : (DOIS PONTOS + ESPAÇO)
            // ___(TRES) SUBSTITUI POR /
            NO_TARGET = 01,
            DATA_INSUFFICIENT_ = 02,
            DATA_UNSTABLE = 03,
            JAM_DETECTED = 07,
            WRONG_DIRECTION = 08,
            RANGE_ERROR = 09,
            INVALID_COMMAND = 20,
            SYNTAX_ERROR = 22,
            OUT_OF_RANGE = 23,
            INCORRECT_PASSWORD = 24,
            AUTHORIZATION_REQUIRED = 25,
            ADRESS_NOT_SET = 26,
            INVALID_HEX_DIGIT = 27,
            INVALID_ADRESS_ACCESS = 28,
            INVALID_ADRESS = 29,
            FLASH_PROGRAMMING = 30,
            FLASH_UPDATE_NOT_ALLOWED = 31,
            TRY_LATER__NOT_READY_TO_EXEC = 32,
            CODE_PROTECTED = 33,
            NOT_ALLOW_COMMAND = 34,
            INVALID_PARAMETER = 35,
            FAILED_EXECUTION = 36,
            INCORRECT_CHECKSUM = 37,
            INVALID_HARDWARE_CONFIGURATION = 38,
            NO_LICENSE__CALL_LTI = 39,
            TOO_COLD = 52,
            TOO_HOT = 53,
            LOW_BATTERY = 54,
            SPAN_ERROR = 56,
            TILT_SENSOR_ERROR = 57,
            ADC___DAC_ERROR = 58,
            RX_CAL_ERROR = 59,
            STACK_OVERFLOW = 60,
            APD_FAILED = 62,
            FLASH_MEMORY__CAL = 63,
            FLASH_MEMORY__SYS1 = 64,
            FLASH_MEMORY__SYS2 = 65,
            FLASH_MEMORY__USER = 66,
            FLASH_MEMORY__CODE = 67,
            HV_TX_FAILED = 68,
            TX_REFERENCE_TIMING = 69,
            HV_RX_FAILED = 70,
            UNDEFINED_ERROR = 100
        }

        public struct SW
        {
            public int sw_0;
            public int sw_1;
        }

        public struct MD
        {
            public int min;
            public int max;
            public int med;
        }

        public enum DM
        {
            NO_OUTPUT = 0,
            INTERNAL_RANGE_DATA = 1,
            FIRST_TARGET_ONLY = 2,//$DM,F,<distance>,<unit for distance>,<error code>,<signal strength>,<time since power on> *CRC16
            STRONGEST_TARGET_ONLY = 3,//$DM,S,<distance>,<unit for distance>,<error code>,<signal strength>,<time since power on> *CRC16
            LAST_FARTHEST_TARGET_ONLY = 4, //$DM,L,<distance>,<unit for distance>,<error code>,< signal strength>,<time since power on> *CRC16
            FIRST_SECOND_THIRD_TARGETS = 5, //$DM,F3,<distance for 1st>,<distance for 2nd>,<distance for 3rd>,<unit for distance>,<error code>,<time since power on> *CRC16
            LAST_TWO_FARTHEST_SECOND_TO_FARTHEST_TARGETS = 6,//$DM,L2,<distance for last>,<distance for second to last>,<unit for distance>,<error code>,<time since power on> *CRC16
            FIRST_STRONGEST_LAST_TARGETS_DEFAULT = 7, //$DM,A,<distance for first>,<distance for strongest>,<distance for last>,<unit for distance>,<error code>,<time since power on> *CRC16
            FIRST_SECOND_THIRD_STRONGEST_LAST_TARGETS = 8, //$DM,B,<distance for first>,<distance for second>,<distance for third>,<distance for strongest>,<distance for last>,<unit for distance>,<error code>,<time since power on> *CRC16
            ALIGNMENT = 9 //$DM,I,<calibration type>-<index>,<distance>,<distance without distance offset>,<unit for distance>,<time since power on> *CRC16           
        }

        public enum UNIT
        {
            F = 0,
            M = 1,
            N = 2,
            E = 3
        }

        public struct MU
        {
            public UNIT distunit;
            public int decdist;
            public string speedunit;
            public int decspeed;
        }

        public struct parameters
        {
            public bool DB;
            public bool DT;
            public bool DE;
            public bool MA;
            public CM Modo_CM;
            public MM Modo_MM;
            public DM Modo_DM;
            public int SL_limit;
            public SW SW_value;
            public MD MD_value;
            public bool NX;
            public string SN;
            public MU MU_value;
            public int HT;
            public int CaptureSpeed;
            public bool IS;
            public bool ISRANDOM;
            public string ISERRORMessage;
        }

        public enum MultiStringsDirection
        {
            Departing = 0,
            Aproaching = 1,
            Both = 2
        }

        public enum OperationMode
        {
            Nenhum,
            Normal,
            Ajuste,
            Manutenção,
            Teste
        }

        public enum DG_mode
        {
            unlocked = 0,
            undefined = 1,
            locked = 10,
            soft_locked = 11
        }

        public enum AccessLevel
        {
            None,
            Level1,
            Level2,
            FullAccess
        }

        public enum InterferenceMode
        {           
            Automatico,
            Manual,
            Checksum
        }

        public enum Sector4Type
        {
            FIRST_lowTemperature = 0,
            SECOND_highTemperature_wrongCapture = 1,
            CURRENT_temperature_and_capture = 2
        }
    }
}
