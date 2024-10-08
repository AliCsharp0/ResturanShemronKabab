﻿using FrameWork.DTOS;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Employee;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
	public class AppetizerRepository : IAppetizerRepository
	{
		private readonly RestaurantShemronKababContext db;

		public AppetizerRepository(RestaurantShemronKababContext db)
		{
			this.db = db;
		}

		public bool ExistAppetizerName(string AppetizerName)
		{
			return db.Appetizers.Any(x => x.AppetizerName == AppetizerName);
		}

		public bool ExistImage(string Image)
		{
			return db.Appetizers.Any(x => x.ImageURL == Image);
		}

        public bool ExistImageInUpdate(int ID, string Image)
        {
            return db.Appetizers.Any(x=>x.AppetizerID != ID && x.ImageURL == Image);
        }

        public bool ExistNameInUpdate(int ID, string Name)
        {
			return db.Appetizers.Any(x => x.AppetizerID != ID && x.AppetizerName == Name);
        }

        public Appetizer Get(int ID)
		{
			return db.Appetizers.FirstOrDefault(x => x.AppetizerID == ID);
		}

		public List<Appetizer> GetAll()
		{
			return db.Appetizers.ToList();
		}

		public List<AppetizerListItem> GetAllListItem()
		{
			var appetizer = db.Appetizers.Select(x => new AppetizerListItem
			{
				AppetizerID = x.AppetizerID,
				AppetizerName = x.AppetizerName,
				CategoryName = x.category.CategoryName,
				Image = x.ImageURL,
				UnitPrice = x.UnitPrice,
			}).ToList();
			return appetizer;
		}

		public List<AppetizerListItemUI> GetAllListItemInUI()
		{
			var appetizer = db.Appetizers.Select(x => new AppetizerListItemUI
			{
				AppetizerID = x.AppetizerID,
				AppetizerName= x.AppetizerName,
				UnitPrice= x.UnitPrice,
				Image = x.ImageURL,
			}).ToList();
			return appetizer;
		}

		public bool HasRelatedOrders(int AppetizerID)
		{
			return Get(AppetizerID).orderDetails.Any();
		}

		public OperationResult Register(Appetizer Current)
		{
			OperationResult op = new OperationResult("Register Appetizer");
			try
			{
				db.Appetizers.Add(Current);
				db.SaveChanges();
				return op.ToSuccess("Registration Appetizer Success Fully");
			}
			catch (Exception ex)
			{
				return op.ToFail("Registration Appetizer Failed" + ex.Message);
			}
		}

		public OperationResult Remove(int ID)
		{
			OperationResult op = new OperationResult("Remove Appetizer");
			try
			{
				var appetizer = Get(ID);
				db.Appetizers.Remove(appetizer);
				db.SaveChanges();
				return op.ToSuccess("Remove Appetizer Success Fully");
			}
			catch (Exception ex)
			{
				return op.ToFail("Remove Appetizer Failed");
			}
		}

		public void RemoveImage(int appetizerID)
		{
			var appetizer = db.Appetizers.FirstOrDefault(x => x.AppetizerID == appetizerID);
			if (appetizer != null && appetizer.ImageURL != string.Empty && appetizer.ImageURL.ToLower() != "~/images/noimage.png")
			{
				appetizer.ImageURL = "~/images/noimage.png";
				db.SaveChanges();
			}
		}

		public List<AppetizerListItem> Search(AppetizerSearchModel searchModel, out int RecordCount)
		{
			if (searchModel.PageSize == 0)
			{
				searchModel.PageSize = 5;
			}
			var q = from appetizer in db.Appetizers select appetizer;
			if (searchModel.CategoryID != null && searchModel.CategoryID > 0)
			{
				q = q.Where(x => x.CategoryID == searchModel.CategoryID);
			}
			if (!string.IsNullOrEmpty(searchModel.AppetizerName))
			{
				q = q.Where(x => x.AppetizerName == searchModel.AppetizerName);
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
			q = q.OrderByDescending(x => x.AppetizerID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);
			var q2 = from appetizer in q
					 select new AppetizerListItem
					 {
						 AppetizerName = appetizer.AppetizerName,
						 AppetizerID = appetizer.AppetizerID,
						 CategoryName = appetizer.category.CategoryName,
						 Image = appetizer.ImageURL,
						 UnitPrice = appetizer.UnitPrice,
					 };
			return q2.ToList();
		}

		public OperationResult Update(Appetizer Current)
		{
			OperationResult op = new OperationResult("Update Appetizer ");
			var appetizer = db.Appetizers.FirstOrDefault(x => x.AppetizerID == Current.AppetizerID);
			try
			{
				appetizer.AppetizerID = Current.AppetizerID;
				appetizer.AppetizerName = Current.AppetizerName;
				appetizer.UnitPrice = Current.UnitPrice;
				appetizer.CategoryID = Current.CategoryID;
				appetizer.SmallDescription = Current.SmallDescription;
				appetizer.ImageURL = Current.ImageURL;
				db.SaveChanges();
				return op.ToSuccess("Update Appetizer Success Fully");
			}
			catch
			{
				return op.ToFail("Update Appetizer Failed");
			}
		}
	}
}
