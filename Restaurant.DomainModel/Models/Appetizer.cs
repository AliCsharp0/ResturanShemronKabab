using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models
{
    public class Appetizer
    {
        public int AppetizerID { get; set; }

        public int CategoryID { get; set; }

        public string AppetizerName { get; set; }

        public int UnitPrice { get; set; }

        public string SmallDescription { get; set; }

        public string Image { get; set; }

        public Category category { get; set; }

        public List<OrderDetails> orderDetails { get; set; }

        public Appetizer()
        {
            this.orderDetails = new List<OrderDetails>();
        }
    }
}
