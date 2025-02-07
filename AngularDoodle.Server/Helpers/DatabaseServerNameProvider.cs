namespace AdvisoryShowcase.Helpers
{
    public static class DatabaseServerNameProvider
    {
        private const string DevServerName = "AIT-DSQL-KEB\\DSTAN01";
        private const string TestServerName = "AIT-TSQL-KEB\\TSTAN01";
        private const string ProdServerName = "AIT-PSQL-KEB\\PSTAN01";

        public static string GetServerName()
        {
            string machineName = Environment.MachineName;

            switch (machineName)
            {
                case "AIT-TSQL-EGN01":
                case "AIT-TIIS01":
                    return TestServerName;
                case "AIT-PSQL-EGN01":
                case "AIT-PIIS01":
                    return ProdServerName;
                default:
                    return DevServerName;
            }
        }

    }
}