using FrameWork;
using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;
using ResturanShemronKabab.ViewModel;
using System.Security.Policy;

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
        public IActionResult Add(AppetizerAddEditViewModel model)
        {
            if (model.Picture == null)
            {
                InflateCategoryDrp();
                TempData["ErrorMessage"] = "Please select the food";
                return View(model);
            }
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

		public IActionResult Search(AppetizerSearchModel searchModel)
		{
			var appetizer = appetizerApplication.Search(searchModel, out int recordCount);
			return PartialView("List", appetizer);
		}

		public IActionResult Update(int appetizerID)
		{
			var oldAppetizer = appetizerApplication.Get(appetizerID);
			InflateCategoryDrp();
			var appetizer = new AppetizerDetailsModel
			{
				AppetizerID = appetizerID,
				AppetizerName = oldAppetizer.AppetizerName,
				CategoryID = oldAppetizer.CategoryID,
				ImageURl = oldAppetizer.ImageURL,
				UnitPrice = oldAppetizer.UnitPrice,
				SmallDescription = oldAppetizer.SmallDescription,
			};
			return View(appetizer);
		}

		[HttpPost]
		public IActionResult Update(AppetizerAddEditViewModel model)
		{
			var oldAppetizer = appetizerApplication.Get(model.AppetizerID);
			if (model.Picture == null)
			{
				AppetizerAddAndEditModel NewAppetizer = new AppetizerAddAndEditModel
				{
					ImageURL = oldAppetizer.ImageURL,
					AppetizerID = model.AppetizerID,
					AppetizerName = model.AppetizerName,
					UnitPrice = model.UnitPrice,
					SmallDescription = model.SmallDescription,
					CategoryID =model.CategoryID,
				};
				var opExist = appetizerApplication.Update(NewAppetizer);
				var appetizer = new AppetizerDetailsModel
				{
					AppetizerID = oldAppetizer.AppetizerID,
					AppetizerName = oldAppetizer.AppetizerName,
					UnitPrice = oldAppetizer.UnitPrice,
					SmallDescription = oldAppetizer.SmallDescription,
					CategoryID = oldAppetizer.CategoryID,
					ImageURl = oldAppetizer.ImageURL,
				};
				if (!opExist.Success)
				{
					InflateCategoryDrp();
					TempData["ErrorMessage"] = opExist.Message;
					return View(appetizer);
				}
				return RedirectToAction("Index");
			}

			else//agar ax bood
			{

				if (!string.IsNullOrEmpty(oldAppetizer.ImageURL) && oldAppetizer.ImageURL.ToLower() != "~/images/noimage.png")
				{
					var url = env.ContentRootPath + @"\wwwroot" + oldAppetizer.ImageURL.Substring(1, oldAppetizer.ImageURL.Length - 1).Replace(@"/", @"\");
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
				AppetizerAddAndEditModel n = new AppetizerAddAndEditModel
				{
					AppetizerName = model.AppetizerName,
					ImageURL = Relativeaddress,
					AppetizerID = model.AppetizerID,
					SmallDescription = model.SmallDescription,
					CategoryID = model.CategoryID,
					UnitPrice = model.UnitPrice,
				};
				var op = appetizerApplication.Update(n);
				var appetizer = new AppetizerDetailsModel
				{
					AppetizerID = oldAppetizer.AppetizerID,
					CategoryID = oldAppetizer.CategoryID,
					ImageURl = oldAppetizer.ImageURL,
					SmallDescription = oldAppetizer.SmallDescription,
					UnitPrice = oldAppetizer.UnitPrice,
					AppetizerName = oldAppetizer.AppetizerName,
				};
				if (op.Success)
				{
					return RedirectToAction("index");

				}
				else
				{
					InflateCategoryDrp();
					TempData["ErrorMessage"] = op.Message;
					return View(appetizer);
				}

			}
		}

		[HttpPost]
		public JsonResult DeleteImage(int appetizerID)
		{
			var n = appetizerApplication.Get(appetizerID);
			if (n != null && !string.IsNullOrEmpty(n.ImageURL) && n.ImageURL.ToLower() != @"~/images/noimage.png")
			{
				var url = Path.Combine(env.ContentRootPath, "wwwroot", n.ImageURL.Substring(1).Replace("/", "\\"));
				if (System.IO.File.Exists(url))
				{
					System.IO.File.Delete(url);
				}
			}
			OperationResult op = new OperationResult("Delete Image ");
			try
			{
				appetizerApplication.RemoveImage(appetizerID);
				return Json(op.ToSuccess("Delete Image Success Fully"));
			}
			catch (Exception ex)
			{
				return Json(op.ToFail("Image did not Removed"));
			}
		}
	}
}
