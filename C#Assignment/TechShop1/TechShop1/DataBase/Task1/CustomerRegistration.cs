using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Mains;

namespace TechShop1.DataBase.Task1
{
    class CustomerRegistration
    {
        public static void AddCustomer()
        {
            DatabaseConnector.con = DatabaseConnector.getConnection();
            string FirstName, LastName, Email, Phone, Address;
            Console.Write("Enter The FirstName = ");
            FirstName = Console.ReadLine();
            Console.Write("Enter The LastName = ");
            LastName = Console.ReadLine();
            Console.Write("Enter The Email = ");
            Email = Console.ReadLine();
            Console.Write("Enter The Phone = ");
            Phone = Console.ReadLine();
            Console.Write("Enter The Address = ");
            Address = Console.ReadLine();

            DatabaseConnector.cmd = new SqlCommand("insert into Customers(FirstName, LastName, Email, Phone, [Address]) values (@FirstName,@LastName,@Email,@Phone,@Address)", DatabaseConnector.con);

            DatabaseConnector.cmd.Parameters.AddWithValue("FirstName", FirstName);
            DatabaseConnector.cmd.Parameters.AddWithValue("LastName", LastName);
            DatabaseConnector.cmd.Parameters.AddWithValue("Email", Email);
            DatabaseConnector.cmd.Parameters.AddWithValue("Phone", Phone);
            DatabaseConnector.cmd.Parameters.AddWithValue("Address", Address);

            int rows = DatabaseConnector.cmd.ExecuteNonQuery();
            if (rows > 0)
            {
                Console.WriteLine("Record added successfully..");
            }
            else { Console.WriteLine("Unable to add a record .."); }

        }
    }
}