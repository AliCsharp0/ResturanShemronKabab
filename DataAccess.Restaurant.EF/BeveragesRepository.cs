using FrameWork.DTOS;
using Restaurant.DataAccessServiceContract.Repositories;
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
            return db.Beverages.Any(x => x.Image == Image);
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
                Image = x.Image,
                UnitPrice = x.UnitPrice,
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
            if (searchModel.BeveragesID != null)
            {
                q = q.Where(x => x.BeveragesID == searchModel.BeveragesID);
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
                         Image = beverages.Image,
                         UnitPrice = beverages.UnitPrice,
                     };
            return q2.ToList();
        }

        public OperationResult Update(Beverages Current)
        {
            OperationResult op = new OperationResult("Update Beverages");
            try
            {
                db.Beverages.Attach(Current);
                db.Entry<Beverages>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return op.ToSuccess("Update Beverages Success Fully ");
            }
            catch (Exception ex)
            {
                return op.ToFail("Update Beverages Failed");
            }
        }
    }
}
