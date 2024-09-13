using Microsoft.AspNetCore.Mvc;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.ApplicationModel.User;

namespace ResturanShemronKabab.Controllers
{
	public class UserManagementController : Controller
	{
		private readonly IUserApplication userApplication;
        public UserManagementController(IUserApplication userApplication)
        {
            this.userApplication = userApplication;
        }
        public IActionResult Index(UserSearchModel sm)
		{
			return View(sm);
		}
		public IActionResult List()
		{
			var user = userApplication.GetAllListItem();
			return View(user);
		}
		public IActionResult Search(UserSearchModel sm)
		{
			var user = userApplication.Search(sm, out int recordCount);
			return PartialView("List", user);
		}
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		public JsonResult Add(UserAddAndEditModel model)
		{
			if(model.RoleID == 0)
			{
				model.RoleID = 2;
			}
			var op = userApplication.Register(model);
			return Json(op);
		}
		[HttpPost]
		public JsonResult Remove(int ID)
		{
			var op = userApplication.Delete(ID);
			return Json(op);
		}
		public IActionResult Update(int id)
		{
			var user = userApplication.Get(id);
			return View(user);
		}
		[HttpPost]
		public JsonResult Update(UserAddAndEditModel model)
		{
			if (model.RoleID == 0)
			{
				model.RoleID = 2;
			}
			var op = userApplication.Update(model);
			return Json(op);
		}

	}
}
