using br.com.ltb.Camera.Pumatronix;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static TruCaptureEmulator.Trucaptureemulator;

namespace TruCaptureEmulator
{
    static class Program
    {

        public static bool programarodando = true;
        public static List<byte[]> imgvideo = new List<byte[]>();
        public static List<byte[]> imgphoto = new List<byte[]>();
        public static List<ParameterCGI> ParameterList = new List<ParameterCGI>();
        public static string _currentCameraModel = "ITSCAM401";
        public static string version = "v19.2.4";
        public static string buildVersion = string.Empty;
        public static parameters parametros;
        public static bool senhaAPI = false;
        public static string senha = "admin:123";
        public static bool HasDS = false;
        public static bool DSDefault = false;

        public static RSAParameters _publicKey = new RSAParameters();
        public static RSAParameters _privateKey = new RSAParameters();

        public const int SignDelay = 500;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {           

            
              

                Application.Run(new frmTrucapture());
                programarodando = false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Falha ao ler os frames do video ou as imagens padrão", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }   
      


    }
}
