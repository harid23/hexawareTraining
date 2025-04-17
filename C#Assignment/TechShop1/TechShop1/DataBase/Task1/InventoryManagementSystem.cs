using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task1

{
    public class InventoryManagementSystem
    {
        public static void AddInventory()
        {
            SqlConnection con = DatabaseConnector.getConnection();

            Console.Write("Enter Product ID: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Quantity In Stock: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            string query = "insert into Inventory (ProductID, QuantityInStock, LastStockUpdate) " +
                           "values (@ProductID, @QuantityInStock, @LastStockUpdate)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productId);
            cmd.Parameters.AddWithValue("@QuantityInStock", quantity);
            cmd.Parameters.AddWithValue("@LastStockUpdate", DateTime.Now);

            int rows = cmd.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ? "Inventory added successfully." : "Failed to add inventory.");
        }

        public static void UpdateInventory()
        {
            SqlConnection con = DatabaseConnector.getConnection();

            Console.Write("Enter Product ID to update stock: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter New Quantity In Stock: ");
            int newQuantity = Convert.ToInt32(Console.ReadLine());

            string query = "update Inventory set QuantityInStock = @QuantityInStock, LastStockUpdate = @LastStockUpdate " +
                           "where ProductID = @ProductID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productId);
            cmd.Parameters.AddWithValue("@QuantityInStock", newQuantity);
            cmd.Parameters.AddWithValue("@LastStockUpdate", DateTime.Now);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Inventory updated successfully." : "Product not found in inventory.");
        }

        public static void RemoveDiscontinuedInventory()
        {
            SqlConnection con = DatabaseConnector.getConnection();

            Console.Write("Enter Product ID to discontinue: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            string query = "delete from Inventory where ProductID = @ProductID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Inventory removed successfully." : "Product not found in inventory.");
        }
    }
}