using FrameWork;
using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Beverages;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using ResturanShemronKabab.ViewModel;

namespace ResturanShemronKabab.Controllers
{
	public class BeveragesManagementController : Controller
	{
		private readonly IBeveragesApplication beveragesApplication;
		private readonly ICategoryApplication categoryApplication;
		private readonly IHostEnvironment env;
		public BeveragesManagementController(IBeveragesApplication beveragesApplication, ICategoryApplication categoryApplication, IHostEnvironment env)
		{
			this.categoryApplication = categoryApplication;
			this.beveragesApplication = beveragesApplication;
			this.env = env;
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
		public IActionResult Add(BeveragesAddEditViewModel model)
		{
            if (model.Picture == null)
            {
                InflateCategoryDrp();
                TempData["ErrorMessage"] = "Please select the food";
                return View(model);
            }
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
            if (!op.Success)
            {
                InflateCategoryDrp();
                TempData["ErrorMessage"] = op.Message;
                return View(model);
            }
            return RedirectToAction("index");
        }


		[HttpPost]
		public JsonResult Remove(int id)
		{
			//var beverages = beveragesApplication.Get(id);
			//if (!string.IsNullOrEmpty(beverages.ImageURL))
			//{
			//	var url = env.ContentRootPath + @"\wwwroot" + beverages.ImageURL.Substring(1, beverages.ImageURL.Length - 1).Replace(@"/", @"\");
			//	if (System.IO.File.Exists(url))
			//	{
			//		System.IO.File.Delete(url);
			//	}
			//}
			var op = beveragesApplication.Remove(id);
			return Json(op);
		}

		public IActionResult Update(int beveragesID)
		{
			var n = beveragesApplication.Get(beveragesID);
			InflateCategoryDrp();
			var beverages = new BeveragesDetailsModel
			{
				BeveragesID = beveragesID,
				BeveragesName = n.BeveragesName,
				UnitPrice = n.UnitPrice,
				ImageURL = n.ImageURL,
				CategoryID = n.CategoryID,
			};
			return View(beverages);
		}

		[HttpPost]
		public IActionResult Update(BeveragesAddEditViewModel model)
		{
			var oldBeverages = beveragesApplication.Get(model.BeveragesID);
			if (model.Picture == null)
			{
				BeveragesAddAndEditModel NewBeverages = new BeveragesAddAndEditModel
				{
					ImageURL = oldBeverages.ImageURL,
					BeveragesID = model.BeveragesID,
					CategoryID = model.CategoryID,
					UnitPrice = model.UnitPrice,
					BeveragesName = model.BeveragesName,
				};
				var opExist = beveragesApplication.Update(NewBeverages);
				var beverages = new BeveragesDetailsModel
				{
					BeveragesID = oldBeverages.BeveragesID,
					ImageURL = oldBeverages.ImageURL,
					BeveragesName = oldBeverages.BeveragesName,
					UnitPrice = oldBeverages.UnitPrice,
					CategoryID = oldBeverages.CategoryID,
				};
				if (!opExist.Success)
				{
					InflateCategoryDrp();
					TempData["ErrorMessage"] = opExist.Message;
					return View(beverages);
				}
				return RedirectToAction("Index");
			}

			else//agar ax bood
			{

				if (!string.IsNullOrEmpty(oldBeverages.ImageURL) && oldBeverages.ImageURL.ToLower() != "~/images/noimage.png")
				{
					var url = env.ContentRootPath + @"\wwwroot" + oldBeverages.ImageURL.Substring(1, oldBeverages.ImageURL.Length - 1).Replace(@"/", @"\");
					if (System.IO.File.Exists(url))
					{
						System.IO.File.Delete(url);
					}
				}
				string PhisycalAddress = Path.GetFileName(model.Picture.FileName);
				string Relativeaddress = @"~/Images/" + PhisycalAddress;
				PhisycalAddress = env.ContentRootPath + @"\wwwroot\Images\" + PhisycalAddress;
				using (FileStream fs = new FileStream(PhisycalAddress, FileMode.Create))
				{
					model.Picture.CopyTo(fs);
				}
				BeveragesAddAndEditModel n = new BeveragesAddAndEditModel
				{
					BeveragesID = model.BeveragesID,
					ImageURL = Relativeaddress,
					BeveragesName = model.BeveragesName,
					CategoryID = model.CategoryID,
					UnitPrice = model.UnitPrice,
				};
				var op = beveragesApplication.Update(n);
				var food = new BeveragesDetailsModel
				{
					BeveragesID = oldBeverages.BeveragesID,
					BeveragesName = oldBeverages.BeveragesName,
					CategoryID = oldBeverages.CategoryID,
					ImageURL = oldBeverages.ImageURL,
					UnitPrice = oldBeverages.UnitPrice,
				};
				if (op.Success)
				{
					return RedirectToAction("index");

				}
				else
				{
					InflateCategoryDrp();
					TempData["ErrorMessage"] = op.Message;
					return View(food);
				}

			}
		}

		public IActionResult Search(BeveragesSearchModel sm)
		{
			var beverages = beveragesApplication.Search(sm, out int recordCount);
			return PartialView("List", beverages);
		}

		[HttpPost]
		public JsonResult RemoveImage(int beveragesID)
		{
			var oldBeverages = beveragesApplication.Get(beveragesID);
			if (oldBeverages == null && !string.IsNullOrEmpty(oldBeverages.ImageURL) && oldBeverages.ImageURL != @"~/images/noimage.png")
			{
				var url = Path.Combine(env.ContentRootPath, "wwwroot", oldBeverages.ImageURL.Substring(1).Replace("/", "\\"));
				if (System.IO.File.Exists(url))
				{
					System.IO.File.Delete(url);
				}
			}
			OperationResult op = new OperationResult("Delete Image ");
			try
			{
				beveragesApplication.RemoveImage(beveragesID);
				return Json(op.ToSuccess("Delete Image Success Fully"));
			}
			catch(Exception ex)
			{
				return Json(op.ToFail("Delete Image Failed " + ex.Message));
			}
		}
	}
}
