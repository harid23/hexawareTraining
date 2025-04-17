using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;
using TechShop1.Task1;
using TechShop1;

namespace TechShop1
{
    public class OrderDetails
    {
        private int _orderDetailID;
        private Orders _order;
        private Products _product;
        private int _quantity;
        private double _discountPercentage;
        public int OrderDetailID
        {
            get { return _orderDetailID; }
            set
            {
                if (value <= 0)
                    throw new InvalidDataException("OrderDetailID must be greater than zero.");
                _orderDetailID = value;
            }
        }

        public Orders Order
        {
            get { return _order; }
            set { _order = value; }
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

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value <= 0)
                    throw new InvalidDataException("Quantity must be greater than zero.");
                _quantity = value;
            }
        }
        public double DiscountPercentage
        {
            get { return _discountPercentage; }
            set
            {
                if (value < 0 || value > 100)
                    throw new InvalidDataException("Discount must be between 0 and 100.");
                _discountPercentage = value;
            }
        }
        //COnstructor
        public OrderDetails(int orderDetailId, Orders order, Products product, int quantity)
        {
            if (product == null)
            {
                throw new IncompleteOrderException("Error--Order detail must have a valid product reference.");
            }

            OrderDetailID = orderDetailId;
            Order = order;
            Product = product;
            Quantity = quantity;
            DiscountPercentage = 0; 
        }
        public OrderDetails() { }

        public OrderDetails(Products product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        //Methods
        public double CalculateSubtotal()
        {
            double subtotal = Product.Price * Quantity;
            if (DiscountPercentage > 0)
            {
                subtotal -= subtotal * (DiscountPercentage / 100);
            }
            return subtotal;
        }

        public string GetOrderDetailInfo()
        {
            return $"OrderDetail ID: {OrderDetailID}\n" +
                   $"Product: {Product.ProductName}\n" +
                   $"Quantity: {Quantity}\n" +
                   $"Price per unit: ${Product.Price}\n" +
                   $"Subtotal: ${CalculateSubtotal()}\n" +
                   "---------------------";
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
            {
                throw new InvalidDataException("Quantity must be greater than zero.");
            }
            Quantity = newQuantity;
        }

        public void AddDiscount(double discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new InvalidDataException("Discount must be between 0 and 100.");
            }
            DiscountPercentage = discountPercentage;
        }




        static void Main(string[] args)
        {
           
            Customers customer = new Customers(1, "Hari", "D", "hari@gmail.com", "1234567890", "Kolathur");
            Products laptop = new Products(1, "Laptop", "High-performance laptop", 1000.0, 20);
            Orders order = new Orders(1, customer, DateTime.Now);
            OrderDetails detail = new OrderDetails(1, order, laptop, 2);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("----- Order Detail Menu -----");
                Console.WriteLine("1. View Order Detail Info");
                Console.WriteLine("2. Calculate Subtotal");
                Console.WriteLine("3. Update Quantity");
                Console.WriteLine("4. Add Discount");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Order Detail Info:");
                        Console.WriteLine(detail.GetOrderDetailInfo());
                        break;

                    case "2":
                        double subtotal = detail.CalculateSubtotal();
                        Console.WriteLine("Subtotal is: " + subtotal);
                        break;

                    case "3":
                        Console.Write("Enter new quantity: ");
                        string qtyInput = Console.ReadLine();
                        int newQty;
                        if (int.TryParse(qtyInput, out newQty))
                        {
                            try
                            {
                                detail.UpdateQuantity(newQty);
                                Console.WriteLine("Quantity updated successfully.");
                            }
                            catch (InvalidDataException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity input.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter discount percentage: ");
                        string discountInput = Console.ReadLine();
                        double discount;
                        if (double.TryParse(discountInput, out discount))
                        {
                            try
                            {
                                detail.AddDiscount(discount);
                                Console.WriteLine("Discount applied successfully.");
                            }
                            catch (InvalidDataException ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid discount input.");
                        }
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting Order Detail menu.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        
       /* static void Main(string[] args)
        {
            try
            {
                Customers customer = new Customers(1, "HariHaran", "D", "harish@gmail.com", "1234567890", "koymbedu");
                Orders order = new Orders(1, customer, DateTime.Now);

                // passing null to trigger the IncompleteOrderException
                OrderDetails detail = new OrderDetails(1, order, null, 2);

                Console.WriteLine("This line will not be reached if exception is thrown.");
            }
            catch (IncompleteOrderException ex)
            {
                Console.WriteLine("Caught custom exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }
        }*/

    }
}