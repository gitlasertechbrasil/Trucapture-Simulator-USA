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
        public static parameters parametros;

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
