using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1.Exceptions;
using TechShop1;
namespace TechShop1
{
    public class PaymentManager
    {
        private List<Payment> _payments = new List<Payment>();

   
        public void AddPayment(Payment payment)
        {
            foreach (var p in _payments)
            {
                if (p.PaymentID == payment.PaymentID)
                {
                    throw new DuplicatePaymentException("Error---Payment with the same ID already exists.");
                }
            }

            if (payment.Amount <= 0)
            {
                throw new InvalidPaymentException("Payment amount must be greater than zero.");
            }

            _payments.Add(payment);
            Console.WriteLine("Payment recorded successfully.");
        }

     
        public void UpdatePaymentStatus(int paymentId, string newStatus)
        {
            Payment payment = null;
            foreach (var p in _payments)
            {
                if (p.PaymentID == paymentId)
                {
                    payment = p;
                    break;
                }
            }

            if (payment == null)
            {
                throw new PaymentNotFoundException("Payment not found.");
            }

            payment.PaymentStatus = newStatus;
            Console.WriteLine("Payment status updated to: " + newStatus);
        }
        public void ListAllPayments()
        {
            if (_payments.Count == 0)
            {
                Console.WriteLine("No payment records found.");
                return;
            }

            foreach (var p in _payments)
            {
                Console.WriteLine(p.GetPaymentDetails());
                Console.WriteLine("--------------------------");
            }
        }
        public List<Payment> GetPaymentsByOrder(int orderId)
        {
            List<Payment> result = new List<Payment>();
            foreach (var payment in _payments)
            {
                if (payment.Order.OrderId == orderId)
                {
                    result.Add(payment);
                }
            }
            return result;
        }

        public bool ProcessPayment(Orders order)
        {
            if (order.TotalAmount <= 0)
            {
                throw new PaymentFailedException("Cannot process payment for zero or negative amount.");
            }

            int paymentId = _payments.Count + 1;
            Payment payment = new Payment(paymentId, order, (double)order.TotalAmount, "Completed", DateTime.Now);
            _payments.Add(payment);

            Console.WriteLine($"Payment of ₹{order.TotalAmount} processed successfully for Order #{order.OrderId}.");
            return true;
        }



    }
}