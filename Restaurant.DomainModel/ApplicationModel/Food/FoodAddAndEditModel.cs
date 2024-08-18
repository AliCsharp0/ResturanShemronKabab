using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Food
{
    public class FoodAddAndEditModel
    {
        public int FoodID { get; set; }

        public int CategoryID { get; set; }

        public string FoodName { get; set; }

        public int UnitPrice { get; set; }

        public string Materials { get; set; }

        public string Image { get; set; }
    }
}
