
using System;
using System.Collections.Generic;
using TechShop1.Task1;
using TechShop1;
namespace TechShop1.Mains.Task4

{
    public class TaskFour
    {
        static void Main(string[] args)
        {
            List<Customers> customers = new List<Customers>();
            List<Products> products = new List<Products>();
            List<Inventory> inventories = new List<Inventory>();
            List<Orders> orders = new List<Orders>();
            List<Payment> payments = new List<Payment>();

            bool exit = false;


            while (!exit)
            {
                Console.WriteLine("\n==== TechShop Menu ====");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Add Inventory");
                Console.WriteLine("4. Place Order");
                Console.WriteLine("5. View All Orders");
                Console.WriteLine("6. Make Payment");
                Console.WriteLine("7. View Inventory");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter First Name: ");
                        string fname = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        string lname = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Phone: ");
                        string phone = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        string address = Console.ReadLine();

                        int customerId = customers.Count + 1;
                        Customers customer = new Customers(customerId, fname, lname, email, phone, address);
                        customers.Add(customer);
                        Console.WriteLine("Customer added successfully.");
                        Console.WriteLine("----------------");
                        break;

                    case "2":
                        Console.Write("Enter Product Name: ");
                        string pname = Console.ReadLine();
                        Console.Write("Enter Description: ");
                        string desc = Console.ReadLine();
                        Console.Write("Enter Price: ");
                        double price = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter Stock Quantity: ");
                        int stock = Convert.ToInt32(Console.ReadLine());

                        int pid = products.Count + 1;
                        Products product = new Products(pid, pname, desc, price, stock);
                        products.Add(product);
                        Console.WriteLine("Product added.");
                        Console.WriteLine("----------------");
                        break;

                    case "3":
                        Console.WriteLine("Available Products:");
                        for (int i = 0; i < products.Count; i++)
                        {
                            Console.WriteLine($"{products[i].ProductID}. {products[i].ProductName}");
                        }

                        Console.Write("Enter Product ID to add to inventory: ");
                        int selectedPid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter quantity: ");
                        int qty = Convert.ToInt32(Console.ReadLine());

                        Products foundProduct = null;
                        for (int i = 0; i < products.Count; i++)
                        {
                            if (products[i].ProductID == selectedPid)
                            {
                                foundProduct = products[i];
                                break;
                            }
                        }

                        if (foundProduct != null)
                        {
                            inventories.Add(new Inventory(inventories.Count + 1, foundProduct, qty));
                            Console.WriteLine("Inventory updated.");
                            Console.WriteLine("----------------");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Product ID.");
                            Console.WriteLine("----------------");
                        }
                        break;

                    case "4":
                        if (customers.Count == 0 || products.Count == 0)
                        {
                            Console.WriteLine("Add at least one customer and one product first.");
                            Console.WriteLine("----------------");
                            break;
                        }

                        Console.WriteLine("Customers:");
                        for (int i = 0; i < customers.Count; i++)
                        {
                            Console.WriteLine($"{customers[i].CustomerId}. {customers[i].FirstName} {customers[i].LastName}");
                        }
                        Console.Write("Enter Customer ID: ");
                        int custId = Convert.ToInt32(Console.ReadLine());

                        Customers selectedCustomer = null;
                        for (int i = 0; i < customers.Count; i++)
                        {
                            if (customers[i].CustomerId == custId)
                            {
                                selectedCustomer = customers[i];
                                break;
                            }
                        }

                        Orders order = new Orders(orders.Count + 1, selectedCustomer, DateTime.Now);

                        string addMore = "y";
                        while (addMore.ToLower() == "y")
                        {
                            Console.WriteLine("Products:");
                            for (int i = 0; i < products.Count; i++)
                            {
                                Console.WriteLine($"{products[i].ProductID}. {products[i].ProductName} - ${products[i].Price}");
                            }

                            Console.Write("Enter Product ID: ");
                            int orderPid = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Quantity: ");
                            int orderQty = Convert.ToInt32(Console.ReadLine());

                            Products orderProduct = null;
                            for (int i = 0; i < products.Count; i++)
                            {
                                if (products[i].ProductID == orderPid)
                                {
                                    orderProduct = products[i];
                                    break;
                                }
                            }

                            if (orderProduct != null && orderProduct.StockInQuantity >= orderQty)
                            {
                                OrderDetails od = new OrderDetails(order.OrderDetails.Count + 1, order, orderProduct, orderQty);
                                orderProduct.StockInQuantity -= orderQty;

                                Console.Write("Apply discount? (y/n): ");
                                if (Console.ReadLine().ToLower() == "y")
                                {
                                    Console.Write("Enter discount %: ");
                                    double disc = Convert.ToDouble(Console.ReadLine());
                                    od.AddDiscount(disc);
                                }

                                order.OrderDetails.Add(od);
                            }
                            else
                            {
                                Console.WriteLine("Product not available or insufficient stock.");
                            }

                            Console.Write("Add more items? (y/n): ");
                            addMore = Console.ReadLine();
                        }

                        order.CalculateTotalAmount();
                        orders.Add(order);
                        Console.WriteLine("Order placed.");
                        Console.WriteLine("-------------------------");
                        break;

                    case "5":
                        for (int i = 0; i < orders.Count; i++)
                        {
                            orders[i].GetOrderDetails();
                            Console.WriteLine("-----------------------------");
                        }
                        break;

                    case "6":
                        Console.WriteLine("Orders:");
                        for (int i = 0; i < orders.Count; i++)
                        {
                            Console.WriteLine($"Order {orders[i].OrderId} - ${orders[i].TotalAmount}");
                        }

                        Console.Write("Enter Order ID for payment: ");
                        int payId = Convert.ToInt32(Console.ReadLine());

                        Orders payOrder = null;
                        for (int i = 0; i < orders.Count; i++)
                        {
                            if (orders[i].OrderId == payId)
                            {
                                payOrder = orders[i];
                                break;
                            }
                        }

                        if (payOrder != null)
                        {
                            Console.Write("Payment Status (Pending/Completed): ");
                            string status = Console.ReadLine();
                            int paymentid = payments.Count + 1;
                            Payment payment = new Payment(paymentid, payOrder, (double)payOrder.TotalAmount, status, DateTime.Now);
                            payments.Add(payment);
                            Console.WriteLine(payment.GetPaymentDetails());
                        }
                        else
                        {
                            Console.WriteLine("Order not found.");
                            Console.WriteLine("------------------------------");
                        }
                        break;

                    case "7":
                        Inventory.ListAllProducts(inventories);
                        break;

                    case "8":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
