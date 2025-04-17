using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Entity
{
    public class Dog : Pet
    {
        public string DogBreed {  get; set; }
        // public Dog() { }
        public Dog(int Id, string name, int age, string breed, string type, string dogBreed) : base(Id, name, age, breed, type)
        {
            DogBreed=dogBreed;
        }
        public override string ToString()
        {
            return base.ToString()+ ",Dog Breed :"+DogBreed;
        }
    }
}
