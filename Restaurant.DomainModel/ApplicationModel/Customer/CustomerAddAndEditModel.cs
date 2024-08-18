﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Customer
{
    public class CustomerAddAndEditModel
    {
        public int CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string Mobile { get; set; }

        public string TelHome { get; set; }
    }
}
