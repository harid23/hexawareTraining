using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace PetPals.Entity
{
    public class Pet
    {
       /* public int PetID;
        public string Name;
        public int Age;
        public string Breed;
        public string Type;*/

        public int PetID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }

        public string Type { get; set; }

        public Pet( int Id, string name, int age, string breed, string type)
        {
            if(age <=0)
            {
                throw new InvalidPetAgeException("Invalid Pet age");
            }
            PetID = Id;
            Name = name;
            Age = age;
            Breed = breed;
            Type = type;
        }
        public override string ToString()
        {
            return $"Pet ID : {PetID}, Pet Name :{Name},Age : {Age},Breed :{Breed}";
        }
    }
}
