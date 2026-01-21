using Oracle.ManagedDataAccess.Client;

namespace Com.Wipro.Library.Util
{
    public class DBUtil
    {
        public static OracleConnection GetDBConnection()
        {
            string connStr =
                "User Id=LIB3980612345;" +
                "Password=LIB3980612345;" +
                "Data Source=localhost:1521/XE";

            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            return con;
        }
    }

    public class BookNotAvailableException : System.Exception
    {
        public override string ToString()
        {
            return "BOOK NOT AVAILABLE";
        }
    }
}
