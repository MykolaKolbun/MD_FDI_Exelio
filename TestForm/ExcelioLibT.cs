using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestForm
{
    class ExcelioLibT
    {
        ComPortT printer;
        static UInt16 sync = 0;
        public static Timer receivingTimer;
        public static byte ANS { get; set; }
        private bool received { get; set; }
        public static UInt32 lastError { get; set; }
        public static int lastStatus { get; set; }
        private bool timeElapsed { get; set; }

        // Printer statuses
        public static bool blockedStatus = false;
        public static bool shiftStartedStatus = false;
        public static bool fiscReceiptBegStatus = false;
        public static bool docBegStatus = false;
        public static bool operRegStatus = false;
        public static bool outOfPaperStatus = false;
        public static bool blockedDueTo72 = false;
        public static bool blockedDueTo24 = false;
        public static bool lowPaperStatus = false;
        public static bool customerDisplConnectionErr = false;

        byte[] dataReceived;

        public uint daySum = 0;

        public delegate void StatusChanged(int status);
        public event StatusChanged StatusChangedEvent;

        public int Connect(string port)
        {
            printer = new ComPortT(); 
            return printer.Connect(port);
        }

        /// <summary>
        /// 21h (33) ОЧИСТКА ДИСПЛЕЯ
        /// </summary>
        /// <returns></returns>
        public int ClearDisplay()
        {
            byte cmd = 0x21;
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 23h (35) ОТОБРАЖЕНИЕ ТЕКСТА НА ДИСПЛЕЕ
        /// </summary>
        /// <returns></returns>
        public int TextToDisplay(string text)
        {
            byte cmd = 0x23;
            byte[] bText = Encoding.ASCII.GetBytes(text);
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            outMessage.AddRange(bText);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 24H (36) НАСТРОЙКИ ETHERNET
        /// </summary>
        /// <returns></returns>
        public int SetupEthernet()
        {
            return 0;
        }

        /// <summary>
        /// 26h (38) ОТКРЫТИЕ НЕФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int OpenDoc()
        {
            byte cmd = 0x26;
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 27h (39) ЗАКРЫТИЕ НЕФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int CloseDoc()
        {
            byte cmd = 0x27;
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 2Ah (42) ПЕЧАТЬ НЕФИСКАЛЬНОГО КОМЕНТАРИЯ
        /// </summary>
        /// <returns></returns>
        public int PrintText(string text)
        {
            byte cmd = 0x2A;
            byte[] bText = Encoding.ASCII.GetBytes(text);
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            outMessage.AddRange(bText);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 2Ch (44) ПРОТЯЖКА БУМАГИ
        /// </summary>
        /// <returns></returns>
        public int PaperOut()
        {
            byte cmd = 0x2C;
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 2Dh (45) ОБРЕЗКА БУМАГИ
        /// </summary>
        /// <returns></returns>
        public int CutPaper()
        {
            byte cmd = 0x2D;
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 30h (48) ОТКРЫТИЕ ФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int OpenReceipt(byte OpCode, byte[] OpPwd, byte TillNmb)
        {
            byte cmd = 0x30;
            List<byte> outMessage = new List<byte>();
            outMessage.Add(cmd);
            outMessage.Add(OpCode);
            outMessage.Add(0x2C);
            outMessage.AddRange(OpPwd);
            outMessage.Add(0x2C);
            outMessage.Add(TillNmb);
            return printer.SendData(outMessage.ToArray());
        }

        /// <summary>
        /// 34h (52) РЕГИСТРАЦИЯ ПРОДАЖИ И ОТОБРАЖЕНИЕ НА ДИСПЛЕЕ
        /// </summary>
        /// <returns></returns>
        public int SaleAndDisplay()
        {
            return 0;
        }

        /// <summary>
        /// 35h (53) ИТОГ («ВСЕГО»)
        /// </summary>
        /// <returns></returns>
        public int Total()
        {
            return 0;
        }

        /// <summary>
        /// 37h (55) ОПЛАТА И ПОДТВЕРЖДЕНИЕ ЗАКРЫТИЯ ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int PayAndCloseRecipt()
        {
            return 0;
        }

        /// <summary>
        /// 36h (54) ПЕЧАТЬ ПРОИЗВОЛЬНОГО ФИСКАЛЬНОГО ТЕКСТА (КОМЕНТАРИЯ)
        /// </summary>
        /// <returns></returns>
        public int PrintFiscalText()
        {
            return 0;
        }

        /// <summary>
        /// 38h (56) ЗАКРЫТИЕ ФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int CloseReceipt()
        {
            return 0;
        }

        /// <summary>
        /// 39h (57) АННУЛИРОВАНИЕ ФИСКАЛЬНОГО ЧЕКА
        /// </summary>
        /// <returns></returns>
        public int VoidReceipt()
        {
            return 0;
        }

        /// <summary>
        /// 3Ah(58) ПРОДАЖА АРТИКУЛА
        /// </summary>
        /// <returns></returns>
        public int SaleArticle()
        {
            return 0;
        }

        /// <summary>
        /// 3Dh (61) УСТАНОВКА ДАТЫ/ВРЕМЕНИ
        /// </summary>
        /// <returns></returns>
        public int SetDateTime()
        {
            return 0;
        }

        /// <summary>
        /// 3Eh (62) ЗАПРОС ДАТЫ/ВРЕМЕНИ
        /// </summary>
        /// <returns></returns>
        public int RequestDateTime()
        {
            return 0;
        }

        /// <summary>
        /// 3Fh (63) ОТОБРАЖЕНИЕ ДАТЫ/ВРЕМЕНИ НА ДИСПЛЕЕ
        /// </summary>
        /// <returns></returns>
        public int DisplayDateTime()
        {
            return 0;
        }

        /// <summary>
        /// 46h (70) СЛУЖЕБНЫЙ ВНОС/ВЫДАЧА
        /// </summary>
        /// <returns></returns>
        public int CashInOut()
        {
            return 0;
        }

        /// <summary>
        /// 4Ah (74) СТАТУС РЕГИСТРАТОРА
        /// </summary>
        /// <returns></returns>
        public int PrinterStatus()
        {
            return 0;
        }

        /// <summary>
        /// 55h (85) ВОЗВРАТНЫЙ ЧЕК
        /// </summary>
        /// <returns></returns>
        public int Return()
        {
            return 0;
        }

        /// <summary>
        /// 6Dh (109) ПЕЧАТЬ КОПИИ ЧЕКОВ
        /// </summary>
        /// <returns></returns>
        public int CopyReceipt()
        {
            return 0;
        }

        /// <summary>
        /// 6Fh (111) ОТЧЕТ ПО АРТИКУЛАМ
        /// </summary>
        /// <returns></returns>
        public int ArticleReport()
        {
            return 0;
        }

        /// <summary>
        /// 5Dh (93) ПЕЧАТЬ РАЗДЕЛИТЕЛЬНОЙ ЛИНИИ
        /// </summary>
        /// <returns></returns>
        public int PrintCuttingLine()
        {
            return 0;
        }

    }
}
