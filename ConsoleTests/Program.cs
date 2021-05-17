using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = { 0x01, 0x20, 0x21, 0x5A, 0x6B, 0x0C, 0x05 };
            List<byte> ls = Bcc(data);
            byte seq = 0x20;
            List<byte> outMessage = new List<byte>();
                int i = 0;
                var outData = new byte[data.Length + 7];
                outData[++i] = 0x01;                          // Start byte
                outData[++i] = (byte)(data.Length + 4);       // Len byte
                outData[++i] = seq;                           // Seq byte

                outMessage.Add((byte)(data.Length + 4));
                outMessage.Add(seq);
                outMessage.AddRange(data);
                outMessage.Add(0x05);
                outMessage.AddRange(Bcc(outMessage.ToArray()));
                outMessage.Add(0x03);
                outMessage.Insert(0, 0x01);

            foreach (byte b in outMessage)
            {
                 Console.WriteLine(b);
            }
            Console.ReadKey();

        }

        static List<byte> Bcc(byte[] data)
        {
            List<byte> bcc = new List<byte>();
            uint sum = 0;
            foreach (byte d in data)
            {
                sum += d;
            }

            bcc.Add((byte)(sum / 4096));
            sum %= 4096;
            bcc.Add((byte)(sum / 256));
            sum %= 256;
            bcc.Add((byte)(sum / 16));
            sum %= 16;
            bcc.Add((byte)sum);
            return bcc;
        }
    }
}
