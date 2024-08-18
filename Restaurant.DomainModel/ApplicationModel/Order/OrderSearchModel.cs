using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Order
{
    public class OrderSearchModel: PageModel2
    {
        public int? OrderID { get; set; }

        //public string? EmployeeName { get; set; }

        //public string? CustomerName { get; set; }

        public DateTime? OrderDate { get; set; }
    }
}
