using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Food
{
    public class FoodSearchModel : PageModel2
    {
        public int? FoodID { get; set; }

        public string? FoodName { get; set; }

        public int? UnitPriceFrom { get; set; }

        public int? UnitPriceTo { get; set; }
    }
}
