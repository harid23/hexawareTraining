using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Entity
{
    internal class PetShelter
    {
        private List<Pet> availablePets= new List<Pet>();
        public void AddPet(Pet pet)
        {
            availablePets.Add(pet);
            Console.WriteLine($"{pet.Name}, added to shetler successfully");
        }
        public void RemovePet(Pet pet)
        {
            if (availablePets.Contains(pet))
            {
                availablePets.Remove(pet);
                Console.WriteLine($"{pet.Name} is removed.");
            }
            else
            {
                Console.WriteLine($"{pet.Name} not found");
            }
        }
        public void ListAvailablePets()
        {
            if (availablePets.Count > 0)
            {
                throw new NullReferenceException("No available pets in the shelter");
            }
            foreach (var pet in availablePets)
            {
                Console.WriteLine(pet.ToString());
            }
        }
    }
}
