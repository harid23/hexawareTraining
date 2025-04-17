using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;

namespace TechShop1
{
    //TASK 1,2,3 Main is below
    public class Products
    {
        private int _ProductID;
        private string _ProductName;
        private string _Description;
        private int _StockInQuantity;
        private double _Price;


        public Products() { }
       
        public int ProductID
        {
            get { return _ProductID; }
            set
            {
                if (value <= 0)
                    throw new InvalidDataException("Error--Product ID must be greater than zero.");
                _ProductID = value;
            }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public double Price
        {
            get { return _Price; }
            set
            {
                if (value < 0)
                    throw new InvalidDataException("Error--Price cannot be negative.");
                _Price = value;
            }
        }

        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Error---Product name cannot be empty.");
                _ProductName = value;

            }
        }

        public int StockInQuantity
        {
            get { return _StockInQuantity; }
            set
            {
                if (value <= 0)
                    throw new InsufficientStockException("Error--Stock quantity cannot be negative.");
                _StockInQuantity = value;
            }
        }

        public Products(int ProductId, string ProductName, String Description, double price, int StockInQuantity)
        {
            this.ProductID = ProductId;
            this.ProductName = ProductName;
            this.Description = Description;
            this.Price = price;
            this.StockInQuantity = StockInQuantity;
        }

        public Products(int ProductId, string productName, int productPrice)
        {
            this.ProductID = ProductId;
            this.ProductName = productName;
            this.Price = productPrice;
        }

        //Methods
        public string GetProductDetails()
        {
            return $"Product ID: {ProductID}\n" +
              $"Name: {ProductName}\n" +
              $"Description: {Description}\n" +
              $"Price: ${Price}";
        }

        public static void UpdateProductInfo(Products product)
        {
            if (product != null)
            {
                Console.Write("Enter new price (leave blank to keep current): ");
                string newPriceInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPriceInput))
                {
                    double newPrice = Convert.ToDouble(newPriceInput);
                    product.Price = newPrice;
                }

                Console.WriteLine("Product info updated.");
            }
            else
            {
                Console.WriteLine("No product available to update.");
            }
        }

        public bool IsProductInStock()
        {
            return this.StockInQuantity > 0;
        }

        static void Main(string[] args)
        {
            Products product = null;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== PRODUCT MENU ===");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View Product Details");
                Console.WriteLine("3. Update Product Info");
                Console.WriteLine("4. Check Product Stock");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter Product ID: ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Product Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Description: ");
                            string desc = Console.ReadLine();

                            Console.Write("Enter Price: ");
                            double price = Convert.ToDouble(Console.ReadLine());

                            Console.Write("Enter Stock Quantity: ");
                            int stock = Convert.ToInt32(Console.ReadLine());

                            product = new Products(id, name, desc, price, stock);
                            Console.WriteLine(" Product added successfully.");
                        }
                        catch (InValidDataException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (InsufficientStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "2":
                        if (product != null)
                        {
                            Console.WriteLine("\n--- Product Details ---");
                            Console.WriteLine(product.GetProductDetails());
                            Console.WriteLine($"Stock Quantity: {product.StockInQuantity}");
                        }
                        else
                        {
                            Console.WriteLine("No product found. Please add a product first.");
                        }
                        break;

                    case "3":
                        UpdateProductInfo(product);
                        break;

                    case "4":
                        if (product != null)
                        {
                            if (product.IsProductInStock())
                            {
                                Console.WriteLine("Product is in stock.");
                            }
                            else
                            {
                                Console.WriteLine("Product is out of stock.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Add a product first to check stock.");
                        }
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}