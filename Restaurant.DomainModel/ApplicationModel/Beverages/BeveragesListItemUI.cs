using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DomainModel.ApplicationModel.Beverages
{
	public class BeveragesListItemUI
	{
		public int BeveragesID { get; set; }

		public string BeveragesName { get; set; }

		public int UnitPrice { get; set; }

		public string Image { get; set; }
	}
}
