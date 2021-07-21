using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Timers;

namespace MD_FDI_Exelio
{
    class ComPort
    {
        SerialPort Port = new SerialPort();
        public delegate void Received(byte[] data, ushort len);
        public event Received ReceivedEvent;
        byte seq = 0x31;

        static int proc = 2;
        public static bool sent = false;
        static bool timeout = false;

        Timer timeoutTimer;
        public void OnRecievedEvent(byte[] data, ushort len)
        {
            if (ReceivedEvent != null)
            {
                ReceivedEvent(data, len);
            }
        }

        /// <summary>
        /// Инициализация и открытие СОМ-порта
        /// </summary>
        /// <param name="_portname">Имя СОМ-порта</param>
        /// <returns></returns>
        public int Connect(string _portname)
        {
            //Насторойка порта
            timeoutTimer = new Timer(9000)
            {
                AutoReset = false
            };
            timeoutTimer.Elapsed += TimeoutTimer_Elapsed;
            Port.PortName = _portname;
            Port.BaudRate = 115200;
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.Handshake = Handshake.None;
            Port.RtsEnable = true;
            Port.DtrEnable = true;
            Port.Encoding = Encoding.Default;
            Port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            try
            {
                if (!(Port.IsOpen))
                {
                    Port.Open();
                }
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }

        private void TimeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timeout = true;
        }

        /// <summary>
        /// Prepare and send data to printer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SendData(byte[] data)
        {

            List<byte> outMessage = new List<byte>
            {
                (byte)(data.Length + 3 + 32),
                seq
            };
            outMessage.AddRange(data);
            outMessage.Add(0x05);
            outMessage.AddRange(Bcc(outMessage.ToArray()));
            outMessage.Add(0x03);
            outMessage.Insert(0, 0x01);
            
            int error = Send(outMessage.ToArray());
            sent = false;
            timeout = false;
            timeoutTimer.Enabled = true;
            while (!sent && !timeout)
            { }
            if (timeout)
            {
                error = 2;
            }
            if (seq >= 0x7e)
                seq = 0x31;
            else
                seq++;
            return error;
        }

        /// <summary>
        /// Обработчик события прихода данных в COM-порт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort port = (SerialPort)sender;
                port.ReadTimeout = 10000;
                var buffer = new byte[256];
                byte received;
                byte len;
            Reread:
                received = (byte)port.ReadByte();
                if (received == 0x16)
                {
                    timeoutTimer.Enabled = false;
                    timeoutTimer.Enabled = true;
                    goto Reread;
                }
                
                if (received == 0x01)
                {
                    len = (byte)port.ReadByte();
                    for (int i = 0; i < (len - 28); i++)
                    {
                        buffer[i] = (byte)port.ReadByte();
                    }
                    timeoutTimer.Enabled = false;
                    OnRecievedEvent(buffer, len);
                }
                else
                {
                    port.ReadExisting();
                }
            }
            catch
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
                Port.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
                Port.Close();
                Port.Dispose();
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
