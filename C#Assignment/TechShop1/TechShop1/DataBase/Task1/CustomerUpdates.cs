using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Mains;
using TechShop1.Task1;
using TechShop1.Exceptions;

namespace TechShop1.DataBase.Task1
{
    public class CustomerUpdates
    {
        public static void UpdateCustomerAccount()
        {
            SqlConnection con = DatabaseConnector.getConnection();

            Console.Write("Enter your CustomerID: ");
            int customerId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the field you want to update (Email/Phone): ");
            string field = Console.ReadLine();

            string newValue = "";

            if (field == "Email")
            {
                Console.Write("Enter new Email: ");
                newValue = Console.ReadLine();
                if (!Customers.isValidEmail(newValue))
                {
                    throw new InValidDataException("Invalid Email ! Enter Correct Email");

                }
            }
            else if (field == "Phone")
            {
                Console.Write("Enter new Phone Number: ");
                newValue = Console.ReadLine();
                if (!Customers.isValidPhone(newValue))
                {
                    throw new InValidDataException("Invalid Number enter a correct Number ");
                }
            }
            else
            {
                Console.WriteLine("Invalid field selected.");
                return;
            }

            string query = $"update Customers set {field} = @newValue where CustomerID =@customerId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@newValue", newValue);
            cmd.Parameters.AddWithValue("@customerId", customerId);

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine($"{field} updated successfully.");
            }
            else
            {
                Console.WriteLine("Customer ID not found or update failed.");
            }

            con.Close();
        }

    }
}