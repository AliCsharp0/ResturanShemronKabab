using FrameWork.DTOS;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Beverages;
using Restaurant.DomainModel.Models;
using Restaurant.DomainModel.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
	public class BeveragesRepository : IBeveragesRepository
	{
		private readonly RestaurantShemronKababContext db;

		public BeveragesRepository(RestaurantShemronKababContext db)
		{
			this.db = db;
		}

		public bool ExistBeveragesName(string BeveragesName)
		{
			return db.Beverages.Any(x => x.BeveragesName == BeveragesName);
		}

		public bool ExistImage(string Image)
		{
			return db.Beverages.Any(x => x.ImageURL == Image);
		}

		public Beverages Get(int ID)
		{
			return db.Beverages.FirstOrDefault(x => x.BeveragesID == ID);
		}

		public List<Beverages> GetAll()
		{
			return db.Beverages.ToList();
		}

		public List<BeveragesListItem> GetAllListItem()
		{
			var beverages = db.Beverages.Select(x => new BeveragesListItem
			{
				BeveragesID = x.BeveragesID,
				BeveragesName = x.BeveragesName,
				CategoryName = x.category.CategoryName,
				Image = x.ImageURL,
				UnitPrice = x.UnitPrice,
			}).ToList();
			return beverages;
		}

		public List<BeveragesListItemUI> GetAllListItemInUI()
		{
			var beverages = db.Beverages.Select(x => new BeveragesListItemUI
			{
				BeveragesID = x.BeveragesID,
				BeveragesName = x.BeveragesName,
				UnitPrice = x.UnitPrice,
				Image = x.ImageURL,
			}).ToList();
			return beverages;
		}

		public bool HasRelatedOrders(int BeveragesID)
		{
			return Get(BeveragesID).orderDetails.Any();
		}

		public OperationResult Register(Beverages Current)
		{
			OperationResult op = new OperationResult("Register Beverages");
			try
			{
				db.Beverages.Add(Current);
				db.SaveChanges();
				return op.ToSuccess("Registration Beverages Success Fully");
			}
			catch (Exception ex)
			{
				return op.ToFail("Registration Beverages Failed");
			}
		}

		public OperationResult Remove(int ID)
		{
			OperationResult op = new OperationResult("Remove Beverages");
			try
			{
				var beverages = Get(ID);
				db.Beverages.Remove(beverages);
				db.SaveChanges();
				return op.ToSuccess("Remove Beverages Success Fully");
			}
			catch (Exception ex)
			{
				return op.ToFail("Remove Beverages Failed" + ex.Message);
			}
		}

		public List<BeveragesListItem> Search(BeveragesSearchModel searchModel, out int RecordCount)
		{
			if (searchModel.PageSize == 0)
			{
				searchModel.PageSize = 5;
			}
			var q = from beverages in db.Beverages select beverages;
			if (searchModel.CategoryID != null && searchModel.CategoryID > 0)
			{
				q = q.Where(x => x.CategoryID == searchModel.CategoryID);
			}
			if (!string.IsNullOrEmpty(searchModel.BeveragesName))
			{
				q = q.Where(x => x.BeveragesName == searchModel.BeveragesName);
			}
			if (searchModel.UnitPriceFrom != null || searchModel.UnitPriceFrom > 0)
			{
				q = q.Where(x => x.UnitPrice >= searchModel.UnitPriceFrom);
			}
			if (searchModel.UnitPriceTo != null || searchModel.UnitPriceTo > 0)
			{
				q = q.Where(x => x.UnitPrice <= searchModel.UnitPriceTo);
			}
			RecordCount = q.Count();
			q = q.OrderByDescending(x => x.BeveragesID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);
			var q2 = from beverages in q
					 select new BeveragesListItem
					 {
						 BeveragesID = beverages.BeveragesID,
						 BeveragesName = beverages.BeveragesName,
						 CategoryName = beverages.category.CategoryName,
						 Image = beverages.ImageURL,
						 UnitPrice = beverages.UnitPrice,
					 };
			return q2.ToList();
		}

		public OperationResult Update(Beverages Current)
		{
			OperationResult op = new OperationResult("Update Beverages");
			var beve = db.Beverages.FirstOrDefault(x => x.BeveragesID == Current.BeveragesID);
			try
			{
				beve.BeveragesID = Current.BeveragesID;
				beve.BeveragesName = Current.BeveragesName;
				beve.CategoryID = Current.CategoryID;
				beve.UnitPrice = Current.UnitPrice;
				beve.ImageURL = Current.ImageURL;
				db.SaveChanges();
				return op.ToSuccess("Update Beverages Success Fully ");
			}
			catch (Exception ex)
			{
				return op.ToFail("Update Beverages Failed");
			}
		}

		public bool ExistImageInUpdate(int ID, string Image)
		{
			return db.Appetizers.Any(x => x.AppetizerID != ID && x.ImageURL == Image);
		}

		public bool ExistNameInUpdate(int ID, string Name)
		{
			return db.Appetizers.Any(x => x.AppetizerID != ID && x.AppetizerName == Name);
		}

		public void RemoveImage(int ID)
		{
			var beverages = db.Beverages.FirstOrDefault(x => x.BeveragesID == ID);
			if (beverages != null && beverages.ImageURL != string.Empty && beverages.ImageURL.ToLower() != "~/images/noimage.png")
			{
				beverages.ImageURL = "~/images/noimage.png";
				db.SaveChanges();
			}
		}
	}
}
