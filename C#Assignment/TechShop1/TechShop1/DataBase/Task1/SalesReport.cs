using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task1
{
    public class SalesReport
    {
        public static void GenerateSalesReportByCategory()
        {
            SqlConnection con = DatabaseConnector.getConnection();
            string query = "select p.category, sum(od.Quantity * p.Price) as TotalEarnings from OrderDetails od join Products p on od.ProductID = p.ProductID group by p.category";
            DatabaseConnector.cmd = new SqlCommand(query, DatabaseConnector.con);
            DatabaseConnector.dr = DatabaseConnector.cmd.ExecuteReader();

            if (!DatabaseConnector.dr.HasRows)
            {
                Console.WriteLine("No orders found for this customer.");
                return;
            }
            while (DatabaseConnector.dr.Read())
            {
                Console.WriteLine($" Category=={DatabaseConnector.dr[0]} \n TotalAmount=={DatabaseConnector.dr[1]}");
            }
        }
    }
}