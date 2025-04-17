using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;
using TechShop1;

namespace TechShop1
{
    public class ProductManager
    {
        private List<Products> _products = new List<Products>();

        public List<Products> GetAllProducts()
        {
            return _products;
        }

        public void AddProduct(Products product)
        {
            foreach (Products p in _products)
            {
                if (p.ProductID == product.ProductID)
                {
                    throw new DuplicateProductException("ERROR--Product with the same ID already exists.");
                }
            }

            _products.Add(product);
            Console.WriteLine("Product added successfully.");
        }

        public void UpdateProduct(int productId, string newName, string newDesc, double newPrice, int newStock)
        {
            Products product = null;
            foreach (Products p in _products)
            {
                if (p.ProductID == productId)
                {
                    product = p;
                    break;
                }
            }

            if (product == null)
            {
                throw new ProductNotFoundException("Product not found.");
            }

            if (newPrice < 0 || newStock < 0)
            {
                throw new InvalidProductUpdateException("ERROR---Invalid price or stock quantity.");
            }

            product.ProductName = newName;
            product.Description = newDesc;
            product.Price = newPrice;
            product.StockInQuantity = newStock;
            Console.WriteLine("Product updated successfully.");
        }
        public void RemoveProduct(int productId, List<Orders> existingOrders)
        {
            Products productToRemove = null;
            foreach (Products p in _products)
            {
                if (p.ProductID == productId)
                {
                    productToRemove = p;
                    break;
                }
            }

            if (productToRemove == null)
            {
                throw new ProductNotFoundException("Product not found.");
            }
            bool isUsedInOrder = false;
            foreach (Orders order in existingOrders)
            {
                foreach (OrderDetails detail in order.OrderDetails)
                {
                    if (detail.Product.ProductID == productId)
                    {
                        isUsedInOrder = true;
                        break;
                    }
                }

                if (isUsedInOrder)
                {
                    break;
                }
            }

            if (isUsedInOrder)
            {
                throw new ProductInUseException("Error---Cannot remove product.");
            }
            _products.Remove(productToRemove);
            Console.WriteLine("Product removed successfully.");
        }

        public void ListAllProducts()
        {
            if (_products.Count == 0)
            {
                Console.WriteLine("No products available.");
                return;
            }

            foreach (var p in _products)
            {
                Console.WriteLine(p.GetProductDetails());
            }
        }

        public List<Products> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Search keyword cannot be empty.");

            List<Products> results = new List<Products>();

            foreach (Products product in _products)
            {
                if (product.ProductName.ToLower().Contains(keyword.ToLower()))
                {
                    results.Add(product);
                }
            }

            if (results.Count == 0)
            {
                Console.WriteLine("No products found matching the name.");
            }

            return results;
        }


    }
}