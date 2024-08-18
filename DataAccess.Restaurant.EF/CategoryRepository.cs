using Azure.Core;
using FrameWork.DTOS;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Employee;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly RestaurantShemronKababContext db;

        public CategoryRepository(RestaurantShemronKababContext db)
        {
            this.db = db;
        }

        public bool ExistCategoryName(string CategoryName)
        {
            return db.Categories.Any(x => x.CategoryName == CategoryName);
        }

        public bool ExistCategoryName(string CategoryName, int CategoryID)
        {
            return db.Categories.Any(x => x.CategoryName == CategoryName && x.CategoryID == CategoryID);
        }

        public Category Get(int ID)
        {
            return db.Categories.FirstOrDefault(x => x.CategoryID == ID);
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public List<CategoryListItem> GetAllListItem()
        {
            var cat = db.Categories.Select(x => new CategoryListItem
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
               FoodCountInCategory = x.Foods.Count,
               AppetizerCountInCategory = x.appetizers.Count,
                BeverageCountInCategory = x.beverages.Count,
                Slug = x.Slug,
                SmallDescription = x.SmallDescription,
            }).ToList();
            return cat;
        }
        public List<CategoryListItem> GetRoots()
        {
            var cats =
            db.Categories.Where(x => x.ParentID == null).Select(x => new CategoryListItem
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
                Slug = x.Slug,
                FoodCountInCategory      = x.Foods.Count,
                AppetizerCountInCategory = x.appetizers.Count,
                BeverageCountInCategory = x.beverages.Count,
            }).ToList();
            return cats;
        }

        //      public List<CategoryListItem> GetAllView()
        //{
        //	var Cate =  db.Categories.Select(x => new CategoryListItem
        //	{
        //		Slug = x.Slug,
        //		CategoryID = x.CategoryID,
        //		CategoryName = x.CategoryName,
        //              FoodCount =x.Foods.Count,
        //              AppetizerCount = x.appetizers.Count,
        //              BeverageCount = x.beverages.Count,
        //	}).ToList();
        //	return Cate;
        //}

        public List<CategoryListForDrp> GetDrp()
        {
            return db.Categories.Select(cat => new CategoryListForDrp { CategoryID = cat.CategoryID, CategoryName = cat.CategoryName }).ToList();
        }

        public async Task<List<CategoryListItem>> GetSubCategory(int ParentID)
        {
            var Cats = await db.Categories.Where(x => x.ParentID == ParentID).Select(x => new CategoryListItem
            {
                CategoryName = x.CategoryName,
                CategoryID = x.CategoryID,
                AppetizerCountInCategory = x.appetizers.Count(),
                Slug = x.Slug,
                BeverageCountInCategory = x.beverages.Count(),
                FoodCountInCategory = x.Foods.Count(),
            }).ToListAsync();
            return Cats;
        }

        public async Task<bool> HasAppetizer(int ID)
        {
            return await db.Appetizers.AnyAsync(x => x.CategoryID == ID);
        }

        public async Task<bool> HasBeverages(int ID)
        {
            return await db.Beverages.AnyAsync(x => x.CategoryID == ID);
        }

        public async Task<bool> HasChild(int ID)
        {
            return await db.Categories.AnyAsync(x => x.ParentID == ID);
        }

        public async Task<bool> HasFood(int ID)
        {
            return await db.Foods.AnyAsync(x => x.CategoryID == ID);
        }

        //public List<FastCategoryListToDropDown> GetDrp()
        //{
        //    return db.Categories.Select(cat => new FastCategoryListToDropDown { CategoryID = cat.CategoryID, CategoryName = cat.CategoryName }).ToList();

        //}

        public OperationResult Register(Category Current)
        {
            OperationResult op = new OperationResult("Register Category");
            try
            {
                db.Categories.Add(Current);
                db.SaveChanges();
                return op.ToSuccess("Registration Category Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Registration Category Failed" + ex.Message);
            }
        }

        public OperationResult Remove(int ID)
        {
            OperationResult op = new OperationResult("Remove Category");
            try
            {
                var cat = Get(ID);
                db.Categories.Remove(cat);
                db.SaveChanges();
                return op.ToSuccess("Remove Category Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Remove category Failed");
            }
        }

        public List<CategoryListItem> Search(CategorySearchModel searchModel, out int RecordCount)
        {
            if (searchModel.PageSize == 0)
            {
                searchModel.PageSize = 5;
            }
            var q = from cat in db.Categories select cat;
            if (!string.IsNullOrEmpty(searchModel.CategoryName))
            {
                q = q.Where(x => x.CategoryName == searchModel.CategoryName);
            }
            if (searchModel.CategoryID != null)
            {
                q = q.Where(x => x.CategoryID == searchModel.CategoryID);
            }
            RecordCount = q.Count();
            q = q.OrderByDescending(x => x.CategoryID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);

            var q2 = from cat in q
                     select new CategoryListItem
                     {
                         AppetizerCountInCategory = cat.appetizers.Count(),
                         BeverageCountInCategory = cat.beverages.Count(),
                         FoodCountInCategory = cat.Foods.Count(),
                         CategoryID = cat.CategoryID,
                         CategoryName = cat.CategoryName,
                     };
            return q2.ToList();
        }

        public OperationResult Update(Category Current)
        {
            OperationResult op = new OperationResult("Update Category");
            try
            {
                db.Categories.Attach(Current);
                db.Entry<Category>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return op.ToSuccess("Update Category Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Update Category Failed");
            }
        }

    }
}
