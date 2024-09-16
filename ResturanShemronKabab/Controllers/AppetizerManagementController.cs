using FrameWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;
using ResturanShemronKabab.ViewModel;

namespace ResturanShemronKabab.Controllers
{
	public class AppetizerManagementController : Controller
	{
		private readonly IAppetizerApplication appetizerApplication;

		private readonly ICategoryApplication categoryApplication;

        private readonly IHostEnvironment env;

        public AppetizerManagementController(IAppetizerApplication appetizerApplication , ICategoryApplication categoryApplication, IHostEnvironment env) 
        {
            this.appetizerApplication = appetizerApplication;
			this.categoryApplication = categoryApplication;
			this.env = env;
        }

        public IActionResult Index(AppetizerSearchModel sm)
		{
			InflateCategoryDrp();
			return View(sm);
		}

		public IActionResult List()
		{
			var Aappe = appetizerApplication.GetAllListItem();
			return View(Aappe);
		}

		public IActionResult ListUI()
		{
			var appetizer = appetizerApplication.GetAllListItemInUI();
			return View(appetizer);
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
        public JsonResult Add(AppetizerAddEditViewModel model)
        {
            string PhisycalAddress = Path.GetFileName(model.Picture.FileName);
            string Relativeaddress = @"~/Images/" + PhisycalAddress;
            PhisycalAddress = env.ContentRootPath + @"\wwwroot\Images\" + PhisycalAddress;
            FileStream fs = new FileStream(PhisycalAddress, FileMode.Create);
            {
                model.Picture.CopyTo(fs);
            };
            AppetizerAddAndEditModel appetizerAddAndEditModel = new AppetizerAddAndEditModel
            {
                ImageURL = Relativeaddress,
				AppetizerID = model.AppetizerID,
				CategoryID = model.CategoryID,
				AppetizerName = model.AppetizerName,
			    SmallDescription = model.SmallDescription,
				UnitPrice = model.UnitPrice,
               
            };
            var op = appetizerApplication.Register(appetizerAddAndEditModel);
            return Json(op);
        }

        [HttpPost]
		public JsonResult Remove(int id)
		{
			//var appetizer = appetizerApplication.Get(id);
			//if (!string.IsNullOrEmpty(appetizer.ImageURL))
			//{
			//	var url = env.ContentRootPath + @"\wwwroot" + appetizer.ImageURL.Substring(1, appetizer.ImageURL.Length - 1).Replace(@"/", @"\");
			//	if (System.IO.File.Exists(url))
			//	{
			//		System.IO.File.Delete(url);
			//	}
			//}
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

		public IActionResult Search(AppetizerSearchModel searchModel)
		{
			var appetizer = appetizerApplication.Search(searchModel, out int recordCount);
			return PartialView("List", appetizer);
		}

	}
}
