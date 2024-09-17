using FrameWork;
using FrameWork.DTOS;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using ResturanShemronKabab.Framwork.UI.Services;
using ResturanShemronKabab.ViewModel;
using System.Net.NetworkInformation;

namespace ResturanShemronKabab.Controllers
{
	public class FoodManagementController : Controller
	{
		private readonly IFoodApplication foodApplication;

		private readonly ICategoryApplication categoryApplication;

		private readonly IHostEnvironment env;

		public FoodManagementController(IFoodApplication foodApplication, ICategoryApplication categoryApplication, IHostEnvironment env)
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
			string PhisycalAddress = Path.GetFileName(model.Picture.FileName);
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
			//         var food = foodApplication.Get(ID);
			//if (!string.IsNullOrEmpty(food.ImageURL))
			//{
			//	var url = env.ContentRootPath + @"\wwwroot" + food.ImageURL.Substring(1, food.ImageURL.Length - 1).Replace(@"/", @"\");
			//	if (System.IO.File.Exists(url))
			//	{
			//		System.IO.File.Delete(url);
			//	}
			//}
			var op = foodApplication.Remove(ID);
			return Json(op);
		}

		public IActionResult Search(FoodSearchModel sm)
		{
			var food = foodApplication.Search(sm, out int recordCount);
			return PartialView("List", food);
		}

		[HttpPost]
		public JsonResult DeleteImage(int foodID)
		{
			var n = foodApplication.Get(foodID);
			if (n != null && !string.IsNullOrEmpty(n.ImageURL) && n.ImageURL.ToLower() != @"~/images/noimage.png")
			{
				var url = Path.Combine(env.ContentRootPath, "wwwroot", n.ImageURL.Substring(1).Replace("/", "\\"));
				if (System.IO.File.Exists(url))
				{
					System.IO.File.Delete(url);
				}
			}
			OperationResult op = new OperationResult("Delete Image");
			try
			{
				foodApplication.RemoveImage(foodID);
				return Json(op.ToSuccess("Image Removed"));
			}
			catch (Exception)
			{
				return Json(op.ToFail("Image did not Removed"));
			}
		}


		public IActionResult Update(int foodID)
		{
			var n = foodApplication.Get(foodID);
			InflateCategoryDrp();
			var food = new FoodDetailsModel
			{
				FoodID = foodID,
				FoodName = n.FoodName,
				CategoryID = n.CategoryID,
				ImageURL = n.ImageURL,
				Materials = n.Materials,
				UnitPrice = n.UnitPrice,
			};
			return View(food);
		}

		[HttpPost]
		public IActionResult Update(FoodAddEditViewModel model)
		{
			var oldFood = foodApplication.Get(model.FoodID);
			if (model.Picture == null)
			{
				FoodAddAndEditModel NewFood = new FoodAddAndEditModel
				{
					FoodID = model.FoodID,
					ImageURL = oldFood.ImageURL,
					FoodName = model.FoodName,
					CategoryID = model.CategoryID,
					Materials = model.Materials,
					UnitPrice = model.UnitPrice,
				};
				var opExist = foodApplication.Update(NewFood);
				var food = new FoodDetailsModel
				{
					FoodID = oldFood.FoodID,
					FoodName = oldFood.FoodName,
					CategoryID = oldFood.CategoryID,
					ImageURL = oldFood.ImageURL,
					Materials = oldFood.Materials,
					UnitPrice = oldFood.UnitPrice,
				};
				if (!opExist.Success)
				{
					InflateCategoryDrp();
					TempData["ErrorMessage"] = opExist.Message;
					return View(food);
				}
				return RedirectToAction("Index");
			}

			else//agar ax bood
			{

				if (!string.IsNullOrEmpty(oldFood.ImageURL) && oldFood.ImageURL.ToLower() != "~/images/noimage.png")
				{
					var url = env.ContentRootPath + @"\wwwroot" + oldFood.ImageURL.Substring(1, oldFood.ImageURL.Length - 1).Replace(@"/", @"\");
					if (System.IO.File.Exists(url))
					{
						System.IO.File.Delete(url);
					}
				}
				string PhisycalAddress = Path.GetFileName(model.Picture.FileName);
				string Relativeaddress = @"~/Images/" + PhisycalAddress;
				PhisycalAddress = env.ContentRootPath + @"\wwwroot\Images\" + PhisycalAddress;
				FileStream fs = new FileStream(PhisycalAddress, FileMode.Create);
				{
					model.Picture.CopyToAsync(fs);
					fs.Close();
				};
				FoodAddAndEditModel n = new FoodAddAndEditModel
				{
					FoodID = model.FoodID,
					ImageURL = Relativeaddress,
					FoodName = model.FoodName,
					CategoryID = model.CategoryID,
					Materials = model.Materials,
					UnitPrice = model.UnitPrice,
				};
				var op = foodApplication.Update(n);
				var food = new FoodDetailsModel
				{
					FoodID = oldFood.FoodID,
					FoodName = oldFood.FoodName,
					CategoryID = oldFood.CategoryID,
					ImageURL = oldFood.ImageURL,
					Materials = oldFood.Materials,
					UnitPrice = oldFood.UnitPrice,
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
	}
}
