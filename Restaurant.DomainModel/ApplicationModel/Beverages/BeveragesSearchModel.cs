using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Beverages
{
    public class BeveragesSearchModel : PageModel2
    {
        public int? BeveragesID { get; set; }

        public string? BeveragesName { get; set; }

        public int? UnitPriceFrom { get; set; }

        public int? UnitPriceTo { get; set; }
    }
}
