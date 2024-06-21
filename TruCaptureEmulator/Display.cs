//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Net;
//using System.Threading;


//namespace TruCaptureEmulator
//{
//    public class Display
//    {
//        #region atributos
//        private string IP;
//        private int porta;
//        private string endereco;
//        private IPEndPoint ipEp;
//        private string SSID;
//        private string password;
//        private Socket_Serial s;
//        private bool is_running;
//        private int timereconect = 2000;
//        private int timeoutresposta = 600;
//        private bool log_checked = false;
//        //private Log log;
//        private bool ADHOC = true;
//        private OperationMode mode = OperationMode.SPEED;
//        private int speed;
//        private Period period = new Period();
//        private int count_fault = 0;
//        private bool send = false;

//        public Period Interval
//        {
//            get { return period; }
//            set
//            {
//                if (value.begin > value.end)
//                {
//                    period = value;
//                }
//                else
//                {
//                    throw new Exception("O Valor de Início deve ser maior que o valor de Término");
//                }
//            }
//        }

//        public OperationMode Mode
//        {
//            get { return mode; }
//            set { mode = value; }
//        }
//        public int Speed
//        {
//            get { return speed; }
//            set { speed = value; }
//        }
//        #endregion

//        #region variáveis, Threads e delegates
//        private Thread t, c;
//        private DateTime hora;
//        private DateTime resp;
//        private int intervalo = 10;
//        private bool resposta = true;
//        public delegate void Exceptions_Display(Object sender, string text);
//        public event Exceptions_Display exceptions_display;
//        public delegate void Read_Display(Object sender, string text);
//        public event Read_Display read_display;
//        private Checksum_display checksum = new Checksum_display();
//        public bool Is_running
//        {
//            get { return is_running; }
//        }
//        #endregion

//        #region Construtores
//        public Display(string IP, int porta, string SSID, string password, int speed)
//        {
//            try
//            {
//                ipEp = new IPEndPoint(IPAddress.Parse(IP), porta);
//                this.IP = IP;
//                this.porta = porta;
//                this.SSID = SSID;
//                this.password = password;
//                this.speed = speed;
//                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
//                period.end = new DateTime(1, 1, 1, 6, 0, 0);
//                s = new Socket_Serial(ipEp, SSID, password);
//                t = new Thread(new ThreadStart(Stay_Connected));
//                t.Priority = ThreadPriority.Lowest;
//                t.IsBackground = true;
//                c = new Thread(new ThreadStart(Connect));
//                c.Priority = ThreadPriority.Lowest;
//                c.IsBackground = true;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public Display(IPEndPoint ipEp, string SSID, string password, int speed)
//        {
//            try
//            {
//                this.ipEp = ipEp;
//                this.SSID = SSID;
//                this.password = password;
//                this.speed = speed;
//                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
//                period.end = new DateTime(1, 1, 1, 6, 0, 0);
//                s = new Socket_Serial(ipEp, SSID, password);

//                t = new Thread(new ThreadStart(Stay_Connected));
//                t.Priority = ThreadPriority.Lowest;
//                t.IsBackground = true;

//                c = new Thread(new ThreadStart(Connect));
//                c.Priority = ThreadPriority.Lowest;
//                c.IsBackground = true;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public Display(IPEndPoint ipEp, int speed)
//        {
//            try
//            {
//                this.ipEp = ipEp;
//                this.speed = speed;
//                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
//                period.end = new DateTime(1, 1, 1, 6, 0, 0);
//                s = new Socket_Serial(ipEp);
//                ADHOC = false;
//                t = new Thread(new ThreadStart(Stay_Connected));
//                t.Priority = ThreadPriority.Lowest;
//                t.IsBackground = true;
//                c = new Thread(new ThreadStart(Connect));
//                c.Priority = ThreadPriority.Lowest;
//                c.IsBackground = true;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public Display(string IP, int porta, int speed)
//        {
//            try
//            {
//                //ipEp.Address;
//                ipEp = new IPEndPoint(IPAddress.Parse(IP), porta);
//                this.speed = speed;
//                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
//                period.end = new DateTime(1, 1, 1, 6, 0, 0);
//                s = new Socket_Serial(ipEp);
//                ADHOC = false;
//                t = new Thread(new ThreadStart(Stay_Connected));
//                t.Priority = ThreadPriority.Lowest;
//                t.IsBackground = true;

