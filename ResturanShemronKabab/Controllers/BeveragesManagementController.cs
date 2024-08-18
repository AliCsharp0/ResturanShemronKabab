using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Beverages;
using Restaurant.DomainModel.ApplicationModel.Category;

namespace ResturanShemronKabab.Controllers
{
    public class BeveragesManagementController : Controller
    {
        private readonly IBeveragesApplication beveragesApplication;
        private readonly ICategoryApplication categoryApplication;
        public BeveragesManagementController(IBeveragesApplication beveragesApplication , ICategoryApplication categoryApplication)
        {
            this.categoryApplication = categoryApplication;
            this.beveragesApplication = beveragesApplication;
        }

		private void InflateCategoryDrp()
		{
			var Cat = categoryApplication.GetDrp();
			Cat.Insert(0, new CategoryListForDrp { CategoryID = -1, CategoryName = "...Please Select..." });
			SelectList categoryDropDown = new SelectList(Cat, "CategoryID", "CategoryName");
			ViewBag.categoryDropDown = categoryDropDown;
		}

		public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var beverages = beveragesApplication.GetAllListItem();
            return View(beverages);
        }

        public IActionResult Add()
        {
            InflateCategoryDrp();
            return View();
        }
        [HttpPost]
        public JsonResult Add(BeveragesAddAndEditModel model)
        {
            var op = beveragesApplication.Register(model);
            return Json(op);
        }

		[HttpPost]
		public JsonResult Remove(int id)
		{
			var op = beveragesApplication.Remove(id);
			return Json(op);
		}

		public IActionResult Update(int id)
        {
            InflateCategoryDrp();
            var beverages = beveragesApplication.Get(id);
            return View(beverages);
        }

        [HttpPost]
        public JsonResult Update(BeveragesAddAndEditModel model)
        {
            var op = beveragesApplication.Update(model);
            return Json(op);
        }
    }
}
