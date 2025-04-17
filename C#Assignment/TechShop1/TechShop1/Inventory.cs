using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;
using TechShop1;

namespace TechShop1
{
   
    public class Inventory
    {
       
        private int _inventoryID;
        private Products _product;
        private int _quantityInStock;
        private DateTime _lastStockUpdate;

       
        public int InventoryID
        {
            get { return _inventoryID; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Inventory ID must be a positive integer.");
                }

                _inventoryID = value;
            }
        }

        public Products Product
        {
            get { return _product; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Product), "Product cannot be null.");
                }

                _product = value;
            }
        }

        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            {
                if (value <= 0)
                {
                    throw new InsufficientStockException("Quantity in stock cannot be zero and negative");
                }
                _quantityInStock = value;
                LastStockUpdate = DateTime.Now; // Automatically update stock time
            }
        }

        public DateTime LastStockUpdate
        {
            get { return _lastStockUpdate; }
            private set { _lastStockUpdate = value; }
        }

        // Constructor

        public Inventory(Products product, int quantityInStock)
        {
            _product = product;
            _quantityInStock = quantityInStock;
            _lastStockUpdate = DateTime.Now;
        }

        public Inventory(int inventoryID, Products product, int quantity)
        {
            InventoryID = inventoryID;
            Product = product;
            QuantityInStock = quantity;
            LastStockUpdate = DateTime.Now;
        }

        public Inventory(Products product, int quantity, DateTime LastStockUpdate)
        {
            Product = product;
            QuantityInStock = quantity;
            this.LastStockUpdate = LastStockUpdate;
        }

        // Methods

        public Products GetProduct()
        {
            return Product;
        }

        public int GetQuantityInStock()
        {
            return QuantityInStock;
        }

        public void AddToInventory(int quantity)
        {
            QuantityInStock += quantity;
        }

        public void RemoveFromInventory(int quantity)
        {
            if (quantity > QuantityInStock)
            {
                throw new InsufficientStockException(
                    $"Requested quantity ({quantity}) exceeds available stock ({QuantityInStock}).");
            }

            QuantityInStock -= quantity;
        }

        public void UpdateStockQuantity(int newQuantity)
        {
            QuantityInStock = newQuantity;
        }

        public bool IsProductAvailable(int quantityToCheck)
        {
            return QuantityInStock >= quantityToCheck;
        }

        public double GetInventoryValue()
        {
            return Product.Price * QuantityInStock;
        }
        public static void ListLowStockProducts(List<Inventory> inventoryList, int threshold)
        {
            Console.WriteLine($"\nProducts with stock less than {threshold}:\n");

            bool found = false;

            foreach (var item in inventoryList)
            {
                if (item.QuantityInStock < threshold)
                {
                    Console.WriteLine($"Product ID   : {item.Product.ProductID}");
                    Console.WriteLine($"Product Name : {item.Product.ProductName}");
                    Console.WriteLine($"Stock        : {item.QuantityInStock}");
                    Console.WriteLine($"Last Updated : {item.LastStockUpdate}");
                    Console.WriteLine("------------------------");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("All products meet the minimum stock level.");
            }
        }
        public static void ListOutOfStockProducts(List<Inventory> inventoryList)
        {
            Console.WriteLine("\nOut-of-Stock Products:\n");

            bool found = false;

            foreach (var item in inventoryList)
            {
                if (item.QuantityInStock == 0)
                {
                    Console.WriteLine($"Product ID   : {item.Product.ProductID}");
                    Console.WriteLine($"Product Name : {item.Product.ProductName}");
                    Console.WriteLine($"Price        : {item.Product.Price:C}");
                    Console.WriteLine($"Last Updated : {item.LastStockUpdate}");
                    Console.WriteLine("------------------------");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No products are out of stock.");
            }
        }

        public static void ListAllProducts(List<Inventory> inventoryList)
        {
            Console.WriteLine("All Products in Inventory:\n");
            foreach (var item in inventoryList)
            {
                Console.WriteLine($"Product ID   : {item.Product.ProductID}");
                Console.WriteLine($"Product Name : {item.Product.ProductName}");
                Console.WriteLine($"Price        : {item.Product.Price:C}");
                Console.WriteLine($"Quantity     : {item.QuantityInStock}");
                Console.WriteLine($"Last Updated : {item.LastStockUpdate}");
                Console.WriteLine("------------------------");
            }
        }

         static void Main(string[] args)
         {
             List<Inventory> inventoryList = new List<Inventory>();

             Products p1 = new Products(1, "Laptop", "Gaming Laptop", 75000, 10);
             Products p2 = new Products(2, "Mouse", "Wireless Mouse", 1500, 5);
             Products p3 = new Products(3, "Keyboard", "Mechanical Keyboard", 3500, 20);

             Inventory i1 = new Inventory(1, p1, 30);
             Inventory i2 = new Inventory(2, p2, 15);
             Inventory i3 = new Inventory(3, p3, 20);

             inventoryList.Add(i1);
             inventoryList.Add(i2);
             inventoryList.Add(i3);

             bool exit = false;

             while (!exit)
             {
                 Console.WriteLine("\nInventory Management Menu:");
                 Console.WriteLine("1 Add to Inventory");
                 Console.WriteLine("2 Remove from Inventory");
                 Console.WriteLine("3 Update Stock Quantity");
                 Console.WriteLine("4 Check Product Availability");
                 Console.WriteLine("5 Get Inventory Value");
                 Console.WriteLine("6 List Low Stock Products");
                 Console.WriteLine("7 List Out of Stock Products");
                 Console.WriteLine("8 List All Products");
                 Console.WriteLine("9 Exit");
                 Console.Write("Enter your choice: ");
                 string input = Console.ReadLine();

                 switch (input)
                 {
                     case "1":
                         Console.Write("Enter Product ID to add stock: ");
                         int pidToAdd = int.Parse(Console.ReadLine());
                         Console.Write("Enter quantity to add: ");
                         int addQty = int.Parse(Console.ReadLine());

                         foreach (var item in inventoryList)
                         {
                             if (item.Product.ProductID == pidToAdd)
                             {
                                 item.AddToInventory(addQty);
                                 Console.WriteLine("Stock added successfully.");
                             }
                         }
                         break;

                     case "2":
                         Console.Write("Enter Product ID to remove stock: ");
                         int pidToRemove = int.Parse(Console.ReadLine());
                         Console.Write("Enter quantity to remove: ");
                         int removeQty = int.Parse(Console.ReadLine());

                         foreach (var item in inventoryList)
                         {
                             if (item.Product.ProductID == pidToRemove)
                             {
                                 try
                                 {
                                     item.RemoveFromInventory(removeQty);
                                     Console.WriteLine("Stock removed successfully.");
                                 }
                                 catch (InsufficientStockException ex)
                                 {
                                     Console.WriteLine("Error: " + ex.Message);
                                 }
                             }
                         }
                         break;

                     case "3":
                         Console.Write("Enter Product ID to update stock: ");
                         int pidToUpdate = int.Parse(Console.ReadLine());
                         Console.Write("Enter new stock quantity: ");
                         int newQty = int.Parse(Console.ReadLine());

                         foreach (var item in inventoryList)
                         {
                             if (item.Product.ProductID == pidToUpdate)
                             {
                                 try
                                 {
                                     item.UpdateStockQuantity(newQty);
                                     Console.WriteLine("Stock updated successfully.");
                                 }
                                 catch (InsufficientStockException ex)
                                 {
                                     Console.WriteLine("Error: " + ex.Message);
                                 }
                             }
                         }
                         break;

                     case "4":
                         Console.Write("Enter Product ID to check availability: ");
                         int pidCheck = int.Parse(Console.ReadLine());
                         Console.Write("Enter quantity to check: ");
                         int checkQty = int.Parse(Console.ReadLine());

                         foreach (var item in inventoryList)
                         {
                             if (item.Product.ProductID == pidCheck)
                             {
                                 if (item.IsProductAvailable(checkQty))
                                 {
                                     Console.WriteLine("Product is available in sufficient quantity.");
                                 }
                                 else
                                 {
                                     Console.WriteLine("Product is not available in sufficient quantity.");
                                 }
                             }
                         }
                         break;

                     case "5":
                         foreach (var item in inventoryList)
                         {
                             Console.WriteLine($"Product {item.Product.ProductName} Value: {item.GetInventoryValue()}");
                         }
                         break;

                     case "6":
                         Console.Write("Enter stock threshold: ");
                         int threshold = int.Parse(Console.ReadLine());
                         Inventory.ListLowStockProducts(inventoryList, threshold);
                         break;

                     case "7":
                         Inventory.ListOutOfStockProducts(inventoryList);
                         break;

                     case "8":
                         Inventory.ListAllProducts(inventoryList);
                         break;

                     case "9":
                         exit = true;
                         break;

                     default:
                         Console.WriteLine("Invalid choice. Try again.");
                         break;
                 }
               }
            
            }
    }
}