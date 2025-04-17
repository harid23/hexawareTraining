using System;
using System.Data.SqlClient;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task1
{
    public class OrderProcessor
    {
        public static void PlaceOrder()
        {
            SqlConnection con = DatabaseConnector.getConnection();

            Console.Write("Enter CustomerID: ");
            int customerId = Convert.ToInt32(Console.ReadLine());

            DateTime orderDate = DateTime.Now;
            decimal totalAmount = 0;

            string insertOrderQuery = "insert into Orders (CustomerID, OrderDate, TotalAmount)VALUES (@customerId, @orderDate, @totalAmount)";
            SqlCommand insertOrderCmd = new SqlCommand(insertOrderQuery, con);
            insertOrderCmd.Parameters.AddWithValue("@customerId", customerId);
            insertOrderCmd.Parameters.AddWithValue("@orderDate", orderDate);
            insertOrderCmd.Parameters.AddWithValue("@totalAmount", totalAmount);

           
            SqlCommand idCmd = new SqlCommand("SELECT MAX(OrderID) FROM Orders", con);
            int orderId = (int)idCmd.ExecuteScalar();

            bool addingProducts = true;

            while (addingProducts)
            {
                Console.Write("Enter ProductID to order: ");
                int productId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                string getPriceQuery = "select Price from Products where ProductID = @productId";
                SqlCommand priceCmd = new SqlCommand(getPriceQuery, con);
                priceCmd.Parameters.AddWithValue("@productId", productId);
                decimal price = (decimal)priceCmd.ExecuteScalar();

          
                decimal subtotal = price * quantity;
                totalAmount += subtotal;

                string insertDetailQuery = "insert into OrderDetails (OrderID, ProductID, Quantity) values (@orderId, @productId,@quantity)";
                SqlCommand detailCmd = new SqlCommand(insertDetailQuery, con);
                detailCmd.Parameters.AddWithValue("@orderId", orderId);
                detailCmd.Parameters.AddWithValue("@productId", productId);
                detailCmd.Parameters.AddWithValue("@quantity", quantity);
                detailCmd.ExecuteNonQuery();    
                string updateInventoryQuery = "update Inventory set QuantityInStock = QuantityInStock - @quantity WHERE ProductID = @productId";
                SqlCommand invCmd = new SqlCommand(updateInventoryQuery, con);
                invCmd.Parameters.AddWithValue("@quantity", quantity);
                invCmd.Parameters.AddWithValue("@productId", productId);
                invCmd.ExecuteNonQuery();

                Console.Write("Add another product? (yes/no): ");
                string s = Console.ReadLine().ToLower();
                if (!(s == "yes"))
                {
                    addingProducts = false;
                }
            }
            string updateTotalQuery = "update Orders SET TotalAmount = @total WHERE OrderID = @orderId";
            SqlCommand updateCmd = new SqlCommand(updateTotalQuery, con);
            updateCmd.Parameters.AddWithValue("@total", totalAmount);
            updateCmd.Parameters.AddWithValue("@orderId", orderId);
            updateCmd.ExecuteNonQuery();

            Console.WriteLine($"\nOrder #{orderId} placed successfully! Total Amount: ${totalAmount}");
            Console.WriteLine("-------------------");
            con.Close();
        }
    }
}