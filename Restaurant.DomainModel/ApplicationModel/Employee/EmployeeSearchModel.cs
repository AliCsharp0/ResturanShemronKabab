using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Employee
{
    public class EmployeeSearchModel : PageModel2
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public int? Age { get; set; }

        public double? RightsFrom { get; set; }

        public double? RightsTo { get; set; }
    }
}
