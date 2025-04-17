using System;
using System.Collections.Generic;
using TechShop1.Collections;
using TechShop1.Exceptions;

namespace TechShop1.Mains.Task6
{
    class PM_MAIN
    {
        static void Main(string[] args)
        {
            ProductManager manager = new ProductManager();
            List<Orders> existingOrders = new List<Orders>();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Product Manager Menu ===");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Remove Product");
                Console.WriteLine("4. View All Products");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Product ID: ");
                            int id = int.Parse(Console.ReadLine());
                            Console.Write("Enter Product Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Description: ");
                            string desc = Console.ReadLine();
                            Console.Write("Enter Price: ");
                            double price = double.Parse(Console.ReadLine());
                            Console.Write("Enter Stock Quantity: ");
                            int stock = int.Parse(Console.ReadLine());

                            Products newProduct = new Products(id, name, desc, price, stock);
                            manager.AddProduct(newProduct);
                            break;

                        case "2":
                            Console.Write("Enter Product ID to update: ");
                            int updateId = int.Parse(Console.ReadLine());
                            Console.Write("Enter New Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Enter New Description: ");
                            string newDesc = Console.ReadLine();
                            Console.Write("Enter New Price: ");
                            double newPrice = double.Parse(Console.ReadLine());
                            Console.Write("Enter New Stock Quantity: ");
                            int newStock = int.Parse(Console.ReadLine());

                            manager.UpdateProduct(updateId, newName, newDesc, newPrice, newStock);
                            break;

                        case "3":
                            Console.Write("Enter Product ID to remove: ");
                            int removeId = int.Parse(Console.ReadLine());
                            manager.RemoveProduct(removeId, existingOrders);
                            break;

                        case "4":
                            manager.ListAllProducts();
                            break;

                        case "5":
                            exit = true;
                            Console.WriteLine("Exiting Product Manager...");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select between 1-5.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}