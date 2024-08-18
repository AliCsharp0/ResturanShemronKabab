using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Food
{
    public class FoodListItem
    {
        public int FoodID { get; set; }

        public string CategoryName { get; set; }

        public string FoodName { get; set; }

        public int UnitPrice { get; set; }

        public string Materials { get; set; }

        public string Image { get; set; }

        public bool HasRelatedOrder { get; set; }
    }
}
