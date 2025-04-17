using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;
using TechShop1.Task1;
using TechShop1;

namespace TechShop1
{
    //Task 1,2,3 Main is below
   public  class Orders
    {
        //Private Fields
        private int _OrderId;
        private Customers _Customer; //Composition
        private DateTime _OrderDate;
        private decimal _TotalAmount;
        private List<OrderDetails> _orderDetails = new List<OrderDetails>(); // For products in the order
        private string _OrderStatus;

        //Public Properties with Encapsulation

        public List<OrderDetails> OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("OrderDetails list cannot be null.");
                _orderDetails = value;
            }
        }
        public int OrderId
        {
            get { return _OrderId; }
            set
            {
                if (value <= 0)
                    throw new InvalidDataException("Order ID must be greater than 0.");
                _OrderId = value;
            }
        }

        public Customers Customer
        {
            get { return _Customer; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Customer", "Customer cannot be null.");
                _Customer = value;
            }
        }

        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set
            {
                if (value > DateTime.Now)
                    throw new InvalidDataException("Order date cannot be in the future.");
                _OrderDate = value;
            }
        }

        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set
            {
                if (value < 0)
                    throw new InvalidDataException("Total amount cannot be negative.");
                _TotalAmount = value;
            }
        }

        public string OrderStatus
        {
            get { return _OrderStatus; }
            set
            {
                string[] array = { "Pending", "Confirmed", "Shipped", "Delivered", "Cancelled" };
                if (!(array.Contains(value)))
                {
                    throw new InvalidDataException("Enter a Proper Data");
                }
                _OrderStatus = value;
            }
        }


        //Constructor

        public Orders() { }
        public Orders(int orderId, Customers customer, DateTime orderDate)
        {
            _OrderId = orderId;
            _Customer = customer;
            _OrderDate = orderDate;
            _OrderStatus = "Pending";
        }

        public Orders(int orderId, DateTime orderDate, string orderStatus, List<OrderDetails> orderDetails, decimal totalAmount)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
            OrderDetails = orderDetails;
            TotalAmount = totalAmount;
        }




        //Methods
        public void CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (var item in _orderDetails)
            {
                total += (int)item.CalculateSubtotal();
            }
            TotalAmount = total;
            Console.WriteLine($"Total Amount is == {TotalAmount}");
        }

        public void GetOrderDetails()
        {
            Console.WriteLine($"Order ID: {OrderId}");
            Console.WriteLine($"Customer: {_Customer.FirstName} {_Customer.LastName}");
            Console.WriteLine($"Date: {OrderDate}");
            Console.WriteLine($"Status: {OrderStatus}");
            Console.WriteLine("Order Items:");
            foreach (var item in _orderDetails)
            {
                Console.WriteLine(item.GetOrderDetailInfo());
            }
            Console.WriteLine($"Total: ${TotalAmount}");
        }

        public void CancelOrder()
        {
            foreach (var detail in _orderDetails)
            {
                detail.Product.StockInQuantity += detail.Quantity;
            }

            _orderDetails.Clear();
            TotalAmount = 0;
            OrderStatus = "Cancelled";
            Console.WriteLine("Order has been cancelled and stock has been adjusted.");
        }

        public void UpdateOrderStatus(string newStatus)
        {
            string[] status = { "Pending", "Confirmed", "Shipped", "Delivered", "Cancelled" };
            if (!(status.Contains(newStatus)))
            {
                throw new InvalidDataException("Error : Enter a Valid Status");
            }
            OrderStatus = newStatus;
            Console.WriteLine($"Order status updated to: {OrderStatus}");
        }

        static void Main(string[] args)
        {
            Customers customer = new Customers(1, "Hari", "D", "hari@gmail.com", "1234567890", "kolathur");
            Products laptop = new Products(1001, "Laptop", "Gaming Laptop", 1200.50, 10);
            Products mouse = new Products(1002, "Wireless Mouse", "Bluetooth mouse", 25.99, 30);

            Orders order = new Orders(1, customer, DateTime.Now);

            OrderDetails detail1 = new OrderDetails(1, order, laptop, 1);
            OrderDetails detail2 = new OrderDetails(2, order, mouse, 2);

            order.OrderDetails.Add(detail1);
            order.OrderDetails.Add(detail2);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== ORDER MENU ===");
                Console.WriteLine("1. View Order Details");
                Console.WriteLine("2. Calculate Total Amount");
                Console.WriteLine("3. Update Order Status");
                Console.WriteLine("4. Cancel Order");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        order.GetOrderDetails();
                        break;

                    case "2":
                        order.CalculateTotalAmount();
                        Console.WriteLine("Total amount calculated.");
                        break;

                    case "3":
                        Console.Write("Enter new order status (Pending, Confirmed, Shipped, Delivered, Cancelled): ");
                        string status = Console.ReadLine();
                        try
                        {
                            order.UpdateOrderStatus(status);
                        }
                        catch (InvalidDataException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case "4":
                        order.CancelOrder();
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}