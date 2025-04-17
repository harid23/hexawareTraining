using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task2
{
    public class ProductCatalogManager : IProductCatalogManager
    {

        public void AddProduct()
        {
            SqlConnection con = DatabaseConnector.getConnection();
            string ProductName, Description, Price, category;
            Console.Write("Enter The ProductName = ");
            ProductName = Console.ReadLine();
            Console.Write("Enter The Description = ");
            Description = Console.ReadLine();
            Console.Write("Enter The Price = ");
            Price = Console.ReadLine();
            Console.Write("Enter The category = ");
            category = Console.ReadLine();

            string checkQuery = "select count(*) from products where ProductName = @ProductName";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@ProductName", ProductName);
            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                Console.WriteLine("Product already exists in the catalog.");
                return;
            }


            string query = "insert into products(ProductName,Description,Price,category)values(@ProductName,@Description,@Price,@category)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("ProductName", ProductName);
            cmd.Parameters.AddWithValue("Description", Description);
            cmd.Parameters.AddWithValue("Price", Price);
            cmd.Parameters.AddWithValue("category", category);

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                Console.WriteLine("Record added successfully..");
            }
            else { Console.WriteLine("Unable to add a record .."); }
        }

        public void UpdateProduct()
        {
            SqlConnection con = DatabaseConnector.getConnection();
            Console.Write("Enter the ProductID that need to be Updated");
            int ProductID = Convert.ToInt32(Console.ReadLine());
            string query = "update products set ProductName = @name, Description = @desc, Price = @price,category = @category WHERE ProductID = @ProductID";
            string name, desc, price, category;
            Console.Write("Enter The ProductName = ");
            name = Console.ReadLine();
            Console.Write("Enter The Description = ");
            desc = Console.ReadLine();
            Console.Write("Enter The Price = ");
            price = Console.ReadLine();
            Console.Write("Enter The category = ");
            category = Console.ReadLine();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", ProductID);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@category", category);
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0) { Console.WriteLine("Product updated successfully."); }

            else { Console.WriteLine("Product ID not found."); }

        }
    }
}