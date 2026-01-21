using System;
using Oracle.ManagedDataAccess.Client;
using Com.Wipro.Library.Util;
using Com.Wipro.Library.Bean;

namespace Com.Wipro.Library.Dao
{
    public class LibraryDAO
    {
        public bool ValidateBook(string bookID)
        {
            using OracleConnection con = DBUtil.GetDBConnection();
            string query =
                "SELECT COUNT(*) FROM LIB3980612345.BOOK_TBL WHERE Book_ID = :bid";

            using OracleCommand cmd = new OracleCommand(query, con);
            cmd.Parameters.Add("bid", bookID.Trim().ToUpper());

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool CheckBookAvailability(string bookID)
        {
            using OracleConnection con = DBUtil.GetDBConnection();
            string query =
                "SELECT Availability FROM LIB3980612345.BOOK_TBL WHERE Book_ID = :bid";

            using OracleCommand cmd = new OracleCommand(query, con);
            cmd.Parameters.Add("bid", bookID.Trim().ToUpper());

            return cmd.ExecuteScalar()?.ToString() == "Available";
        }

        public void ViewBook(string bookID)
        {
            using OracleConnection con = DBUtil.GetDBConnection();
            string query =
                "SELECT Book_Title, Author, Availability " +
                "FROM LIB3980612345.BOOK_TBL WHERE Book_ID = :bid";

            using OracleCommand cmd = new OracleCommand(query, con);
            cmd.Parameters.Add("bid", bookID.Trim().ToUpper());

            using OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Console.WriteLine("\n--- BOOK DETAILS ---");
                Console.WriteLine("Title       : " + dr.GetString(0));
                Console.WriteLine("Author      : " + dr.GetString(1));
                Console.WriteLine("Availability: " + dr.GetString(2));
            }
            else
            {
                Console.WriteLine("INVALID BOOK ID");
            }
        }

        public bool IssueBook(BookIssueBean bean)
        {
            using OracleConnection con = DBUtil.GetDBConnection();

            string insertQuery =
                "INSERT INTO LIB3980612345.BOOK_ISSUE_TBL " +
                "VALUES (:iid, :bid, :mname, SYSDATE)";

            using OracleCommand cmd = new OracleCommand(insertQuery, con);
            cmd.Parameters.Add("iid", new Random().Next(3000, 9999));
            cmd.Parameters.Add("bid", bean.BookID.Trim().ToUpper());
            cmd.Parameters.Add("mname", bean.MemberName);

            int rows = cmd.ExecuteNonQuery();

            string updateQuery =
                "UPDATE LIB3980612345.BOOK_TBL " +
                "SET Availability='Issued' WHERE Book_ID=:bid";

            using OracleCommand cmd2 = new OracleCommand(updateQuery, con);
            cmd2.Parameters.Add("bid", bean.BookID.Trim().ToUpper());
            cmd2.ExecuteNonQuery();

            return rows > 0;
        }
    }
}
