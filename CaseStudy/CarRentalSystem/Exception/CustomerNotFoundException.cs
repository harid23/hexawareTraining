﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Exception
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
