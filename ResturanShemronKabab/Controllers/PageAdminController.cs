using Microsoft.AspNetCore.Mvc;

namespace ResturanShemronKabab.Controllers
{
	public class PageAdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
