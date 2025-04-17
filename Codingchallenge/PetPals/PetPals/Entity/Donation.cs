using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace PetPals.Entity
{
    public abstract class Donation
    {
        public string DonorName { get; set; }
        public decimal Amount { get; set; }

        public Donation(string donorName, decimal amount)
        {
            if (amount < 10)
            {
                throw new InsufficientFundsException("Donation must be at least $10.");
            }

            DonorName = donorName;
            Amount = amount;
        }

        public abstract void RecordDonation();
    }

    public class CashDonation : Donation
    {
        public DateTime DonationDate { get; set; }

        public CashDonation(string donorName, decimal amount, DateTime donationDate)
            : base(donorName, amount)
        {
            DonationDate = donationDate;
        }

        public override void RecordDonation()
        {
            Console.WriteLine($"Cash donation of {Amount} from {DonorName} on {DonationDate} recorded.");
        }
    }

    public class ItemDonation : Donation
    {
        public DateTime DonationDate { get; set; }

        public ItemDonation(string donorName, decimal amount, DateTime donationDate)
            : base(donorName, amount)
        {
            DonationDate = donationDate;
        }

        public override void RecordDonation()
        {
            Console.WriteLine($"Item donation on {DonationDate} from {DonorName} recorded.");
        }
    }

}