using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Category
{
    public class CategorySearchModel : PageModel2
    {
        public int? CategoryID { get; set; }

        public string? CategoryName { get; set; }
    }
}
