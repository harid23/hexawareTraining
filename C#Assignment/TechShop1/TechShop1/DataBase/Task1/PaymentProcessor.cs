using System;
using System.Data.SqlClient;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task1
{
    public class PaymentProcessor
    {
        public static void ProcessPayment()
        {
            SqlConnection con = DatabaseConnector.getConnection();

            Console.Write("Enter Order ID: ");
            int orderId = Convert.ToInt32(Console.ReadLine());

            // Check if Order Exists
            string checkQuery = "select count(*) from OrderDetails where OrderID = @orderId";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@orderId", orderId);

            int exists = (int)checkCmd.ExecuteScalar();
            if (exists == 0)
            {
                Console.WriteLine("Invalid Order ID. No such order found.");
                return;
            }

            // Check if already paid
            string statusQuery = "select PaymentStatus FROM OrderDetails where OrderID = @orderId";
            SqlCommand statusCmd = new SqlCommand(statusQuery, con);
            statusCmd.Parameters.AddWithValue("@orderId", orderId);

            string status = statusCmd.ExecuteScalar().ToString();
            if (status == "Completed")
            {
                Console.WriteLine("Payment has already been completed for this order.");
                return;
            }
            string calcQuery = "select sum(p.Price * od.Quantity) from OrderDetails od join Products p on od.ProductID = p.ProductID where od.OrderID = @orderId";
            SqlCommand calcCmd = new SqlCommand(calcQuery, con);
            calcCmd.Parameters.AddWithValue("@orderId", orderId);
            decimal amountPaid = Convert.ToDecimal(calcCmd.ExecuteScalar());
            Console.Write("Enter Payment Method (e.g., Card/UPI/Cash): ");
            string method = Console.ReadLine();

            string updateQuery = "update OrderDetails set AmountPaid = @amountPaid, PaymentStatus = 'Completed' WHERE OrderID = @orderId";

            SqlCommand updateCmd = new SqlCommand(updateQuery, con);
            updateCmd.Parameters.AddWithValue("@amountPaid", amountPaid);
            updateCmd.Parameters.AddWithValue("@orderId", orderId);

            int rows = updateCmd.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine($"Payment of ${amountPaid} via {method} recorded successfully for Orderid={orderId}.");
            }
            else
            {
                Console.WriteLine("Failed to update payment. Please try again.");
            }

            con.Close();
        }
    }
}