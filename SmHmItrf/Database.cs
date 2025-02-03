using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmHmItrf
{
    public class Database
    {
        public static bool Exec_SQL(string sql)
        {
            bool result = false;

            string connString = String.Format("Data Source=yourdatabase.db");
            SqliteConnection sqlite_conn = new SqliteConnection(connString);
            sqlite_conn.Open();

            SqliteCommand sqlite_cmd = new SqliteCommand(sql, sqlite_conn);
            try
            {
                sqlite_cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex) { result = false; }

            sqlite_conn.Close();
            return result;
        }
    }
}
