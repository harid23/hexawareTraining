using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using PetPals.Entity;
using Exceptions;
using PetPals.DBUtility;

namespace PetPals
{
    internal class Mains
    {
        private static void Main(string[] args)
        {
            string connectionString = DBProperty.GetConnectionString();
            PetDAO petDAO = new PetDAO(connectionString);
            DonationDAO donationDAO = new DonationDAO(connectionString);
            AdoptionEventDAO eventDAO = new AdoptionEventDAO(connectionString);

            PetShelter petShelter = new PetShelter();

            bool continueApp = true;

            while (continueApp)
            {
                Console.WriteLine("\n===== PetPals Main Menu =====");
                Console.WriteLine("1. Add a Pet");
                Console.WriteLine("2. Remove a Pet");
                Console.WriteLine("3. List Available Pets");
               Console.WriteLine("4. Record a Cash Donation");
               Console.WriteLine("5. Record an Item Donation");
                Console.WriteLine("6. Create an Adoption Event");
                Console.WriteLine("7. List Upcoming Adoption Events");
                Console.WriteLine("8. Register for an Adoption Event");
                
                 Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddPet(petDAO);
                        break;
                    case "2":
                        RemovePet(petDAO);
                        break;
                    case "3":
                        ListAvailablePets(petDAO);
                        break;
                    case "4":
                        RecordCashDonation(donationDAO);
                        break;
                    case "5":
                        RecordItemDonation(donationDAO);
                        break;
                    case "6":
                        ListUpcomingEvents(eventDAO);
                        break;
                    case "7":
                        RegisterForEvent(eventDAO);
                        break;
                    case "8":
                        CreateAdoptionEvent(eventDAO);
                        break;
                    case "9":
                        continueApp = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddPet(PetDAO petDAO)
        {
            try
            {
                Console.WriteLine("Enter Pet ID :");
                int Id=Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Pet Name:");
                string name = Console.ReadLine();

                    Console.WriteLine("Enter Pet Age:");
                    if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
                    throw new InvalidPetAgeException("Pet age must be a positive integer.");

                    Console.WriteLine("Enter Pet Breed:");
                     string breed = Console.ReadLine();

                Console.WriteLine("Enter the TYPE: DOG/CAT");
                string type = Console.ReadLine();

                Pet pet = new Pet(Id,name, age, breed, type);
                petDAO.AddPet(pet);

                Console.WriteLine("Pet added successfully.");
            }
            catch (InvalidPetAgeException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void RemovePet(PetDAO petDAO)
        {
            try
            {
                Console.WriteLine("Enter the ID of the pet to remove:");
                if (!int.TryParse(Console.ReadLine(), out int petId))
                    throw new Exception("Invalid pet ID.");

                    petDAO.RemovePet(petId);
                Console.WriteLine("Pet removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void ListAvailablePets(PetDAO petDAO)
        {
            try
            {
                List<Pet> pets = petDAO.GetAllPets();
                if (pets.Count == 0)
                {
                    Console.WriteLine("No pets available for adoption.");
                }
                else
                {
                    Console.WriteLine("\n===== Available Pets =====");
                    foreach (var pet in pets)
                    {
                        Console.WriteLine(pet.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void RecordCashDonation(DonationDAO donationDAO)
        {
            try
            {
                Console.WriteLine("Enter Donor Name:");
                    string donorName = Console.ReadLine();

                    Console.WriteLine("Enter Donation Amount:");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount < 10)
                    throw new Exception("Donation amount must be at least $10.");

                Console.WriteLine("Enter Donation Date (YYYY-MM-DD):");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime donationDate))
                    throw new Exception("Invalid date format.");

                    CashDonation donation = new CashDonation(donorName, amount, donationDate);
                donationDAO.RecordCashDonation(donation);

                Console.WriteLine("Cash donation recorded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void RecordItemDonation(DonationDAO donationDAO)
        {
            try
            {
                Console.WriteLine("Enter Donor Name:");
                string donorName = Console.ReadLine();

                Console.WriteLine("Enter Donation Amount:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                    throw new Exception("Invalid donation amount.");

                Console.WriteLine("Enter Donation Date (YYYY-MM-DD):");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime donationDate))
                    throw new Exception("Invalid date format.");

                ItemDonation donation = new ItemDonation(donorName, amount, donationDate);
                donationDAO.RecordItemDonation(donation);

                Console.WriteLine("Item donation recorded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void ListUpcomingEvents(AdoptionEventDAO eventDAO)
        {
            try
            {
                List<string> events = eventDAO.GetUpcomingEvents();
                if (events.Count == 0)
                {
                    Console.WriteLine("No upcoming events.");
                }
                else
                {
                    Console.WriteLine("\n===== Upcoming Events =====");
                    foreach (var eventDetail in events)
                    {
                        Console.WriteLine(eventDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void RegisterForEvent(AdoptionEventDAO eventDAO)
        {
            try
            {
                Console.WriteLine("Enter Event ID to register:");
                if (!int.TryParse(Console.ReadLine(), out int eventId))
                    throw new Exception("Invalid event ID.");

                Console.WriteLine("Enter Participant Name:");
                string participantName = Console.ReadLine();

                eventDAO.RegisterParticipant(participantName, eventId);
                Console.WriteLine("Participant registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void CreateAdoptionEvent(AdoptionEventDAO eventDAO)
        {
            try
            {
                Console.WriteLine("Enter Event Name:");
                string eventName = Console.ReadLine();

                Console.WriteLine("Enter Event Date (YYYY-MM-DD):");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime eventDate))
                    throw new Exception("Invalid date format.");

                eventDAO.AddAdoptionEvent(eventName, eventDate);
                Console.WriteLine("Adoption event created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        //PetShelter shelter = new PetShelter();

        //// Adding a dog
        //try
        //{
        //    Dog dog = new Dog("Buddy", 3, "Labrador", "Labrador Retriever");
        //    shelter.AddPet(dog);
        //}
        //catch (InvalidPetAgeException ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}

        //// Adding a cat
        //try
        //{
        //    Cat cat = new Cat("Whiskers", 2, "Persian", "White");
        //    shelter.AddPet(cat);
        //}
        //catch (InvalidPetAgeException ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}

        //// List all available pets
        //try
        //{
        //    shelter.ListAvailablePets();
        //}
        //catch (NullReferenceException ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}

        //// Adding a donation
        //try
        //{
        //    CashDonation donation = new CashDonation("John Doe", 15.00m, DateTime.Now);
        //    donation.RecordDonation();
        //}
        //catch (InsufficientFundsException ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}

        //Console.WriteLine("Operations completed successfully.");

    }
}