//                c = new Thread(new ThreadStart(Connect));
//                c.Priority = ThreadPriority.Lowest;
//                c.IsBackground = true;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        #endregion

//        #region Métodos

//        /* public void SetLog(Log log)
//        {
//            this.log = log;
//            log_checked = true;
//        }*/

//        private void Connect()
//        {
//            int count = 0;
//            while (count < 3)
//            {
//                try
//                {
//                    resposta = true;
//                    t.Start();
//                    s.Open();
//                    is_running = true;
//                    s.leituradados += new Socket_Serial.Leitura_dados(Read);
//                    //s.exceptions += new Socket_Serial.Exceptions(Exceptions);
//                    //if (log_checked) log.WriteLogEvent("Display conectado", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Status);
//                    break;
//                }
//                catch (Exception ex)
//                {
//                    //if (log_checked) log.WriteLogEvent("Erro ao conectar display " + ex.Message, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                    count++;
//                    if (count < 3)
//                    {

//                        if (ADHOC)
//                        {
//                            s.Disconect();
//                            s = null;
//                            s = new Socket_Serial(ipEp, SSID, password);
//                            if (exceptions_display != null)
//                            {
//                                exceptions_display(this, "Houve uma falha ao tentar conectar com a rede " + SSID);
//                            }
//                        }
//                        else
//                        {
//                            s = null;
//                            s = new Socket_Serial(ipEp);
//                            try
//                            {
//                                t.Abort();
//                                t = null;
//                                t = new Thread(new ThreadStart(Stay_Connected));
//                                t.Priority = ThreadPriority.Lowest;
//                                t.IsBackground = true;
//                            }
//                            catch { }
//                        }
//                        Thread.Sleep(timereconect);
//                    }
//                    else
//                    {
//                        //if (log_checked) log.WriteLogEvent("Excedidas 3 tentativas de conexão" + ex.Message, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                        if (exceptions_display != null)
//                        {
//                            exceptions_display(this, "Excedidas 3 tentativas de conexão " + ex.Message);
//                        }
//                    }
//                }
//            }
//        }

//        public void Start()
//        {
//            try
//            {
//                c.Start();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public void Close()
//        {
//            try
//            {
//                s.Close();
//                is_running = false;
//                s.leituradados -= new Socket_Serial.Leitura_dados(Read);
//                //s.exceptions -= new Socket_Serial.Exceptions(Exceptions);
//                t.Abort();
//                //if (log_checked) log.WriteLogEvent("Display desconectado", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Status);
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }

//        public void Send(string texto)
//        {
//            //&<velocidade>,<distancia>,<bytecomando>*csum\r\n            
//            string[] palavra = new string[texto.Split(new char[] { ',' }).Length - 1];
//            string envio = string.Empty;
//            try
//            {
//                palavra = texto.Split(',');
//                palavra[1] = palavra[1].Replace("-", string.Empty);
//                byte[] control = new byte[1];
//                control[0] = Command_2(Convert.ToInt32(palavra[1]));
//                envio = "DL," + palavra[1].Replace("-", string.Empty) + "," + palavra[2] + ",";
//                byte[] temp = System.Text.Encoding.Default.GetBytes(envio);
//                byte[] temp2 = new byte[temp.Length + 1];
//                System.Array.Copy(temp, 0, temp2, 0, temp.Length);
//                System.Array.Copy(control, 0, temp2, temp.Length, 1);
//                byte b = checksum.ComputeChecksum_Display(temp2);
//                envio = "&" + Encoding.Default.GetString(temp2, 0, temp2.Length) + "*" + Encoding.Default.GetString(new byte[] { b }, 0, 1) + "\r\n";
//            }
//            catch (Exception ex)
//            {
//                //if (log_checked) log.WriteLogEvent("String de entrada incompatível com o formato adequado " + envio.Replace("\r\n", string.Empty), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                throw ex;
//            }

//            try
//            {
//                s.Send(envio);
//                send = true;
//                resposta = false;
//                //resp = DateTime.Now;
//                hora = DateTime.Now;
//            }
//            catch (Exception ex)
//            {
//                //if (log_checked) log.WriteLogEvent("Falha ao enviar string: " + envio.Replace("\r\n", string.Empty), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                throw ex;
//            }
//        }

