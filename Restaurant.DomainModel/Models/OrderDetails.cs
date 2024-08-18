using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models
{
    public class OrderDetails
    {
        public int OrderDetailsID { get; set; }

        public int OrderID { get; set; }

        public int FoodID { get; set; }

        public int BeveragesID { get; set; }

        public int AppetizerID { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public int TotalPrice { get; set; }

        public Order order { get; set; }

        public Food food { get; set; }

        public Beverages beverage { get; set; }

        public Appetizer appetizer { get; set; }

        public OrderDetails()
        {

        }
    }
}
