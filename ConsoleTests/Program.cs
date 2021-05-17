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

            ///
            ///     01 25 21 5a 30 05 30 30 3d 35 03                  .%!Z0.00=5.      

            byte[] data = { 0x5a, 0x30 };
            List<byte> ls = Bcc(data);
            byte seq = 0x21;
            List<byte> outMessage = new List<byte>();
                outMessage.Add((byte)(data.Length + 3 + 32));
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

            bcc.Add((byte)(sum / 4096 + 48));
            sum %= 4096;
            bcc.Add((byte)(sum / 256 + 48));
            sum %= 256;
            bcc.Add((byte)(sum / 16 + 48));
            sum %= 16;
            bcc.Add((byte)(sum + 48));
            return bcc;
        }
    }
}
