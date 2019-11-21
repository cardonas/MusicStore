
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    internal class DBConnection
    {
        private static string connectionString =
            @"Data Source=localhost\sqlexpress;Initial Catalog=Music_Store;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
