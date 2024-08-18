using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Category
{
    public class CategoryListItem
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string SmallDescription { get; set; }

        public int FoodCountInCategory { get; set; }

        public int AppetizerCountInCategory { get; set; }

        public int BeverageCountInCategory { get; set; }

        public string Slug { get; set; }

    }
}
