using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Service
{
    internal interface ICarLeaseService
    {
        void AddVehicle();
        void RemoveVehicle();
        void GetAllVehicles();
        void GetRentedVehicles();
        void GetVehicleById();
        void ReturnCar();
        void AddCustomer();
        void RemoveCustomer();
        void GetAllCustomers();
        void GetCustomerById();
        void CreateLease();

        //void GetActiveLeases();
        void GetLeaseHistory();
        void ViewLeaseById();
        void RecordPayment();
        void CalculateLeaseAmount();

    }
}
