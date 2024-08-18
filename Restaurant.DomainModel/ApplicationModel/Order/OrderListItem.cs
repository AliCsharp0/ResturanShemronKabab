using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Order
{
    public class OrderListItem
    {
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public int EmployeeID { get; set; }

        public int CustomerID { get; set; }

        public string SmallDescription { get; set; }

        public int AddressID { get; set; }//Address Restaurant
    }
}
