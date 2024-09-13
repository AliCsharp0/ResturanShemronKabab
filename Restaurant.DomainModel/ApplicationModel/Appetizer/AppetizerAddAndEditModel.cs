﻿using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Appetizer
{
    public class AppetizerAddAndEditModel
    {

        public int AppetizerID { get; set; }

        public int CategoryID { get; set; }

        public string AppetizerName { get; set; }

        public int UnitPrice { get; set; }

        public string SmallDescription { get; set; }

        public string ImageURL { get; set; }
    }
}
