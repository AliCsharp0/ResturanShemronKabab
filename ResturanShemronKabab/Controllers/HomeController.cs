using Microsoft.AspNetCore.Mvc;
using ResturanShemronKabab.Models;
using Security.Domain;
using System.Diagnostics;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		var fullname = User.Identity.IsAuthenticated ? User.Claims.FirstOrDefault(q => q.Type == "FullName")?.Value : null;
		ViewBag.FullName = fullname;
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
