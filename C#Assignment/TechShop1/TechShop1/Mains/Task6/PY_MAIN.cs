using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;

namespace TechShop1.Mains.Task6
{
    class PY_Main
    {
        static void Main(string[] args)
        {
            PaymentManager paymentManager = new PaymentManager();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- TechShop Payment Manager ---");
                Console.WriteLine("1. Add Payment");
                Console.WriteLine("2. Update Payment Status");
                Console.WriteLine("3. List All Payments");
                Console.WriteLine("4. Get Payments by Order ID");
                Console.WriteLine("5. Process Payment for Order");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.Write("Enter Payment ID: ");
                            int paymentId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Order ID: ");
                            int orderId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Amount: ");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter Status: ");
                            string status = Console.ReadLine();

                            Orders order = new Orders { OrderId = orderId, TotalAmount = (decimal)amount };
                            Payment payment = new Payment(paymentId, order, amount, status, DateTime.Now);
                            paymentManager.AddPayment(payment);
                        }
                        catch (DuplicatePaymentException ex)
                        {
                            Console.WriteLine("Duplicate Error: " + ex.Message);
                        }
                        catch (InvalidPaymentException ex)
                        {
                            Console.WriteLine("Invalid Amount Error: " + ex.Message);
                        }
                        break;

                    case 2:
                        try
                        {
                            Console.Write("Enter Payment ID: ");
                            int pid = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter new status: ");
                            string status = Console.ReadLine();

                            paymentManager.UpdatePaymentStatus(pid, status);
                        }
                        catch (PaymentNotFoundException ex)
                        {
                            Console.WriteLine("Update Error: " + ex.Message);
                        }
                        break;

                    case 3:
                        paymentManager.ListAllPayments();
                        break;

                    case 4:
                        Console.Write("Enter Order ID: ");
                        int oid = Convert.ToInt32(Console.ReadLine());
                        var payments = paymentManager.GetPaymentsByOrder(oid);
                        if (payments.Count == 0)
                        {
                            Console.WriteLine("No payments found for this order.");
                        }
                        else
                        {
                            foreach (var p in payments)
                            {
                                Console.WriteLine(p.GetPaymentDetails());
                                Console.WriteLine("-----------------------");
                            }
                        }
                        break;

                    case 5:
                        try
                        {
                            Console.Write("Enter Order ID: ");
                            int newOrderId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Total Amount: ");
                            decimal total = Convert.ToDecimal(Console.ReadLine());

                            Orders newOrder = new Orders { OrderId = newOrderId, TotalAmount = total };
                            paymentManager.ProcessPayment(newOrder);
                        }
                        catch (PaymentFailedException ex)
                        {
                            Console.WriteLine("Payment Failed: " + ex.Message);
                        }
                        break;

                    case 6:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

            Console.WriteLine("Exiting Payment Manager.");
        }
    }
}