//        private byte Command(int spd)
//        {
//            /*1111-1111
//             *  nibble + significativo
//             *  bit 0 (0001) -  nível 0 = menor luminosidade
//             *                  nível 1 = maior luminosidade
//             *  bit 1 (0010) -  nível 0 = distância
//             *                  nível 1 = velocidade
//             *  nible - significativo
//             *  bit 0 (0001) - nível 0 = luz vermelha desligada
//             *                 nível 1 = luz vermelha ligada
//             *  bit 1 (0010) - nível 0 = luz verde desligada
//             *                 nível 1 = luz verde ligada
//             */
//            byte retorno = 16;
//            bool lamp = false;
//            if (DateTime.Now.Hour > period.end.Hour && DateTime.Now.Hour < period.begin.Hour) retorno = 0;
//            //else retorno &= 239;
//            if (mode == OperationMode.SPEED)
//            {
//                retorno |= 32; // 0001 0000 - 0x10
//                lamp = true;
//            }
//            //else retorno &= 239;
//            if (lamp)
//            {
//                if (spd <= speed)                               // 0000 0010 - 0x02
//                {
//                    retorno |= 2;
//                    //retorno &= 254;
//                }
//                else
//                {
//                    retorno |= 1;
//                    //retorno &= 253;
//                }
//            }
//            return retorno;
//        }

//        private byte Command_2(int spd)
//        {
//            /*
//             * bit 0 - incrementa brilho (1)
//             * bit 1 - decrementa brilho (2)
//             * bit 2 - exibe velocidade (4)
//             * bit 3 - exibe distância (8)
//             * bit 4 - sinaliza vermelho(16)
//             * bit 5 - sinaliza verde (32)
//             * bit 6 - não usado
//             * bit 7 - sempre 1           
//            */
//            byte retorno = 0;
//            bool lamp = false;
//            if (DateTime.Now.Hour > period.end.Hour && DateTime.Now.Hour < period.begin.Hour)
//            {
//                retorno = 2;
//            }
//            else
//            {
//                retorno = 1;
//            }

//            if (mode == OperationMode.SPEED)
//            {
//                retorno |= 4; // 0000 0100 - 0x04
//                lamp = true;
//            }
//            else
//            {
//                retorno |= 8;
//            }
//            if (lamp)
//            {
//                if (spd <= speed) // 0010 0000 - 0x20
//                {
//                    retorno |= 32;

//                }
//                else //0001 0000 - 0x10
//                {
//                    retorno |= 16;

//                }
//            }
//            retorno |= 128;
//            return retorno;
//        }
//        #endregion

//        #region Threads
//        public void Stay_Connected()
//        {            
//            hora = DateTime.Now;
//            while (true)
//            {
//                Thread.Sleep(timeoutresposta);
//                int ms = (int)DateTime.Now.Subtract(resp).TotalMilliseconds;
//                if (!resposta)
//                {
//                    if (exceptions_display != null)
//                    {
//                        exceptions_display(this, "Houve uma desconexão do socket, tentando reconectar\r\nresposta: " + resposta + "\r\n" + DateTime.Now.Subtract(resp).TotalMilliseconds);
//                    }
//                    //if (log_checked) log.WriteLogEvent("Houve uma desconexão do socket, tentando reconectar", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                    while (true)
//                    {
//                        Thread.Sleep(1);
//                        try
//                        {
//                            s.leituradados -= new Socket_Serial.Leitura_dados(Read);
//                            // s.exceptions -= new Socket_Serial.Exceptions(Exceptions);
//                            s.Close();
//                            //Thread.Sleep(5000);
//                            s = null;
//                            if (ADHOC)
//                            {
//                                s = new Socket_Serial(ipEp, SSID, password);
//                            }
//                            else
//                            {
//                                s = new Socket_Serial(ipEp);
//                            }
//                            s.Open();
//                            s.leituradados += new Socket_Serial.Leitura_dados(Read);
//                            //s.exceptions += new Socket_Serial.Exceptions(Exceptions);
//                            resposta = true;
//                            //resp = DateTime.Now;
//                            //if (log_checked) log.WriteLogEvent("Reconexão realizada com sucesso", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Status);
//                            break;
//                        }
//                        catch (Exception ex)
//                        {
//                            if (exceptions_display != null)
//                            {
//                                exceptions_display(this, ex.Message);
//                            }
//                            //if (log_checked) log.WriteLogEvent("Falha ao tentar reconectar com display", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                        }
//                    }
//                }



