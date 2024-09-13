using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Category;

namespace ResturanShemronKabab.Controllers
{
	public class CategoryManagementController : Controller
	{
		private readonly ICategoryApplication CatApplication;

		public CategoryManagementController(ICategoryApplication CatApplication)
		{
			this.CatApplication = CatApplication;
		}

		private void BindRoods()
		{
			var cats = CatApplication.GetRoots();
			cats.Insert(0, new CategoryListItem { CategoryName = "ادسته اصلی" });
			ViewBag.drpRoots = new SelectList(cats, "CategoryID", "CategoryName");
		}

		public IActionResult Index(CategorySearchModel sm)
		{
			return View(sm);
		}

		public IActionResult List()
		{
			var Cats = CatApplication.GetAllListItem();
			return View(Cats);
		}

		public IActionResult Add()
		{
			BindRoods();
			return View();
		}

		[HttpPost]
		public JsonResult Add(CategoryAddAndEditModel model)
		{
			if (model.ParentID == 0)
			{
				model.ParentID = null;
			}
			var op = CatApplication.Register(model);
			return Json(op);
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			var op = CatApplication.Remove(id);
			return Json(op);
		}

		public IActionResult Update(int id)
		{
			BindRoods();
			var cat = CatApplication.Get(id);
			return View(cat);
		}
		[HttpPost]
		public JsonResult Update (CategoryAddAndEditModel model)
		{
			var op = CatApplication.Update(model);
			return Json(op);
		}

		public IActionResult Search (CategorySearchModel searchModel)
		{
			var cat = CatApplication.Search(searchModel, out int recordCount);
			return PartialView("List", cat);
		}
	}
}
