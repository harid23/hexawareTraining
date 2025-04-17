using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;
using TechShop1;

namespace TechShop1.Collections
{
   
    public class OrderManager
    {
        private List<Orders> _orders = new List<Orders>();

        private SortedList<int, Inventory> _inventory = new SortedList<int, Inventory>();
        private PaymentManager _paymentManager;



        public void AddOrder(Orders newOrder)
        {
         
            foreach (OrderDetails detail in newOrder.OrderDetails)
            {
                Inventory item = FindInventoryItem(detail.Product.ProductID);
                if (item == null || item.QuantityInStock < detail.Quantity)
                {
                    throw new InsufficientStockException("Not enough stock for product: " + detail.Product.ProductName);
                }
                item.QuantityInStock -= detail.Quantity;
            }

            _orders.Add(newOrder);
            Console.WriteLine("Order added and inventory/payment updated.");
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            Orders foundOrder = null;

            foreach (Orders order in _orders)
            {
                if (order.OrderId == orderId)
                {
                    foundOrder = order;
                    break;
                }
            }

            if (foundOrder == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            foundOrder.UpdateOrderStatus(newStatus);
            Console.WriteLine("Order status updated to: " + newStatus);
        }


        public void CancelOrder(int orderId)
        {
            Orders foundOrder = null;

            foreach (Orders order in _orders)
            {
                if (order.OrderId == orderId)
                {
                    foundOrder = order;
                    break;
                }
            }

            if (foundOrder == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            foreach (OrderDetails detail in foundOrder.OrderDetails)
            {
                int productId = detail.Product.ProductID;

                if (_inventory.ContainsKey(productId))
                {
                    _inventory[productId].QuantityInStock += detail.Quantity;
                }
            }

            _orders.Remove(foundOrder);
            Console.WriteLine("Order canceled and stock restored.");
        }


        public Inventory FindInventoryItem(int productId)
        {
            if (_inventory.ContainsKey(productId))
            {
                return _inventory[productId];
            }
            return null;
        }

        public void SortOrdersByDateAscending()
        {
            for (int i = 0; i < _orders.Count - 1; i++)
            {
                for (int j = i + 1; j < _orders.Count; j++)
                {
                    if (_orders[i].OrderDate > _orders[j].OrderDate)
                    {
                        // Swap
                        Orders temp = _orders[i];
                        _orders[i] = _orders[j];
                        _orders[j] = temp;
                    }
                }
            }

            Console.WriteLine("Orders sorted by date (ascending):");
            foreach (Orders order in _orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}");
            }
        }
        public void SortOrdersByDateDescending()
        {
            for (int i = 0; i < _orders.Count - 1; i++)
            {
                for (int j = i + 1; j < _orders.Count; j++)
                {
                    if (_orders[i].OrderDate < _orders[j].OrderDate)
                    {
                        // Swap
                        Orders temp = _orders[i];
                        _orders[i] = _orders[j];
                        _orders[j] = temp;
                    }
                }
            }

            Console.WriteLine("Orders sorted by date (descending):");
            foreach (Orders order in _orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}");
            }
        }

        public void DisplayOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            bool found = false;

            foreach (Orders order in _orders)
            {
                if (order.OrderDate >= startDate && order.OrderDate <= endDate)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No orders found in the given date range.");
            }
        }

        public void DisplayAllOrders()
        {
            foreach (Orders order in _orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Status: {order.OrderStatus}");
                foreach (OrderDetails detail in order.OrderDetails)
                {
                    Console.WriteLine($"  - Product: {detail.Product.ProductName}, Qty: {detail.Quantity}, Price: ${detail.Product.Price}");
                }
            }
        }

        public void AddInventory(Inventory inventory)
        {
            _inventory[inventory.Product.ProductID] = inventory;
        }


    }
}