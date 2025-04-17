
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop1.Exceptions
{
    class ProductInUseException : Exception
    {
        public ProductInUseException(string message) : base(message) { }
    }
}
