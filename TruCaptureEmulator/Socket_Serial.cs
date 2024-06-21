using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;


namespace TruCaptureEmulator
{
    public class Socket_Serial
    {
       #region atributos
       private Socket sktSerial;       
       private IPEndPoint ipEp;     
       private Thread Receive = null;      
       public delegate void Leitura_dados(object sender,string e);
       public event Leitura_dados leituradados;        
       //public delegate void Exceptions(object sender, string e);
       //public event Exceptions exceptions;
       private string lidos;
       private byte[] dados;
       private int bytes_lidos = 0;
       //private OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface INw;
       private string SSID = string.Empty;
       private string password;
       private bool adhoc = true;

       private bool running = false;

       public bool Running
       {
           get { return running; }
       }
       #endregion

       #region Construtores
       public Socket_Serial(IPEndPoint ipEp,string SSID,string password)
       {
           this.ipEp = ipEp;
           this.SSID = SSID;
           sktSerial = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           sktSerial.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
           Receive = new Thread(new ThreadStart(Read));
           Receive.IsBackground = true;           
           Receive.Priority = ThreadPriority.Lowest;
           this.password = Adjust_Password(password);
       }

       public Socket_Serial(IPEndPoint ipEp)
       {
           this.ipEp = ipEp;           
           sktSerial = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           sktSerial.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
           Receive = new Thread(new ThreadStart(Read));
           Receive.IsBackground = true;
           Receive.Priority = ThreadPriority.Lowest;
           adhoc = false;
       }
       #endregion

       #region Métodos
       public void Send(string texto)
       {
           try
           {
               if (running)
               {
                   byte[] b = Encoding.Default.GetBytes(texto);
                   sktSerial.Send(b);
               }
               else
               {
                   throw new Exception("Socket não conectado");
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }      

       /*public void Ping()
       {
           Ping ping = new Ping();
           int count = 0;
           string fault=string.Empty;
           DateTime t = DateTime.Now;

           while (true)
           {
               Thread.Sleep(1);
               try
               {
                   if (running && DateTime.Now.Subtract(t).TotalSeconds > 4)
                   {

                       PingReply pingReply = ping.Send(ipEp.Address.ToString(), 10000);
                       if (pingReply.Status != IPStatus.Success)
                       {
                           count++;
                       }
                       else
                       {
                           count = 0;
                       }
                   }
               }
               catch (Exception ex)
               {
                   count++;
                   fault = ex.Message;
               }

               if (count > 2)
               {                  
                   if (exceptions != null)
                   {
                       exceptions(this, "Falha no ping " + fault);
                   }
               }
           }
       }*/ 
       
       public void Open()
       {
           try
           {
               if (adhoc)
               {
                   Connect_to_Network();
                   Thread.Sleep(5000);
               }
               //sktSerial = null;
               //sktSerial = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);               
               sktSerial.Connect(ipEp);
               running = true;
               Receive.Start();              
           }
           catch (Exception ex)
           {
               throw new Exception("Falha ao abrir o socket " + ex.Message);
           }
       }

       public void Close()
       {
           try
           {
               running = false;
               Receive.Abort();
               //sktSerial.Shutdown(SocketShutdown.Both);
               sktSerial.Close();
               if (adhoc)
               {
                   Disconect();
               }
           }
           catch (Exception ex)
           {
               throw new Exception("Falha ao fechar o socket " + ex.Message);
           }
       }

       private void Connect_to_Network()
       {
           /*foreach (OpenNETCF.Net.NetworkInformation.INetworkInterface ni1 in OpenNETCF.Net.NetworkInformation.WirelessNetworkInterface.GetAllNetworkInterfaces())
           {
               if (ni1 is OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface)
               {
                   try
                   {
                       INw = (OpenNETCF.Net.NetworkInformation.WirelessZeroConfigNetworkInterface)ni1;
                       OpenNETCF.Net.NetworkInformation.EAPParameters r = new OpenNETCF.Net.NetworkInformation.EAPParameters();                    
                       INw.AddPreferredNetwork(SSID,false,password,1,AuthenticationMode.Shared,WEPStatus.WEPEnabled,r);
                       Thread.Sleep(1000);                  
                       INw.ConnectToPreferredNetwork(SSID);
                       Thread.Sleep(1000);
                   }
                   catch (Exception x)
                   {
                       throw new Exception(x.Message);
                   }                

                   break;
               }
           }*/
       }

       public void Disconect()
       {
           try
           {
               //INw.RemovePreferredNetwork(SSID);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       private string Adjust_Password(string pwd)
       {
           char[] charValues = pwd.ToCharArray();
           string hexOutput = "";
           foreach (char letra in charValues)
           {
               int value = Convert.ToInt32(letra);
               hexOutput += String.Format("{0:X}", value);
           }
           return hexOutput;
       }
       #endregion

       #region Threads
       public void Read()
       {
           int disponiveis = 0;
           while (running)
           {
               Thread.Sleep(10);
               try
               {
                   disponiveis = sktSerial.Available;
               }
               catch (Exception ex)
               {
                   //exceptions(this, ex.Message);
                   continue;
               }
               if (disponiveis > 7)
               {
                   dados = new byte[8];

                   try
                   {
                       bytes_lidos = sktSerial.Receive(dados,8,SocketFlags.None);
                   }
                   catch
                   {
                       bytes_lidos = 0;
                       disponiveis = 0;
                       dados = null;
                       continue;
                   }

                   lidos = Encoding.Default.GetString(dados, 0, dados.Length);
                   if (leituradados != null)
                   {
                       leituradados(this, lidos);
                       lidos = string.Empty;
                       bytes_lidos = 0;
                       disponiveis = 0;
                       dados = null;
                   }
               }
           }
       }
       #endregion
    }   
}
