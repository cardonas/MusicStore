using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal class DbConnection
    {
        private static string _connectionString =
            @"Data Source=localhost\sqlexpress;Initial Catalog=Music_Store;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            return conn;
        }
    }
}
