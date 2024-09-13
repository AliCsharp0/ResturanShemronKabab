using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models
{
    public class Beverages
    {
        public int BeveragesID { get; set; }

        public int CategoryID { get; set; }

        public string BeveragesName { get; set; }

        public int UnitPrice { get; set; }

        public string ImageURL { get; set; }

        public Category category { get; set; }

        public List<OrderDetails> orderDetails { get; set; }

        public Beverages()
        {
            this.orderDetails = new List<OrderDetails>();
        }
    }
}
