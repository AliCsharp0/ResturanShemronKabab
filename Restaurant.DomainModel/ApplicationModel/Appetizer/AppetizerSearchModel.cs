using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Appetizer
{
    public class AppetizerSearchModel : PageModel2
    {
        public int? AppetizerID { get; set; }

        public string? AppetizerName { get; set; }

        public int? UnitPriceFrom { get; set; }

        public int? UnitPriceTo { get; set; }
    }
}
