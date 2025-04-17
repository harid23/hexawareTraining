using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1;
using TechShop1.Exceptions;

namespace TechShop1.Task1
{
    public class Customers
    {

       
        private int _CustomerId;
        private string _FirstName;
        private string _LastName;
        private string _Email;
        private string _Phone;
        private string _Address;
        public int CustomerId
        {
            get { return _CustomerId; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("CustomerId can't be 0 or negative");
                }
                _CustomerId = value;
            }
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get { return _Email; }
            set
            {
                if (!isValidEmail(value))
                {
                    throw new InValidDataException("Invalid Email Please enter a correct email");
                }

                _Email = value;
            }
        }

        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (!isValidPhone(value))
                {
                    throw new InValidDataException("Invalid Number Please enter a correct Number");
                }
                _Phone = value;
            }
        }

        public string Address
        {
            get;
            set;
        }

        public Customers(int CustomerID, string FirstName, string LastName, string Email, string Phone, string Address)
        {
            if (!isValidEmail(Email))
            {
                throw new InValidDataException("Invalid Email Please Enter a Valid Email");
            }

            if (!isValidPhone(Phone))
            {
                throw new InValidDataException("Invalid PhoneNumber Please Enter a Valid Number");
            }

            _CustomerId = CustomerID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            _Address = Address;
        }


        private List<Orders> _orders = new List<Orders>();


        public List<Orders> Orders
        {
            get { return _orders; } 
        }

        //Methods
        public void GetCustomerDetails()
        {
            Console.WriteLine("Customer Details:");
            Console.WriteLine($"Name     : {FirstName} {LastName}");
            Console.WriteLine($"Email    : {Email}");
            Console.WriteLine($"Phone    : {Phone}");
            Console.WriteLine($"Address  : {Address}");
        }

        public int CalculateTotalOrders()
        {
            return _orders.Count;
        }

        public void UpdateCustomerInfo()
        {
            Console.WriteLine($"Updating info for Customer ID: {CustomerId} - {FirstName} {LastName}");
            Console.Write("Enter new Email (leave blank to keep current): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                Email = newEmail;
            }
            Console.Write("Enter new Phone (leave blank to keep current): ");
            string newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone))
            {
                Phone = newPhone;
            }
            Console.Write("Enter new Address (leave blank to keep current): ");
            string newAddress = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAddress))
            {
                Address = newAddress;
            }
            Console.WriteLine("Customer information updated successfully.");
        }


        public static bool isValidEmail(string email)
        {
            if (!email.Contains("@"))
            {
                return false;
            }

            if (email[0] == '@')
            {
                return false;
            }

            if (email[email.Length - 1] == '@')
            {
                return false;
            }

            return true;
        }

        public static bool isValidPhone(string phone)
        {
            if (!(phone.Length == 10))
            {
                return false;
            }
            foreach (char c in phone)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            // sample customer
            Customers customer = new Customers(1, "Sudharsan", "M", "sudharsan@gmail.com", "7604875003", "Kolathur, Chennai");

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== CUSTOMER METHOD MENU ===");
                Console.WriteLine("1. View Customer Details");
                Console.WriteLine("2. Update Customer Info");
                Console.WriteLine("3. View Total Orders");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        customer.GetCustomerDetails();
                        break;

                    case "2":
                        try
                        {
                            customer.UpdateCustomerInfo();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case "3":
                        int total = customer.CalculateTotalOrders();
                        Console.WriteLine($"Total Orders: {total}");
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine(" Invalid choice. Please try again.");
                        break;
                }
            }
        }

        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        Customers sonu = new Customers(1, "Hari", "m", "Hariharan", "7604875003", "kolathur");
        //    }
        //    catch (InValidDataException ex)
        //    {
        //        Console.WriteLine("Error --- " + ex.Message);
        //    }
        //}

    }

}
