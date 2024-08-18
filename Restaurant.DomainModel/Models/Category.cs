using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string SmallDescription { get; set; }

        public int? FoodCountInCategory { get; set; }

        public int? AppetizerCountInCategory { get; set; }

        public int? BeverageCountInCategory { get; set; }

		public string Slug { get; set; }

		public int? ParentID { get; set; }

		public Category Parent { get; set; }

		public List<Category> Children { get; set; }

		public ICollection<Food> Foods { get; set; }

        public ICollection<Beverages> beverages { get; set; }

        public ICollection<Appetizer> appetizers { get; set; }

        public Category()
        {
            this.Children = new List<Category>();

            this.Foods = new List<Food>();

            this.appetizers = new List<Appetizer>();

            this.beverages = new List<Beverages>();
        }
    }
}
