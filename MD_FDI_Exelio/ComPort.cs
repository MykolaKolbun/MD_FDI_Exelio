using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD_FDI_Exelio
{
    class ComPort
    {
        SerialPort Port = new SerialPort();
        public delegate void Received(byte[] data, UInt16 len);
        public event Received ReceivedEvent;
        byte seq = 0x20;
        private void OnRecievedEvent(byte[] data, UInt16 len)
        {
            if (this.ReceivedEvent != null)
                this.ReceivedEvent(data, len);
        }

        /// <summary>
        /// Инициализация и открытие СОМ-порта
        /// </summary>
        /// <param name="_portname">Имя СОМ-порта</param>
        /// <returns></returns>
        public int Connect(string _portname)
        {
            //Насторойка порта
            Port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            Port.PortName = _portname;
            Port.BaudRate = 57600;
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.Handshake = Handshake.None;
            Port.RtsEnable = true;
            Port.DtrEnable = true;
            Port.Encoding = Encoding.Unicode;
            try
            {
                if (!(Port.IsOpen))
                    Port.Open();
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }

        /// <summary>
        /// Prepare and send data to printer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SendData(byte [] data)
        {
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
            Send(outMessage.ToArray());
            return 0;
        }

        /// <summary>
        /// Обработчик события прихода данных в COM-порт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            port.ReadTimeout = 500;
            var buffer = new byte[256];
            byte preambula;
            byte len = 0;
            int i = 0;
            preambula = (byte)port.ReadByte();
            if (preambula == 0x01)
            {
                len = (byte)port.ReadByte();

                do
                {
                    buffer[i] = (byte)port.ReadByte();
                }
                while (buffer[++i] != 0x03);
                this.OnRecievedEvent(buffer, (UInt16)(len - 0x20));
            }
            else if(preambula == 0x16)
            {

            }
        }

        /// <summary>
        /// Отправка данных в COM-порт
        /// </summary>
        /// <param name="data">массив с данными для отправки</param>
        /// <returns></returns>
        private int Send(byte[] data)
        {
            try
            {
                Port.Write(data, 0, data.Length);
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }

        /// <summary>
        /// Закрыть COM-порт
        /// </summary>
        public int Close()
        {
            try
            {
                Port.Close();
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
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
