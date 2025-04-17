
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Collections;
using TechShop1.Exceptions;

namespace TechShop1.Mains.Task6
{
    class IM_Main
    {
        static void Main(string[] args)
        {
            InventoryManager manager = new InventoryManager();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- TechShop Inventory Management ---");
                Console.WriteLine("1. Add Inventory Item");
                Console.WriteLine("2. Update Inventory on Order");
                Console.WriteLine("3. Remove Inventory Item");
                Console.WriteLine("4. Display All Inventory");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // Example Product
                        Products newProduct = new Products
                        {
                            ProductID = 101,
                            ProductName = "Gaming Laptop"
                        };

                        Inventory newItem = new Inventory(newProduct, 20, DateTime.Now);

                        manager.AddInventoryItem(newItem);
                        break;

                    case 2:
                        List<OrderDetails> orderList = new List<OrderDetails>
                        {
                            new OrderDetails
                            {
                                Product = new Products{ ProductID = 101, ProductName = "Gaming Laptop" },
                                Quantity = 2
                            }
                        };

                        try
                        {
                            manager.UpdateInventoryOnOrder(orderList);
                        }
                        catch (InsufficientStockException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Product ID to remove: ");
                        int removeId = Convert.ToInt32(Console.ReadLine());
                        manager.RemoveInventoryItem(removeId);
                        break;

                    case 4:
                        manager.DisplayAllInventory();
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            Console.WriteLine("Exiting TechShop Inventory Management.");
        }

    }
}
