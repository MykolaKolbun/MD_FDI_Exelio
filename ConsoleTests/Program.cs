using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {

            ///
            ///     01 25 21 5a 30 05 30 30 3d 35 03                  .%!Z0.00=5.      

            byte[] data = { 0x30, 0x30, 0x30, 0x31, 0x2c, 0x30, 0x30, 0x30, 0x31 };
            byte[] status = { 0x88, 0x80, 0x88, 0xc7, 0x86, 0x98 };
            //List<byte> ls = Bcc(data);
            //byte seq = 0x21;
            List<byte> outMessage = new List<byte>
            {
                0x01,
                0x34,
                0x20,
                0x30
            };
            outMessage.AddRange(data);
            outMessage.Add(0x04);
            outMessage.AddRange(status);
            outMessage.Add(0x05);
            outMessage.AddRange(Bcc(outMessage.ToArray()));
            outMessage.Add(0x03);
            //byte cmd = 0x46;
            //double amount = -120000000;
            //Console.WriteLine(amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
            //outMessage.Add(cmd);
            //outMessage.AddRange(amount.ToString());
            //outMessage = GetBytesFromDouble(amount);
            //foreach (byte b in outMessage)
            //{
            //    Console.WriteLine(b);
            //}
            ReceivedHandler(outMessage.ToArray(), 1);
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

        static private List<byte> GetBytesFromInt(int intValue)
        {
            List<byte> byteList = new List<byte>();
            for (int i = 12; i >= 0; i--)
            {
                byteList.Add((byte)(intValue / Math.Pow(10, i)));
                intValue %= (int)Math.Pow(10, i);
            }
            while (byteList[0] == 0)
            {
                byteList.RemoveAt(0);
            }
            /*
            byteList.Add((byte)(intValue / 4096 + 48));
            intValue %= 4096;
            byteList.Add((byte)(intValue / 256 + 48));
            intValue %= 256;
            byteList.Add((byte)(intValue / 16 + 48));
            intValue %= 16;
            byteList.Add((byte)(intValue + 48));
            */
            return byteList;
        }

        static private List<byte> GetBytesFromString(string str)
        {
            List<byte> outMessage = new List<byte>();
            outMessage.AddRange(Encoding.ASCII.GetBytes(str));
            return outMessage;
        }

        static private List<byte> GetBytesFromDouble(double value)
        {
            string str = value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            List<byte> outMessage = new List<byte>();
            outMessage.AddRange(Encoding.ASCII.GetBytes(str));
            return outMessage;
        }

        private static void ReceivedHandler(byte[] data, ulong dataLen)
        {
            byte preambula = 0x01;
            byte seq = 0;
            byte len = 0;
            byte cmd = 0;
            List<byte> cmdData = new List<byte>();
            byte delimiter = 0x04;
            List<byte> status = new List<byte>();
            byte postambula = 0x05;
            List<byte> bcc = new List<byte>();
            byte terminator = 0x03;
            if (data[0] == preambula)
            {
                len = data[1];
                seq = data[2];
                cmd = data[3];
                int i = 4;
                while (data[i] != delimiter)
                {
                    cmdData.Add(data[i]);
                    i++;
                }
                i++;
                while (data[i] != postambula)
                {
                    status.Add(data[i]);
                    i++;
                }
                i++;
                while (data[i] != terminator)
                {
                    bcc.Add(data[i]);
                    i++;
                }
            }
            Console.WriteLine($"Len: {len}");
            Console.WriteLine($"Seq: {seq}");
            Console.WriteLine($"Cmd : {cmd}");
            Console.WriteLine("Status:");
            foreach (byte b in status)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine("Data:");
            foreach (byte b in cmdData)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine("BCC:");
            foreach (byte b in bcc)
            {
                Console.WriteLine(b);
            }
        }
    }
}
