
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task1
{
    public class TrackingOrderStatus
    {
        public static void SeeOrderStatus()
        {
            DatabaseConnector.con = DatabaseConnector.getConnection();
            Console.Write("Enter your CustomerID = ");
            int CustomerID = Convert.ToInt32(Console.ReadLine());
            string query = "select * from orders where CustomerID = @CustomerID";
            DatabaseConnector.cmd = new SqlCommand(query, DatabaseConnector.con);

            DatabaseConnector.cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            DatabaseConnector.dr = DatabaseConnector.cmd.ExecuteReader();


            if (!DatabaseConnector.dr.HasRows)
            {
                Console.WriteLine("No orders found for this customer.");
                return;
            }
            while (DatabaseConnector.dr.Read())
            {
                Console.WriteLine($" OrderId=={DatabaseConnector.dr[0]} \n CustomerId=={DatabaseConnector.dr[1]} \n orderdate=={DatabaseConnector.dr[2]} \n TotalAmount=={DatabaseConnector.dr[3]} \n status=={DatabaseConnector.dr[4]} ");
            }
        }
    }
}
