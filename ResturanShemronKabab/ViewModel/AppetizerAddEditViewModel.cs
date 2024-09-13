using System.ComponentModel.DataAnnotations;
namespace ResturanShemronKabab.ViewModel
{
	public class AppetizerAddEditViewModel
	{
		public int AppetizerID { get; set; }

		public int CategoryID { get; set; }

		public string AppetizerName { get; set; }

		public int UnitPrice { get; set; }

		public string SmallDescription { get; set; }

		public IFormFile Picture { get; set; }
	}
}
