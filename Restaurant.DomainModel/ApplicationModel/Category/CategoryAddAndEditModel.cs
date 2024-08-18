using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Category
{
    public class CategoryAddAndEditModel
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string SmallDescription { get; set; }

        public string Slug { get; set; }

        public int? ParentID {  get; set; }

        public Restaurant.DomainModel.Models.Category Parent { get; set; }

    }
}
