using System.Runtime.Serialization.Formatters.Binary;

namespace MD_FDI_Exelio
{
    class Transaction
    {
        public string Path { get; set; }
        public uint amount { get; set; }
        public int type { get; set; }

        public Transaction(int type, uint amount)
        {
            Path = $@"C:\FiscalFolder\{FiscalDevice_APM.MachineID}MoneyFlow.dat";
            this.type = type;
            this.amount = amount;
        }
        public Transaction()
        {
            Path = $@"C:\FiscalFolder\{FiscalDevice_APM.MachineID}MoneyFlow.dat";
        }

        private void Send(uint amount)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, amount);
            }
        }

        public uint Get()
        {
            uint total = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(Path, System.IO.FileMode.OpenOrCreate))
                {
                    total = (uint)formatter.Deserialize(fs);
                }
            }
            catch
            {
                total = 0;
            }
            return total;
        }

        public void UpdateData(Transaction tr)
        {
            uint oldAmount = Get();
            uint newAmount = 0;
            if (tr.type == 1)
            {
                newAmount = oldAmount + tr.amount;
            }

            if (tr.type == 2)
            {
                newAmount = oldAmount;
            }

            if (tr.type == 3)
            {
                newAmount = oldAmount + tr.amount;
            }

            if ((tr.type == 4) && (oldAmount >= tr.amount))
            {
                newAmount = oldAmount - tr.amount;
            }

            Send(newAmount);
        }

        public void Set(uint amount)
        {
            Send(amount);
        }
    }
}
