using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models
{
    public class Food
    {
        public int FoodID { get; set; }

        public int CategoryID { get; set; }

        public string FoodName { get; set; }

        public int UnitPrice { get; set; }

        public string Materials { get; set; }

        public string ImageURL { get; set; }

		public Category category { get; set; }

        public ICollection<OrderDetails> orderDetails { get; set; }

        public Food()
        {
            this.orderDetails = new List<OrderDetails>();
        }
    }
}
