namespace MD_FDI_Exelio
{
    class StringValue
    {
        #region SQL
        //public const string SQLServerConnectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = PARK_DB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public const string SQLServerConnectionString = @"Data Source={0};Persist Security Info=True;User ID=qwert;Password=P@ran01d;TrustServerCertificate=False";
        //public const string SQLServerConnectionString = @"Data Source = " + "10.10.50.1" + "; Initial Catalog = PARK_DB; User ID = qwert; Password=P@ran01d";
        #endregion
    }
}
