using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Entity
{
    public class Lease
    {
        //public int LeaseID { get; set; }
        //public string VehicleID { get; set; }
        //public int CustomerID { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public string LeaseType { get; set; }

        private int _leaseID;
        private string _vehicleID;
        private int _customerID;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _leaseType;

        // Properties with getters and setters
        public int LeaseID
        {
            get; set;
        }

        public string VehicleID
        {
            get; set;
        }

        public int CustomerID
        {
            get; set;
        }

        public DateTime StartDate
        {
            get; set;
        }

        public DateTime EndDate
        {
            get; 
            set;
        }

        public string LeaseType
        {
            get; set;
        }
        // Default Constructor
        public Lease() { }


        // Parameterized Constructor
        public Lease(int leaseID, string vehicleID, int customerID, DateTime startDate, DateTime endDate, string leaseType)
        {
            LeaseID = leaseID;
            VehicleID = vehicleID;
            CustomerID = customerID;
            StartDate = startDate;
            EndDate = endDate;
            LeaseType = leaseType;
        }

       
    }
}
