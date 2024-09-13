using FrameWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Beverages;
using Restaurant.DomainModel.ApplicationModel.Category;
using ResturanShemronKabab.ViewModel;

namespace ResturanShemronKabab.Controllers
{
    public class BeveragesManagementController : Controller
    {
        private readonly IBeveragesApplication beveragesApplication;
        private readonly ICategoryApplication categoryApplication;
        private readonly IHostEnvironment env;
        public BeveragesManagementController(IBeveragesApplication beveragesApplication , ICategoryApplication categoryApplication, IHostEnvironment env)
        {
            this.categoryApplication = categoryApplication;
            this.beveragesApplication = beveragesApplication;
            this.env= env; 
        }

		private void InflateCategoryDrp()
		{
			var Cat = categoryApplication.GetDrp();
			Cat.Insert(0, new CategoryListForDrp { CategoryID = -1, CategoryName = "...Please Select..." });
			SelectList categoryDropDown = new SelectList(Cat, "CategoryID", "CategoryName");
			ViewBag.categoryDropDown = categoryDropDown;
		}

		public IActionResult Index(BeveragesSearchModel sm)
        {
            InflateCategoryDrp();
            return View(sm);
        }

        public IActionResult List()
        {
            var beverages = beveragesApplication.GetAllListItem();
            return View(beverages);
        }

		public IActionResult ListUI()
		{
			var beve = beveragesApplication.GetAllListItemInUI();
			return View(beve);
		}

		public IActionResult Add()
        {
            InflateCategoryDrp();
            return View();
        }
        [HttpPost]
        public JsonResult Add(BeveragesAddEditViewModel model)
        {
            string PhisycalAddress = Path.GetFileName(model.Picture.FileName).ToUniqueFileName();
            string Relativeaddress = @"~/Images/" + PhisycalAddress;
            PhisycalAddress = env.ContentRootPath + @"\wwwroot\Images\" + PhisycalAddress;
            FileStream fs = new FileStream(PhisycalAddress, FileMode.Create);
            {
                model.Picture.CopyTo(fs);
            };
            BeveragesAddAndEditModel beveragesAddAndEditModel = new BeveragesAddAndEditModel
            {
                ImageURL = Relativeaddress,
                UnitPrice = model.UnitPrice,
                BeveragesID = model.BeveragesID,
                BeveragesName = model.BeveragesName,
                CategoryID = model.CategoryID,
            };
            var op = beveragesApplication.Register(beveragesAddAndEditModel);
            return Json(op);
        }


        [HttpPost]
		public JsonResult Remove(int id)
		{
			var beverages = beveragesApplication.Get(id);
			if (!string.IsNullOrEmpty(beverages.ImageURL))
			{
				var url = env.ContentRootPath + @"\wwwroot" + beverages.ImageURL.Substring(1, beverages.ImageURL.Length - 1).Replace(@"/", @"\");
				if (System.IO.File.Exists(url))
				{
					System.IO.File.Delete(url);
				}
			}
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

        public IActionResult Search(BeveragesSearchModel sm)
        {
            var beverages = beveragesApplication.Search(sm , out int recordCount);
            return PartialView("List", beverages);
        }
    }
}
