using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Entity
{
    public class Vehicle
    {
        private string _vehicleID;
        private string _make;
        private string _model;
        private int _year;
        private decimal _dailyRate;
        private string _status;
        private int _passengerCapacity;
        private int _engineCapacity;
        // Properties with getters and setters
        public string VehicleID
        {
            get; set;
        }

        public string Make
        {
            get; set;
        }

        public string Model
        {
            get; set;
        }

        public int Year
        {
            get; set;
        }

        public decimal DailyRate
        {
            get; set;
        }

        public string Status
        {
            get; set;
        }

        public int PassengerCapacity
        {
            get; set;
        }

        public int EngineCapacity
        {
            get; set;
        }

        // Default Constructor
        public Vehicle() { }


        // Parameterized Constructor
        public Vehicle(string vehicleID, string make, string model, int year, decimal dailyRate, string status, int passengerCapacity, int engineCapacity)
        {
            VehicleID = vehicleID;
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }
    }
}