//                int x = (int)DateTime.Now.Subtract(hora).TotalSeconds;
//                if (x > intervalo && is_running)
//                {
//                    try
//                    {
//                        byte[] temp = Encoding.ASCII.GetBytes("EV");
//                        byte[] t1 = new byte[1];
//                        t1[0] = checksum.ComputeChecksum_Display(temp);
//                        byte[] envio = new byte[7];
//                        envio[5] = 0x0D;
//                        envio[6] = 0x0A;
//                        Array.Copy(Encoding.ASCII.GetBytes("&EV*"), 0, envio, 0, 4);
//                        Array.Copy(t1, 0, envio, 4, 1);
//                        s.Send(Encoding.ASCII.GetString(envio));
//                        //send = true;
//                        resp = DateTime.Now;
//                        resposta = false;
//                        //if (log_checked) log.WriteLogEvent("Enviado &EV", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Event);
//                    }
//                    catch (Exception ex)
//                    {
//                        if (exceptions_display != null)
//                        {
//                            exceptions_display(this, "Falha ao enviar &EV " + ex.Message);

//                        }
//                        //if (log_checked) log.WriteLogEvent("Falha ao enviar &EV " + ex.Message, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                        continue;
//                    }
//                    hora = DateTime.Now;
//                    //resposta = false;
//                }
//            }

//        }
//        #endregion

//        #region Eventos
//        public void Read(Object sender, string dados)
//        {
//            if (dados.StartsWith("&ST"))
//            {
//                string ch = dados.Substring(1, 3);
//                byte b = checksum.ComputeChecksum_Display(Encoding.Default.GetBytes(ch));
//                byte[] cks = Encoding.Default.GetBytes(dados.Substring(5, 1));
//                if (b == cks[0])
//                {
//                    send = false;
//                    resposta = true;
//                }
//                if (dados.Substring(3, 1) == "0")
//                {
//                    if (count_fault < 3)
//                    {
//                        //if (log_checked) log.WriteLogEvent("Falha no Display\r\n", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                        count_fault++;
//                        //if(count_fault == 3)
//                        //if (log_checked) log.WriteLogEvent("Recebido Status de falha no Display por três vezes consecutivas\r\n", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
//                    }
//                }
//                else if (dados.Substring(3, 1) == "1")
//                {
//                    // if(count_fault == 3)
//                    //if (log_checked) log.WriteLogEvent("O Display reestabeleceu o funcionamento\r\n", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.System);
//                    count_fault = 0;
//                }
//            }
//            if (read_display != null)
//                read_display(this, dados);
//        }

//        /*public void Exceptions(Object sender, string excessoes)
//        {
//            if (log_checked) log.WriteLogEvent(excessoes, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);

//            if (exceptions_display != null)
//            {
//                exceptions_display(sender, excessoes);                
//            }
//        }*/

//        #endregion

//        #region Structs/Enums
//        public struct Period
//        {
//            public DateTime begin;
//            public DateTime end;
//        }

//        public enum OperationMode
//        {
//            DISTANCE = 0,
//            SPEED = 1
//        }
//        #endregion
//    }
//}


using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
//using br.com.ltb.trufix.interop.Laser;
using TruCaptureEmulator;

namespace TruCaptureEmulator
{
    public class Display
    {
        #region atributos
        private string IP;
        private int porta;
        private IPEndPoint ipEp;
        private string SSID;
        private string password;
        private Socket_Serial s;
        private bool is_running;
        private int timereconect = 3000;
        private bool log_checked = false;
        //private Log log;
        private bool ADHOC = true;
        private OperationMode mode = OperationMode.SPEED;
        private int speed;
        private Period period = new Period();
        private int count_fault = 0;
        private int timeoutresposta = 2000;
        private DateTime resp;
        private bool logReconect = false;
        public Period Interval
        {
            get { return period; }
            set
            {
                if (value.begin > value.end)
                {
                    period = value;
                }
                else
                {
                    throw new Exception("Parâmetros inválidos, hora de início deve ser maior que hora de término");
                }
            }
        }
        public OperationMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private Status _status = Status.Disconnected;
        private bool _continue = false;

