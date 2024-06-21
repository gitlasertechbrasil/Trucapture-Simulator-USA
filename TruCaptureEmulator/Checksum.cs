using System;
using System.Collections.Generic;
using System.Text;

namespace TruCaptureEmulator
{
    public class Checksum_display //Checksum das strings do display
    {
        public Checksum_display()
        {
            
        }

        public byte ComputeChecksum_Display(params byte[] bytes)
        {
            byte crc = 0;
            int x = 0;
            for (int y = 0; y < bytes.Length; y++)
            {
                x = x + bytes[y];
            }
            crc = (byte)~x;
            return crc;
        }
    } 
}
