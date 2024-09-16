using Framework;
using Framework.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Security.ApplicationServiceContract.Services;
using Security.Domain;
using Security.Domain.ApplicationModel;

namespace EshopMashtiHasan.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountApplication buss;
        private readonly IPasswordHasher PasswordHasher;
        public AccountController(IAccountApplication buss, IPasswordHasher PasswordHasher)
        {
            this.buss = buss;
            this.PasswordHasher = PasswordHasher;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Security.Domain.DTO.User.Login l)
        {
            var op = buss.Login(l);
            if (!op.Success)
            {
                ViewBag.ErrorMessage = op.Message;
                return View(l);
            }

            //TODO 

            return RedirectToAction("Index", "Home");
        }
		//     public IActionResult Create()
		//     {
		//         return View();
		//     }
		//     [HttpPost]
		//     [ValidateAntiForgeryToken]
		//     public IActionResult Create(UserAddModelCustomer u)
		//     {
		//         u.RoleID = 2;//Customer
		//         u.Password = PasswordHasher.Hash(u.Password);
		//        var op = buss.Register(u);
		//if (op.Success)
		//{
		//	return RedirectToAction("Index", "Home");
		//}
		//else
		//         {
		//	TempData["ErrorMessage"] = op;
		//	return View(u);
		//}

		//     }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(UserAddModelCustomer u)
		{
			u.RoleID = 2; // Customer
			u.Password = PasswordHasher.Hash(u.Password);
			var op = buss.Register(u);
			if (op.Success)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["ErrorMessage"] = op.Message; // Set only the error message in TempData
				return View(u); // Return the view with the user model
			}
		}
		public ActionResult AccessDenied()
        {
            return View();
        }
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

	}
}