        #endregion

        #region variáveis, Threads e delegates
        private Thread t, c;
        private DateTime hora;
        private int intervalo = 30;
        private bool resposta = true;
        public delegate void Exceptions_Display(Object sender, string text);
        public event Exceptions_Display exceptions_display;
        public delegate void Read_Display(Object sender, string text);
        public event Read_Display read_display;
        private Checksum_display checksum = new Checksum_display();
        private ManualResetEvent _answer = new ManualResetEvent(true);
        private List<string> _toSend = new List<string>();

        public bool Is_running
        {
            get { return is_running; }
        }
        #endregion

        #region Construtores
        public Display(string IP, int porta, string SSID, string password, int speed)
        {
            try
            {
                ipEp = new IPEndPoint(IPAddress.Parse(IP), porta);
                this.IP = IP;
                this.porta = porta;
                this.SSID = SSID;
                this.password = password;
                this.speed = speed;
                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
                period.end = new DateTime(1, 1, 1, 6, 0, 0);
                s = new Socket_Serial(ipEp, SSID, password);
                t = new Thread(new ThreadStart(Stay_Connected));
                t.Priority = ThreadPriority.Lowest;
                t.IsBackground = true;
                c = new Thread(new ThreadStart(Connect));
                c.Priority = ThreadPriority.Lowest;
                c.IsBackground = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Display(IPEndPoint ipEp, string SSID, string password, int speed)
        {
            try
            {
                this.ipEp = ipEp;
                this.SSID = SSID;
                this.password = password;
                this.speed = speed;
                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
                period.end = new DateTime(1, 1, 1, 6, 0, 0);
                s = new Socket_Serial(ipEp, SSID, password);
                t = new Thread(new ThreadStart(Stay_Connected));
                t.Priority = ThreadPriority.Lowest;
                t.IsBackground = true;

                c = new Thread(new ThreadStart(Connect));
                c.Priority = ThreadPriority.Lowest;
                c.IsBackground = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Display(IPEndPoint ipEp, int speed)
        {
            try
            {
                this.ipEp = ipEp;
                this.speed = speed;
                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
                period.end = new DateTime(1, 1, 1, 6, 0, 0);
                s = new Socket_Serial(ipEp);
                ADHOC = false;
                t = new Thread(new ThreadStart(Stay_Connected));
                t.Priority = ThreadPriority.Lowest;
                t.IsBackground = true;
                c = new Thread(new ThreadStart(Connect));
                c.Priority = ThreadPriority.Lowest;
                //c.Priority = ThreadPriority.Normal;
                c.IsBackground = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Display(string IP, int porta, int speed)
        {
            try
            {
                ipEp = new IPEndPoint(IPAddress.Parse(IP), porta);
                this.speed = speed;
                period.begin = new DateTime(1, 1, 1, 18, 0, 0);
                period.end = new DateTime(1, 1, 1, 6, 0, 0);
                s = new Socket_Serial(ipEp);
                ADHOC = false;
                t = new Thread(new ThreadStart(Stay_Connected));
                t.Priority = ThreadPriority.Lowest;
                t.IsBackground = true;
                c = new Thread(new ThreadStart(Connect));
                c.Priority = ThreadPriority.Lowest;
                //c.Priority = ThreadPriority.Normal;
                c.IsBackground = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Métodos
        //public void SetLog(Log log)
        //{
        //    this.log = log;
        //    log_checked = true;
        //}

        private void Connect()
        {
            int count = 0;
            while (true)
            {
                try
                {
                    resposta = true;
                    is_running = true;
                    t.Start();
                    s.Open();
                    s.leituradados += new Socket_Serial.Leitura_dados(Read);
                    _status = Status.Connected;
                    //if (log_checked) log.WriteLogEvent("Display conectado ao IP " + ipEp.Address.ToString(), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Status);
                    break;
                }
                catch (Exception ex)
                {
                    //if (log_checked) log.WriteLogEvent("Erro ao conectar display IP " + ipEp.Address.ToString(), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                    is_running = false;
                    count++;
                    //if (count < 3)
                    //{
                    if (ADHOC)
                    {
                        s.Disconect();
                        s = null;
                        s = new Socket_Serial(ipEp, SSID, password);
                        if (exceptions_display != null)
                        {
                            exceptions_display(this, "Houve uma falha ao tentar conectar com a rede " + SSID);
                        }
                    }
                    else
                    {
                        s = null;
                        s = new Socket_Serial(ipEp);
                        try
                        {
                            is_running = false;
                            Thread.Sleep(10);
                            t = null;
                            t = new Thread(new ThreadStart(Stay_Connected));
                            t.Priority = ThreadPriority.Lowest;
                            t.IsBackground = true;
                        }
                        catch { }
                    }
                    //}
                    if (count >= 3)
                    {
                        count = 0;
                        //if (log_checked) log.WriteLogEvent("Excedidas 3 tentativas de conexão" + ex.Message, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                        if (exceptions_display != null)
                        {
                            exceptions_display(this, "Excedidas 3 tentativas de conexão " + ex.Message);
                        }
                    }
                }
                Thread.Sleep(timereconect);
            }
        }

        public void Start()
        {
            try
            {
                c.Start();
                _status = Status.Connecting;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close()
        {
            try
            {
                is_running = false;
                s.Close();
                s.leituradados -= new Socket_Serial.Leitura_dados(Read);
                //s.exceptions -= new Socket_Serial.Exceptions(Exceptions);
                //t.Abort();
                _status = Status.Disconnected;
                //if (log_checked) log.WriteLogEvent("Display desconectado", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Send(string texto)
        {
            if (_status == Status.Connected)
            {
                //&<velocidade>,<distancia>,<bytecomando>*csum\r\n            
                string[] palavra = new string[texto.Split(new char[] { ',' }).Length - 1];
                string envio = string.Empty;
                try
                {
                    palavra = texto.Split(',');
                    palavra[1] = palavra[1].Replace("-", string.Empty);
                    byte[] control = new byte[1];
                    control[0] = Command_2(Convert.ToInt32(palavra[1]));
                    envio = "DL," + palavra[1].Replace("-", string.Empty) + "," + palavra[2] + ",";
                    byte[] temp = System.Text.Encoding.Default.GetBytes(envio);
                    byte[] temp2 = new byte[temp.Length + 1];
                    System.Array.Copy(temp, 0, temp2, 0, temp.Length);
                    System.Array.Copy(control, 0, temp2, temp.Length, 1);
                    byte b = checksum.ComputeChecksum_Display(temp2);
                    envio = "&" + Encoding.Default.GetString(temp2, 0, temp2.Length) + "*" + Encoding.Default.GetString(new byte[] { b }, 0, 1) + "\r\n";
                }
                catch (Exception ex)
                {
                    //if (log_checked) log.WriteLogEvent("String de entrada incompatível com o formato adequado " + envio.Replace("\r\n", string.Empty), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                    throw ex;
                }
                resposta = false;
                resp = DateTime.Now;
                try
                {
                    hora = DateTime.Now;
                    //s.Send(envio);
                    _answer.Reset();
                    SendToSocket(envio);
                    //*************
                    //if (log_checked) log.WriteLogEvent("Enviado " + envio, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Event);
                    //*************
                }
                catch (Exception ex)
                {
                    //if (log_checked) log.WriteLogEvent("Falha ao enviar string: " + envio.Replace("\r\n", string.Empty), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                    throw ex;
                }
            }
            else
            {
                resposta = false;
                resp = DateTime.Now;
               //if (log_checked) log.WriteLogEvent("Tentativa de envio da string " + texto + " com socket desconectado, Display Status: " + is_running + ", " + _status + " Socket Status: " + s.Running, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                throw new Exception("Socket não conectado");
            }
        }

        private byte Command(int spd)
        {
            /*1111-1111
             *  nibble + significativo
             *  bit 0 (0001) -  nível 0 = menor luminosidade
             *                  nível 1 = maior luminosidade
             *  bit 1 (0010) -  nível 0 = distância
             *                  nível 1 = velocidade
             *  nible - significativo
             *  bit 0 (0001) - nível 0 = luz vermelha desligada
             *                 nível 1 = luz vermelha ligada
             *  bit 1 (0010) - nível 0 = luz verde desligada
             *                 nível 1 = luz verde ligada
             */
            byte retorno = 16;
            bool lamp = false;
            if (DateTime.Now.Hour > period.end.Hour && DateTime.Now.Hour < period.begin.Hour) retorno = 0;
            //else retorno &= 239;
            if (mode == OperationMode.SPEED)
            {
                retorno |= 32; // 0001 0000 - 0x10
                lamp = true;
            }
            //else retorno &= 239;
            if (lamp)
            {
                if (spd <= speed)                               // 0000 0010 - 0x02
                {
                    retorno |= 2;
                    //retorno &= 254;
                }
                else
                {
                    retorno |= 1;
                    //retorno &= 253;
                }
            }
            return retorno;
        }

        private byte Command_2(int spd)
        {
            /*
             * bit 0 - incrementa brilho (1)
             * bit 1 - decrementa brilho (2)
             * bit 2 - exibe velocidade (4)
             * bit 3 - exibe distância (8)
             * bit 4 - sinaliza vermelho(16)
             * bit 5 - sinaliza verde (32)
             * bit 6 - não usado
             * bit 7 - não usado            
            */
            byte retorno = 0;
            bool lamp = false;

            if (DateTime.Now.Hour < period.begin.Hour && DateTime.Now.Hour > period.end.Hour)
            {
                retorno = 1;
            }
            else
            {
                retorno = 2;
            }

            if (mode == OperationMode.SPEED)
            {
                retorno |= 4; // 0000 0100 - 0x04
                lamp = true;
            }
            else
            {
                retorno |= 8;
            }
            if (lamp)
            {
                if (spd <= speed) // 0010 0000 - 0x20
                {
                    retorno |= 32;

                }
                else //0001 0000 - 0x10
                {
                    retorno |= 16;

                }
            }
            retorno |= 128;
            return retorno;
        }

        #endregion

        #region Threads
        public void Stay_Connected()
        {
            int count_resp = 0;
            hora = DateTime.Now;
            while (is_running)
            {
                //Thread.Sleep(timeoutresposta);
                //if (!resposta && (DateTime.Now.Subtract(resp).TotalMilliseconds > timeoutresposta))                
                if (!_answer.WaitOne(timeoutresposta, false))
                {
                    if (_continue)
                    {
                        _continue = false;
                        continue;
                    }

                    _status = Status.Connecting;
                    try
                    {
                        _toSend.Clear();
                    }
                    catch (Exception ex)
                    {
                        if (exceptions_display != null)
                        {
                            exceptions_display(this, ex.Message);
                        }
                    }
                    count_resp++;
                    if (count_resp > 0)
                    {
                        count_resp = 0;
                        while (_status != Status.Connected)
                        {
                            try
                            {
                                if (s != null)
                                {
                                    s.leituradados -= new Socket_Serial.Leitura_dados(Read);
                                    s.Close();
                                    Thread.Sleep(100);
                                    s = null;
                                }
                                if (ADHOC)
                                {
                                    s = new Socket_Serial(ipEp, SSID, password);
                                }
                                else
                                {
                                    s = new Socket_Serial(ipEp);
                                }
                                resposta = true;
                                s.Open();
                                s.leituradados += new Socket_Serial.Leitura_dados(Read);
                                _status = Status.Connected;
                                _answer.Set();
                                //if (log_checked) log.WriteLogEvent("Reconexão realizada com sucesso, IP " + ipEp.Address.ToString(), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Status);
                                logReconect = false;
                                break;
                            }
                            catch (Exception ex)
                            {
                                if (!logReconect)
                                {
                                    logReconect = true;
                                    //if (log_checked) log.WriteLogEvent("Falha ao tentar reconectar com display IP" + ipEp.Address.ToString(), Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                                    if (exceptions_display != null)
                                    {
                                        exceptions_display(this, ex.Message);
                                    }
                                }
                            }
                            Thread.Sleep(5000);
                        }
                    }
                }
                else
                {
                    _continue = false;
                }

                int x = (int)DateTime.Now.Subtract(hora).TotalSeconds;
                if (x > intervalo && _status == Status.Connected)
                {
                    try
                    {
                        resposta = false;
                        resp = DateTime.Now;
                        byte[] temp = Encoding.ASCII.GetBytes("EV");
                        byte[] t1 = new byte[1];
                        t1[0] = checksum.ComputeChecksum_Display(temp);
                        byte[] envio = new byte[7];
                        envio[5] = 0x0D;
                        envio[6] = 0x0A;
                        Array.Copy(Encoding.ASCII.GetBytes("&EV*"), 0, envio, 0, 4);
                        Array.Copy(t1, 0, envio, 4, 1);
                        _answer.Reset();

                        //s.Send(Encoding.ASCII.GetString(envio, 0, envio.Length));
                        SendToSocket(Encoding.ASCII.GetString(envio, 0, envio.Length));

                        //*************
                        //if (log_checked) log.WriteLogEvent("Enviado EV ", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Event);
                        //*************
                    }
                    catch (Exception ex)
                    {
                        if (exceptions_display != null)
                        {
                            exceptions_display(this, "Falha ao enviar &EV " + ex.Message);
                        }
                        //if (log_checked) log.WriteLogEvent("Falha ao enviar &EV " + ex.Message, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                    }
                    hora = DateTime.Now;
                }
            }
        }

        #endregion


        private void SendToSocket(string send)
        {
            //Thread t = new Thread(new ThreadStart(SendString));
            Thread t = new Thread(() => SendStringArgs(send));
            _toSend.Add(send);
            t.Priority = ThreadPriority.Normal;
            t.Start();
        }

        //private void SendString()
        //{
        //    if (_toSend.Count > 0)
        //    {
        //        int index = _toSend.Count - 1;
        //        string _send = _toSend[index];

        //        try
        //        {
        //            s.Send(_send);
        //            _toSend.RemoveAt(index);
        //        }
        //        catch (Exception ex)
        //        {
        //            if (exceptions_display != null)
        //            {
        //                exceptions_display(this, "Falha ao enviar " + _send + ", erro: " + ex.Message);
        //            }
        //            if (log_checked) log.WriteLogEvent("Falha ao enviar " + _send + ", erro: " + ex.Message, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
        //        }
        //    }
        //}

        private void SendStringArgs(string _send)
        {
            try
            {
                s.Send(_send);
            }
            catch (Exception ex)
            {
                if (exceptions_display != null)
                {
                    exceptions_display(this, "Falha ao enviar " + _send + ", erro: " + ex.Message);
                }
                //if (log_checked) log.WriteLogEvent("Falha ao enviar " + _send + ", erro: " + ex.Message, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
            }
        }

        #region Eventos
        public void Read(Object sender, string dados)
        {
            //*************
            //if (log_checked) log.WriteLogEvent("Recebido " + dados, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Event);
            //*************
            _answer.Set();
            if (dados.StartsWith("&ST"))
            {
                string ch = dados.Substring(1, 3);
                byte b = checksum.ComputeChecksum_Display(Encoding.Default.GetBytes(ch));
                byte[] cks = Encoding.Default.GetBytes(dados.Substring(5, 1));
                if (b == cks[0])
                {
                    resposta = true;
                }
                if (dados.Substring(3, 1) == "0")
                {
                    if (count_fault < 3)
                    {
                        //if (log_checked) log.WriteLogEvent("Falha no Display", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                        count_fault++;
                        //if (count_fault == 3)
                        //    if (log_checked) log.WriteLogEvent("Recebido Status de falha no Display por três vezes consecutivas", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);
                    }
                }
                else if (dados.Substring(3, 1) == "1")
                {
                    //if (count_fault == 3)
                    //    if (log_checked) log.WriteLogEvent("O Display reestabeleceu o funcionamento", Log.ModuleName.EletronicSpeedDisplay, Log.LogType.System);
                    count_fault = 0;
                }
            }
            if (read_display != null)
                read_display(this, dados);
        }

        /*public void Exceptions(Object sender, string excessoes)
        {
            if (log_checked) log.WriteLogEvent(excessoes, Log.ModuleName.EletronicSpeedDisplay, Log.LogType.Error);

            if (exceptions_display != null)
            {
                exceptions_display(sender, excessoes);                
            }
        }*/

        #endregion

        #region Structs/Enums
        public struct Period
        {
            public DateTime begin;
            public DateTime end;
        }

        public enum OperationMode
        {
            DISTANCE = 0,
            SPEED = 1
        }

        public enum Status
        {
            Disconnected = 0,
            Connecting = 1,
            Connected = 2
        }

        #endregion
    }
}
