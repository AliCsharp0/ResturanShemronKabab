using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Food
{
    public class FoodListItemUI
    {
        public int FoodID { get; set; }

        public string FoodName { get; set; }

        public int UnitPrice { get; set; }

        public string Image { get; set; }
    }
}
