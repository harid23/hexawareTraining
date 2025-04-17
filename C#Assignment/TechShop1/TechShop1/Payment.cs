using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop1;
using TechShop1.Exceptions;

namespace TechShop1
{
    //Task 1,2,3
    public class Payment
    {
        private int _paymentID;
        private Orders _order;
        private double _amount;
        private string _paymentStatus;
        private DateTime _paymentDate;

        public int PaymentID
        {
            get { return _paymentID; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Payment ID must be greater than 0.");
                _paymentID = value;
            }
        }

        public Orders Order
        {
            get { return _order; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Order), "Order cannot be null.");
                }
                _order = value;
            }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {
                if (value <= 0)
                    throw new InvalidPaymentException("Amount must be a positive value.");
                _amount = value;
            }
        }

        public string PaymentStatus
        {
            get => _paymentStatus;
            set
            {
                string[] array = { "Completed", "Pending", "Failed" };
                if (!array.Contains(value))
                {
                    throw new PaymentFailedException("Invalid payment status. Must be Pending, Completed, or Failed.");
                }
                _paymentStatus = value;
            }
        }

        public DateTime PaymentDate
        {
            get;
            set;
        }

        public Payment(int paymentid, Orders order, double amount, string status, DateTime date)
        {
            PaymentID = paymentid;
            Order = order;
            Amount = amount;
            PaymentStatus = status;
            PaymentDate = date;
        }

        public string GetPaymentDetails()
        {
            return $"Payment ID : {PaymentID}\n" +
                   $"Order ID   : {Order.OrderId}\n" +
                   $"Amount     : ${Amount}\n" +
                   $"Status     : {PaymentStatus}\n" +
                   $"Date       : {PaymentDate:dd-MM-yyyy HH:mm:ss}";
        }
    }
}
