using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;

namespace ResturanShemronKabab.Controllers
{
    public class FoodManagementController : Controller
    {
        private readonly IFoodApplication foodApplication;

        private readonly ICategoryApplication categoryApplication;

        public FoodManagementController(IFoodApplication foodApplication , ICategoryApplication categoryApplication)
        {
            this.foodApplication = foodApplication;
            this.categoryApplication = categoryApplication;
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
            var food = foodApplication.GetAllListItem();
            return View(food);
        }

        public IActionResult Add()
        {
			InflateCategoryDrp();
			return View();
        }
        [HttpPost]
        public JsonResult Add(FoodAddAndEditModel model)
        {
            var op = foodApplication.Register(model);
            return Json(op);
        }

        [HttpPost]
        public JsonResult Remove(int ID)
        {
            var op = foodApplication.Remove(ID);
            return Json(op);
        }

        public IActionResult Update(int id)
        {
            InflateCategoryDrp();
            var food = foodApplication.Get(id);
			return View(food);
        }

        [HttpPost]
        public JsonResult Update(FoodAddAndEditModel model)
        {
            var op = foodApplication.Update(model);
            return Json(op);
        }
    }
}
