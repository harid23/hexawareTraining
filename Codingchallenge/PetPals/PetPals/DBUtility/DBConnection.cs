using System.Data.SqlClient;

namespace DBUtility
{
    public static class DBConnection
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
