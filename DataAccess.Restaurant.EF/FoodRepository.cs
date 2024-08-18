using FrameWork.DTOS;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
    public class FoodRepository : IFoodRepository
    {
        private readonly RestaurantShemronKababContext db;

        public FoodRepository(RestaurantShemronKababContext db)
        {
            this.db= db;
        }

        public bool ExistFoodName(string FoodName)
        {
            return db.Foods.Any(x => x.FoodName == FoodName);
        }

        public bool ExistImage(string Image)
        {
            return db.Foods.Any(x => x.Image == Image);
        }

        public bool ExistMaterials(string MaterialName)
        {
            return db.Foods.Any(x=>x.Materials == MaterialName);    
        }
    
        public Food Get(int ID)
        {
            return db.Foods.FirstOrDefault(x => x.FoodID == ID);
        }

        public List<Food> GetAll()
        {
            return db.Foods.ToList();
        }

        public List<FoodListItem> GetAllListItem()
        {
            var food = db.Foods.Select(x => new FoodListItem
            {
                FoodID = x.FoodID,
                FoodName = x.FoodName,
                CategoryName = x.category.CategoryName,
                HasRelatedOrder = x.orderDetails.Count > 0,
                Image = x.Image,
                Materials=x.Materials,
                UnitPrice=x.UnitPrice,
            }).ToList();
            return food;
        }

	

		public bool HasRelatedOrders(int FoodID)
        {
            return Get(FoodID).orderDetails.Any();
        }

        public OperationResult Register(Food Current)
        {
            OperationResult op = new OperationResult("Register Food");
            try
            {
                db.Foods.Add(Current);
                db.SaveChanges();
                return op.ToSuccess("Food Registration Success Fully");
            }
            catch(Exception ex)
            {
                return op.ToFail("Food Registration Failed" + ex.Message);
            }
        }

        public OperationResult Remove(int ID)
        {
            OperationResult op = new OperationResult("Remove Food");
            try
            {
                var food = db.Foods.FirstOrDefault(x=>x.FoodID ==ID);
                db.Foods.Remove(food);
                db.SaveChanges();
                return op.ToSuccess("Remove Food Success Fully" , ID);
            }
            catch(Exception ex)
            {
                return op.ToFail("Remove Food Failed" + ex.Message);
            }
        }

        public List<FoodListItem> Search(FoodSearchModel searchModel, out int RecordCount)
        {
            if(searchModel.PageSize == 0)
            {
                searchModel.PageSize = 5;
            }

            var q = from food in db.Foods select food;

            if(searchModel.FoodID != null)
            {
                q = q.Where(x => x.FoodID == searchModel.FoodID);
            }

            if(!string.IsNullOrEmpty(searchModel.FoodName))
            {
                q = q.Where(x => x.FoodName.StartsWith(searchModel.FoodName));
            }

            if(searchModel.UnitPriceFrom != null)
            {
                q = q.Where(x => x.UnitPrice >= searchModel.UnitPriceFrom);
            }

            if(searchModel.UnitPriceTo != null)
            {
                q = q.Where(x=>x.UnitPrice <=searchModel.UnitPriceTo);
            }

            RecordCount = q.Count();

            q = q.OrderByDescending(x => x.FoodID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);

            var q2 = from food in q select new FoodListItem
            {
                HasRelatedOrder = food.orderDetails.Any(),
                CategoryName = food.category.CategoryName,
                FoodID  = food.FoodID,
                FoodName = food.FoodName,
                Image = food.Image,
                Materials = food.Materials,
                UnitPrice = food.UnitPrice,
            };
            return q2.ToList();
        }

        public OperationResult Update(Food Current)
        {
            OperationResult op = new OperationResult("Update Food");
            try
            {
                db.Foods.Attach(Current);
                db.Entry<Food>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return op.ToSuccess("Update Food Success Fully");
            }
            catch(Exception ex)
            {
                return op.ToFail("Update Food Failed");
            }
        }
    }
}
