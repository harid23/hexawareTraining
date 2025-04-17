using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace Exceptions
    {
        public class DuplicateIdException : Exception
        {
           public DuplicateIdException(string message) : base(message) { }
        }
        public class InvalidPetAgeException : Exception
        {
            public InvalidPetAgeException(string message) : base(message) { }
        }
        public class InsufficientFundsException : Exception
        {
            public InsufficientFundsException(string message) : base(message) { }
        }

    }

