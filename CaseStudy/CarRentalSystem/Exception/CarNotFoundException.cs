﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Exception
{
    public class CarNotFoundException : ApplicationException
    {
        public CarNotFoundException(string message) : base(message) { }

    }
}
