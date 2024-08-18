using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Appetizer
{
    public class AppetizerListItem
    {
        public int AppetizerID { get; set; }

        public string CategoryName { get; set; }

        public string AppetizerName { get; set; }

        public int UnitPrice { get; set; }

        public string Image { get; set; }
    }
}
