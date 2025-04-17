using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TechShop1.DataBase.Task1;
using TechShop1.DataBase.Task2;


namespace TechShop1.Mains
{
    public class DatabaseConnector
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataReader dr;

        public static SqlConnection getConnection()
        {
            con = new SqlConnection("data source = DESKTOP-JJAJ96V\\SQLEXPRESS;initial catalog = TechShop;integrated security = true;");
            con.Open();
            return con;
        }

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("DataBase Connectivity Related Methods");
                Console.WriteLine("1.(TASK 1)add Customer ");
                Console.WriteLine("2. (TASK 2)Add Products ");
                Console.WriteLine("3.(TASK 2) Update Products");
                Console.WriteLine("(TASK--4)4.See Order Status");
                Console.WriteLine("(TASK--5)5.Add To Inventory");
                Console.WriteLine("(TASK--5)6.Update Inventory");
                Console.WriteLine("(TASK--5)7.Delete From Inventory");
                Console.WriteLine("(TASK--6)8.Generate Sale Report Based on Category");
                Console.WriteLine("(TASK--7)9.Update Customer Details");
                Console.WriteLine("(TASK--8)10.Payment Processing -- Pay The Amount");
                Console.WriteLine("(TASK--9)11.Product Searching ");
                Console.WriteLine("(TASK--3)12.Place a Order");
                Console.WriteLine("13.Exit");
                Console.Write("Enter Your Choice = ");
                string choice = Console.ReadLine();

                

            }
        }

    }

}