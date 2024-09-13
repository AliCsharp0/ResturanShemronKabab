using System.ComponentModel.DataAnnotations;

namespace ResturanShemronKabab.ViewModel
{
	public class FoodAddEditViewModel
	{
		public int FoodID { get; set; }

		public int CategoryID { get; set; }

		public string FoodName { get; set; }

		public int UnitPrice { get; set; }

		public string Materials { get; set; }

		public IFormFile Picture { get; set; }
	}
}
