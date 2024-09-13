using FrameWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;
using ResturanShemronKabab.ViewModel;

namespace ResturanShemronKabab.Controllers
{
    public class FoodManagementController : Controller
    {
        private readonly IFoodApplication foodApplication;

        private readonly ICategoryApplication categoryApplication;

		private readonly IHostEnvironment env;

		public FoodManagementController(IFoodApplication foodApplication , ICategoryApplication categoryApplication, IHostEnvironment env)
        {
            this.foodApplication = foodApplication;
            this.categoryApplication = categoryApplication;
            this.env = env;
        }

        private void InflateCategoryDrp()
        {
            var Cat = categoryApplication.GetDrp();
			Cat.Insert(0, new CategoryListForDrp { CategoryID = -1, CategoryName = "...Please Select..." });
			SelectList categoryDropDown = new SelectList(Cat, "CategoryID", "CategoryName");
			ViewBag.categoryDropDown = categoryDropDown;
		}

		public IActionResult Index(FoodSearchModel sm)
        {
            InflateCategoryDrp();
			return View(sm);
        }

        public IActionResult List()
        {
            var food = foodApplication.GetAllListItem();
            return View(food);
        }

		public IActionResult ListUI()
		{
			var food = foodApplication.GetAllListItemInUI();
			return View(food);
		}

		public IActionResult Add()
        {
			InflateCategoryDrp();
			return View();
        }
        [HttpPost]
        public JsonResult Add(FoodAddEditViewModel model)
        {
			//var op = foodApplication.Register(model);
			//return Json(op);
			string PhisycalAddress = Path.GetFileName(model.Picture.FileName).ToUniqueFileName();
			string Relativeaddress = @"~/Images/" + PhisycalAddress;
			PhisycalAddress = env.ContentRootPath + @"\wwwroot\Images\" + PhisycalAddress;
            FileStream fs = new FileStream(PhisycalAddress, FileMode.Create);
			{
				 model.Picture.CopyTo(fs);
			};
            FoodAddAndEditModel foodAddAndEditModel = new FoodAddAndEditModel
            {
                ImageURL = Relativeaddress,
                FoodName = model.FoodName,
                CategoryID = model.CategoryID,
                Materials = model.Materials,
                UnitPrice = model.UnitPrice,
                FoodID = model.FoodID,
            };
            var op = foodApplication.Register(foodAddAndEditModel);
            return Json(op);
		}

        [HttpPost]
        public JsonResult Remove(int ID)
        {
            var food = foodApplication.Get(ID);
			if (!string.IsNullOrEmpty(food.ImageURL))
			{
				var url = env.ContentRootPath + @"\wwwroot" + food.ImageURL.Substring(1, food.ImageURL.Length - 1).Replace(@"/", @"\");
				if (System.IO.File.Exists(url))
				{
					System.IO.File.Delete(url);
				}
			}
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

        public IActionResult Search(FoodSearchModel sm)
        {
            var food = foodApplication.Search(sm , out int recordCount);
            return PartialView("List",food);
        }
	}
}
