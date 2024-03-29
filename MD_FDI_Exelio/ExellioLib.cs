﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace MD_FDI_Exelio
{
    public class ExellioLib
    {
        ComPort printer;
        static ushort sync = 0;
        public static Timer receivingTimer;
        public static byte ANS { get; set; }
        private bool received { get; set; }
        public static uint lastError { get; set; }
        public static int lastStatus { get; set; }
        private bool timeElapsed { get; set; }

        // Printer statuses
        public bool generalError = false;        //General error – “OR” of all errors marked with ‘#’.
        public bool printerError = false;        //Failure in printing mechanism, knife or presenter
        public bool noDisplay = false;           //Not connected a customer display
        public bool clockError = false;          //The clock needs settings
        public bool coverPaper = false;          //Paper cover is open 
        public bool fiscalNotOpened = false;     //A non-fiscal receipt is opened.
        public bool fiscalOpened = false;        //A fiscal receipt is opened
        public bool lowPaper = false;            //Not enough paper
        public bool outOfPaper = false;          //No paper – valid for both paper rolls.
        public bool fiscalMemoryError = false;   //Fiscal memory is fully engaged.
        public bool lowCriticalECL = false;      //Залишилась лише резервна ємність ECL, залишилося менше 2000 Г байт. Команди, записані в ECL, не виконуються.
        public bool low3000ECL = false;          //ECL дуже близький до своєї максимальної ємності, залишилося менше 3000 Г байт. Цей прапор призначений лише для інформації і не впливає на жодну команду.
        public bool low4000ECL = false;          //ECL близький до своєї максимальної ємності, залишилося менше 4000 Г байт. Цей прапор призначений лише для інформації і не впливає на жодну команду.
        public bool AmountOverloaded = false;

        public uint daySum = 0;

        public delegate void StatusChanged(int status);
        public event StatusChanged StatusChangedEvent;

        #region Init
        public int Connect(string port)
        {
            printer = new ComPort();
            printer.ReceivedEvent += new ComPort.Received(Printer_ReceivedEvent);
            return printer.Connect(port);
        }

        public int Close()
        {
            return printer.Close();
        }
        #endregion


        #region Sale
        /// <summary>
        /// 30h (48) ОТКРЫТИЕ ФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <param name="OpCode">Номер на оператор /1 до 16/</param>
        /// <param name="OpPwd">Операторска парола /4 до 8 цифри/</param>
        /// <param name="TillNmb">Номер на касово място /цяло число до 5 цифри/</param>
        /// <returns></returns>
        public int OpenReceipt(int OpCode, string OpPwd, int TillNmb)
        {
            byte cmd = 0x30;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(Encoding.ASCII.GetBytes(OpCode.ToString()));
            outMessage.Add(0x2C);
            outMessage.AddRange(Encoding.ASCII.GetBytes(OpPwd));
            outMessage.Add(0x2C);
            outMessage.AddRange(Encoding.ASCII.GetBytes(TillNmb.ToString()));
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 31h (49) ENTER A PRODUCT (SALE)
        /// </summary>
        /// <param name="prodDescr">Text, up to 42 bytes, containing a line describing the sale</param>
        /// <param name="unitPrice">This is the unit price, with up to 8 significant digits.</param>
        /// <param name="qwant">Optional parameter indicating the product quantity. By default, it is 1.000. Length, up to 8 significant digits(no more than 3 after the decimal point). The result of the multiplication Price*Qwan is rounded by the printer to the number of decimal digits set and also must not exceed 8 significant digits.</param>
        /// <param name="unit">Name of the unit of measurement. Optional text for the unit of measurement for the quantity, up to 8 characters, for instance, “kg”.</param>
        /// <returns></returns>
        public int Sale(string prodDescr, double unitPrice, int qwant, string unit)
        {
            byte cmd = 0x31;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(Encoding.ASCII.GetBytes(prodDescr));
            outMessage.Add(0x09);
            outMessage.AddRange(GetBytes("A"));
            outMessage.AddRange(GetBytes(unitPrice * qwant).ToArray());
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 35h (53) ИТОГ («ВСЕГО»)
        /// </summary>
        /// <param name="amount">Paid amount</param>
        /// <param name="type">Payment type</param>
        /// <returns></returns>
        public int Total(int type, double sum)
        {
            byte cmd = 0x35;
            List<byte> outMessage = new List<byte>
            {
                cmd,
                0x09
            };
            switch (type)
            {
                case 1:
                    //outMessage.AddRange(Encoding.ASCII.GetBytes("P"));
                    break;
                case 2:
                    outMessage.AddRange(Encoding.ASCII.GetBytes("D"));
                    break;
                default:
                    break;
            }
            outMessage.AddRange(GetBytes(sum));
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 38h (56) ЗАКРЫТИЕ ФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int CloseReceipt()
        {
            byte cmd = 0x38;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 39h (57) АННУЛИРОВАНИЕ ФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int VoidReceipt()
        {
            byte cmd = 0x39;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }
        #endregion

        #region Reports
        public int ZReport()
        {
            byte cmd = 0x45;
            List<byte> outMessage = new List<byte>
            {
                cmd,
                0x31
            };
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }
        public int PrintZReport()
        {
            byte cmd = 0x78;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(GetBytes("K"));
            outMessage.Add(0x2C);
            outMessage.Add(0x33);
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }
        public int XReport()
        {
            byte cmd = 0x78;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(GetBytes("K"));
            outMessage.Add(0x2C);
            outMessage.Add(0x31);
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }
        #endregion

        #region Cash In/Out
        /// <summary>
        /// 46h (70) СЛУЖЕБНЫЙ ВНОС/ВЫДАЧА
        /// </summary>
        /// <returns></returns>
        public int CashInOut(double amount)
        {
            byte cmd = 0x46;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(GetBytes(amount));
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        #endregion

        #region Print text
        /// <summary>
        /// 26h (38) ОТКРЫТИЕ НЕФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int OpenDoc()
        {
            byte cmd = 0x26;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 27h (39) ЗАКРЫТИЕ НЕФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int CloseDoc()
        {
            byte cmd = 0x27;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 2Ah (42) ПЕЧАТЬ НЕФИСКАЛЬНОГО КОМЕНТАРИЯ
        /// </summary>
        /// <returns></returns>
        public int PrintText(string text)
        {
            byte cmd = 0x2A;
            byte[] bText = Encoding.Default.GetBytes(text);
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(bText);
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        public int FiscalText(string text)
        {
            byte cmd = 0x36;
            byte[] bText = Encoding.Default.GetBytes(text);
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(bText);
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        #endregion

        #region Service
        /// <summary>
        /// 3Dh (61) УСТАНОВКА ДАТЫ/ВРЕМЕНИ
        /// </summary>
        /// <returns></returns>
        public int SetDateTime()
        {
            byte cmd = 0x3D;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            outMessage.AddRange(GetBytes(DateTime.Now.ToString("dd-MM-yy HH:mm:ss")));
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 6Dh (109) ПЕЧАТЬ КОПИИ Последнего чека
        /// </summary>
        /// <returns></returns>
        public int CopyReceipt(int num)
        {
            byte cmd = 0x6D;
            List<byte> outMessage = new List<byte>
            {
                cmd,
                (byte)(0x30+num)
            };
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }

        /// <summary>
        /// 4Ah (74) СТАТУС РЕГИСТРАТОРА
        /// </summary>
        /// <returns></returns>
        public int PrinterStatus()
        {
            byte cmd = 0x4A;
            List<byte> outMessage = new List<byte>
            {
                cmd
            };
            int error = printer.SendData(outMessage.ToArray());
            if (error == 0)
            {
                error = StatusAnalyzer();
            }
            return error;
        }
        #endregion

        #region Private members
        private void Printer_ReceivedEvent(byte[] data, ushort dataLen)
        {
            List<byte> cmdData = new List<byte>();
            byte delimiter = 0x04;
            List<byte> status = new List<byte>();
            byte postambula = 0x05;
            int i = 2;
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

            StatusHandler(status.ToArray());
            ComPort.sent = true;
        }

        private void StatusHandler(byte[] status)
        {
            byte first = status[0];
            byte second = status[1];
            byte third = status[2];
            byte fourth = status[3];
            byte fifth = status[4];
            byte sixth = status[5];
            generalError = IsBitSet(first, 5);
            printerError = IsBitSet(first, 4);
            noDisplay = IsBitSet(first, 3);
            clockError = IsBitSet(first, 2);

            coverPaper = IsBitSet(second, 5);  // Якщо кришка принтера відкрита.

            lowCriticalECL = IsBitSet(third, 6);//2.6 = 1 Залишилась лише резервна ємність ECL, залишилося менше 2000 Г байт. Команди, записані в ECL, не виконуються.
            fiscalNotOpened = IsBitSet(third, 5);
            low3000ECL = IsBitSet(third, 4);
            fiscalOpened = IsBitSet(third, 3);
            low4000ECL = IsBitSet(third, 2);
            lowPaper = IsBitSet(third, 1);
            outOfPaper = IsBitSet(third, 0);
        }

        private int StatusAnalyzer()
        {
            /*
             * 0 - No error
             * 3 - Connection error
             * 
             * 100 - General error
             * 101 - Printer error
             * 102 - Critical Low EJ Memmory
             * 107 - Fiscal receipt already opened
             * 110 - Out of Paper
             * 111 - Cover not closed
             * 200 - Low paper
             * 201 - Low memmory (3000)
             * 202 - Low memmory (4000)
             * 
             */
            int error = 0;
            if (generalError)
            {
                error = 100;
                if (printerError)
                {
                    error = 101;
                }

                if (lowCriticalECL)
                {
                    error = 102;
                }

                if (fiscalOpened)
                {
                    error = 107;
                }

                if (outOfPaper)
                {
                    error = 110;
                }

                if (coverPaper)
                {
                    error = 111;
                }
            }
            else
            {
                if (lowPaper)
                {
                    error = 200;
                }

                if (low3000ECL)
                {
                    error = 201;
                }

                if (low4000ECL)
                {
                    error = 202;
                }
            }
            return error;
        }

        static bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private List<byte> GetBytes(int value)
        {
            List<byte> byteList = new List<byte>();
            for (int i = 12; i >= 0; i--)
            {
                byteList.Add((byte)(value / Math.Pow(10, i)));
                value %= (int)Math.Pow(10, i);
            }
            while (byteList[0] == 0)
            {
                byteList.RemoveAt(0);
            }
            return byteList;
        }

        private List<byte> GetBytes(double value)
        {
            string str = value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            List<byte> outMessage = new List<byte>();
            outMessage.AddRange(Encoding.ASCII.GetBytes(str));
            return outMessage;
        }

        private List<byte> GetBytes(string value)
        {
            List<byte> outMessage = new List<byte>();
            outMessage.AddRange(Encoding.ASCII.GetBytes(value));
            return outMessage;
        }
        #endregion

    }
}
