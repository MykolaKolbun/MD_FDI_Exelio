using System;
using System.IO;
using System.Text;

namespace TestForm
{
    class LoggerT
    {
        /// <summary>
        /// Полный путь к лог файлу.
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// Конструктор класса Logger. Создает папку и файл.
        /// </summary>
        public LoggerT(string machineID)
        {
            FilePath = $@"C:\Log\{machineID}_FiscalTrace-{DateTime.Now:yyyy-MM-dd}.txt";
        }

        public void Write(string mess)
        {
            string dateTime = DateTime.Now.ToString();
            StreamWriter sw = new StreamWriter(FilePath, true, Encoding.UTF8);
            sw.WriteLine("{0}: {1}", dateTime, mess);
            sw.Close();
        }
    }
}
