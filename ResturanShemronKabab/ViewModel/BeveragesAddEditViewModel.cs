using System.ComponentModel.DataAnnotations;
namespace ResturanShemronKabab.ViewModel
{
	public class BeveragesAddEditViewModel
	{
		public int BeveragesID { get; set; }

		public int CategoryID { get; set; }

		public string BeveragesName { get; set; }

		public int UnitPrice { get; set; }

		public IFormFile Picture { get; set; }
	}
}
