using System;
using System.Data.SqlClient;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task1
{
    public class ProductSearch
    {
        public static void SearchAndRecommend()
        {
            SqlConnection con = DatabaseConnector.getConnection();

            Console.WriteLine("Search Products by:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Category");
            Console.Write("Enter your choice (1 or 2): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            string query = "";
            SqlCommand cmd = null;

            if (choice == 1)
            {
                Console.Write("Enter product name : ");
                string name = Console.ReadLine();
                query = "select * from Products where ProductName = @name";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);
            }
            else if (choice == 2)
            {
                Console.Write("Enter category: ");
                Console.WriteLine("Available Categories: Computing Devices, Wearable Tech, HeadSet, Entertainment, Accessories, Virtual Reality, Lighting");
                string category = Console.ReadLine();
                query = "select * from Products where Category = @category";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@category", category);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("\n--- Matching Products ---");
            while (dr.Read())
            {
                Console.WriteLine($"\nProduct ID: {dr[0]} \nName: {dr[1]} \nDescription : {dr[2]} \nPrice: ${dr[3]} \nCategory: {dr[4]}");
            }

            dr.Close();

            // Optional
            if (choice == 1)
            {
                Console.WriteLine("\nWant recommendations from a specific category? (yes/no)");
                string wantRec = Console.ReadLine().ToLower();
                if (wantRec == "yes")
                {
                    Console.WriteLine("Available Categories: Computing Devices, Wearable Tech, HeadSet, Entertainment, Accessories, Virtual Reality, Lighting");
                    Console.Write("Enter category for recommendations: ");
                    string catRec = Console.ReadLine();

                    string recQuery = "select  * from Products WHERE Category = @catRec";
                    SqlCommand recCmd = new SqlCommand(recQuery, con);
                    recCmd.Parameters.AddWithValue("@catRec", catRec);

                    SqlDataReader dr1 = recCmd.ExecuteReader();

                    Console.WriteLine("\n--- Recommended Products ---");
                    while (dr1.Read())
                    {
                        Console.WriteLine($"\nProduct ID: {dr1[0]} \nName: {dr1[1]} \nDescription : {dr1[2]} \nPrice: ${dr1[3]} \nCategory: {dr1[4]}");
                        Console.WriteLine("--------------------");
                    }
                    dr.Close();
                }
            }

            con.Close();
        }
    }
}