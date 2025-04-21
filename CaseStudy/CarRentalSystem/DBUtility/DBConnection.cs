using System;
using System.Data;
using System.Data.SqlClient;

namespace CarRentalSystem.DBUtility
{
    public  class DBConnection
    {
        private SqlConnection con;
        private SqlCommand cmd;

        public DBConnection()
        {
            con = new SqlConnection("Data Source=DESKTOP-JJAJ96V\\SQLEXPRESS;Initial Catalog=CRSDB;Integrated Security=True;");
        }
        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}
