using System;
using System.Collections.Generic;
using TechShop1.Exceptions;
using TechShop1.Collections;
namespace TechShop1.Mains.Task6
{
    class OM_MAIN
    {
        static void Main(string[] args)
        {
            OrderManager orderManager = new OrderManager();
            bool running = true;
            Products product1 = new Products(1, "Laptop", "Gaming Laptop", 75000, 10);
            Products product2 = new Products(2, "Mouse", "Wireless Mouse", 1500, 5);

            orderManager.AddInventory(new Inventory(1, product1, 10));
            orderManager.AddInventory(new Inventory(2, product2, 15));

            List<Products> productCatalog = new List<Products> { product1, product2 };




            while (running)
            {
                Console.WriteLine("\n===== TechShop Order Management =====");
                Console.WriteLine("1. View All Orders");
                Console.WriteLine("2. Add New Order ");
                Console.WriteLine("3. Cancel Order");
                Console.WriteLine("4. Update Order Status");
                Console.WriteLine("5. Sort Orders by Date (Ascending)");
                Console.WriteLine("6. Sort Orders by Date (Descending)");
                Console.WriteLine("7. Display Orders by Date Range");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        orderManager.DisplayAllOrders();
                        break;

                    case "2":
                        try
                        {
                            Console.WriteLine("Available Products:");

                            foreach (var p in productCatalog)
                            {
                                Console.WriteLine($"ID: {p.ProductID}, Name: {p.ProductName}, Price: {p.Price}, Stock: {p.StockInQuantity}");
                            }

                            Console.Write("Enter Order ID: ");
                            int orderId = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Product ID to order: ");
                            int selectedProductId = Convert.ToInt32(Console.ReadLine());

                            Products selectedProduct = null;

                            foreach (Products p in productCatalog)
                            {
                                if (p.ProductID == selectedProductId)
                                {
                                    selectedProduct = p;
                                    break;
                                }
                            }

                            if (selectedProduct == null)
                            {
                                Console.WriteLine("Invalid Product ID.");
                                break;
                            }

                            Console.Write("Enter Quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());

                            Inventory item = orderManager.FindInventoryItem(selectedProduct.ProductID);
                            if (item == null || item.QuantityInStock < quantity)
                            {
                                Console.WriteLine("Not enough stock available.");
                                break;
                            }

                            // Create order detail and order
                            OrderDetails orderDetail = new OrderDetails(selectedProduct, quantity);
                            List<OrderDetails> orderDetailsList = new List<OrderDetails> { orderDetail };

                            Orders newOrder = new Orders(
                                orderId,
                                DateTime.Now,
                                "Pending",
                                orderDetailsList,
                                (decimal)selectedProduct.Price * quantity
                            );

                            orderManager.AddOrder(newOrder);

                            Console.WriteLine("Order successfully added!");
                        }
                        catch (InsufficientStockException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }
                        break;
                    case "3":
                        Console.Write("Enter Order ID to cancel: ");
                        int cancelId = Convert.ToInt32(Console.ReadLine());
                        orderManager.CancelOrder(cancelId);
                        break;

                    case "4":
                        Console.Write("Enter Order ID to update: ");
                        int updateId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter new status: ");
                        string newStatus = Console.ReadLine();
                        orderManager.UpdateOrderStatus(updateId, newStatus);
                        break;

                    case "5":
                        orderManager.SortOrdersByDateAscending();
                        break;

                    case "6":
                        orderManager.SortOrdersByDateDescending();
                        break;

                    case "7":
                        Console.Write("Enter start date (yyyy-MM-dd): ");
                        DateTime start = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter end date (yyyy-MM-dd): ");
                        DateTime end = DateTime.Parse(Console.ReadLine());
                        orderManager.DisplayOrdersByDateRange(start, end);
                        break;

                    case "8":
                        running = false;
                        Console.WriteLine("Exiting program.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}