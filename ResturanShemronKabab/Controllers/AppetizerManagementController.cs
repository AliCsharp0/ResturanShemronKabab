using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Category;

namespace ResturanShemronKabab.Controllers
{
	public class AppetizerManagementController : Controller
	{
		private readonly IAppetizerApplication appetizerApplication;

		private readonly ICategoryApplication categoryApplication;

        public AppetizerManagementController(IAppetizerApplication appetizerApplication , ICategoryApplication categoryApplication)
        {
            this.appetizerApplication = appetizerApplication;
			this.categoryApplication = categoryApplication;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult List()
		{
			var Aappe = appetizerApplication.GetAllListItem();
			return View(Aappe);
		}

		private void InflateCategoryDrp()
		{
			var Cat = categoryApplication.GetDrp();
			Cat.Insert(0, new CategoryListForDrp { CategoryID = -1, CategoryName = "...Please Select..." });
			SelectList categoryDropDown = new SelectList(Cat, "CategoryID", "CategoryName");
			ViewBag.categoryDropDown = categoryDropDown;
		}

		public IActionResult Add(int id)
		{
			InflateCategoryDrp();
			return View();
		}
		[HttpPost]
		public JsonResult Add(AppetizerAddAndEditModel model)
		{
			var op = appetizerApplication.Register(model);
			return Json(op);
		}

		[HttpPost]
		public JsonResult Remove(int id)
		{
			var op = appetizerApplication.Remove(id);
			return Json(op);
		}

		public IActionResult Update(int id)
		{
			InflateCategoryDrp();
			var appetizer = appetizerApplication.Get(id);
			return View( appetizer);
		}
		[HttpPost]
		public JsonResult Update(AppetizerAddAndEditModel model)
		{
			var op = appetizerApplication.Update(model);
			return Json(op);
		}

	}
}
