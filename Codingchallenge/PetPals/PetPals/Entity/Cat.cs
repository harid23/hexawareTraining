using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Entity
{
     public class Cat : Pet
    {
        public string CatColor { get; set; }
        //public Cat() { }
        public Cat(int Id, string name, int age, string breed, string type, string catColor) : base(Id, name, age, breed, type)
        {
            CatColor = catColor;
        }
        public override string ToString()
        {
            return base.ToString() + ",Cat color :{CatColor}";
        }
    }
}
