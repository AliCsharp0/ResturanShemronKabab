using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public int EmployeeID { get; set; }

        public int CustomerID { get; set; }

        public string SmallDescription { get; set; }

        public int AddressID { get; set; }//Address Restaurant

        public List<OrderDetails> orderDetails { get; set; }

        public Order()
        {
            this.orderDetails = new List<OrderDetails>();
        }
    }
}
