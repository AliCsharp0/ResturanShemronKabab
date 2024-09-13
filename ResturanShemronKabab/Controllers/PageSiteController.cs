using Microsoft.AspNetCore.Mvc;

namespace ResturanShemronKabab.Controllers
{
	public class PageSiteController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
