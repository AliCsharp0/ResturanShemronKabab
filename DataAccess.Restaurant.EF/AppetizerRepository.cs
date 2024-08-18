using FrameWork.DTOS;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Employee;
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
			return db.Appetizers.Any(x => x.Image == Image);
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
				Image = x.Image,
				UnitPrice = x.UnitPrice,
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

		public List<AppetizerListItem> Search(AppetizerSearchModel searchModel, out int RecordCount)
		{
			if (searchModel.PageSize == 0)
			{
				searchModel.PageSize = 5;
			}
			var q = from appetizer in db.Appetizers select appetizer;
			if (searchModel.UnitPriceFrom != null || searchModel.UnitPriceFrom > 0)
			{
				q = q.Where(x => x.UnitPrice >= searchModel.UnitPriceFrom);
			}
			if (searchModel.UnitPriceTo != null || searchModel.UnitPriceTo > 0)
			{
				q = q.Where(x => x.UnitPrice <= searchModel.UnitPriceTo);
			}
			if (searchModel.AppetizerID != null)
			{
				q = q.Where(x => x.AppetizerID == searchModel.AppetizerID);
			}
			if (!string.IsNullOrEmpty(searchModel.AppetizerName))
			{
				q = q.Where(x => x.AppetizerName == searchModel.AppetizerName);
			}
			RecordCount = q.Count();
			q = q.OrderByDescending(x => x.AppetizerID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);
			var q2 = from appetizer in q
					 select new AppetizerListItem
					 {
						 AppetizerName = appetizer.AppetizerName,
						 AppetizerID = appetizer.AppetizerID,
						 CategoryName = appetizer.category.CategoryName,
						 Image = appetizer.Image,
						 UnitPrice = appetizer.UnitPrice,
					 };
			return q2.ToList();
		}

		public OperationResult Update(Appetizer Current)
		{
			OperationResult op = new OperationResult("Update Appetizer");
			try
			{
				db.Appetizers.Attach(Current);
				db.Entry<Appetizer>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				db.SaveChanges();
				return op.ToSuccess("Update Appetizer Success Fully");
			}
			catch (Exception ex)
			{
				return op.ToFail("Update Appetizer Failed");
			}
		}
	}
}
