using FrameWork.DTOS;
using Microsoft.AspNetCore.Hosting;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ICategoryRepository CatRepo;

        public CategoryApplication(ICategoryRepository CatRepo)
        {
            this.CatRepo = CatRepo;
        }

        private Category ToModel(CategoryAddAndEditModel CatAddEdit)
        {
            Category category = new Category
            {
                CategoryID = CatAddEdit.CategoryID,
                SmallDescription = CatAddEdit.SmallDescription,
                CategoryName = CatAddEdit.CategoryName,
                Slug = CatAddEdit.Slug,
                Parent=CatAddEdit.Parent,
                ParentID = CatAddEdit.ParentID,
                FoodCountInCategory = 0,
                BeverageCountInCategory= 0,
                AppetizerCountInCategory =0,
            };
            return category;
        }
        private CategoryAddAndEditModel ToAddEditModel(Category category)
        {
            CategoryAddAndEditModel categoryAddAndEditModel = new CategoryAddAndEditModel
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                SmallDescription = category.SmallDescription,
                Slug = category.Slug,
                Parent = category.Parent,
                ParentID = category.ParentID,
            };
            return categoryAddAndEditModel;
        }

        public CategoryAddAndEditModel Get(int CategoryID)
        {
            return ToAddEditModel(CatRepo.Get(CategoryID));
        }

		public List<CategoryListForDrp> GetDrp()
        {
            return CatRepo.GetDrp();
		}


		public OperationResult Register(CategoryAddAndEditModel category)
        {
            if (CatRepo.ExistCategoryName(category.CategoryName))
            {
                return new OperationResult("Register Category").ToFail("Duplicate Category Name");
            }
            Category Cat = ToModel(category);
            var OperationCategory = CatRepo.Register(Cat);
            return OperationCategory;
        }

        public OperationResult Remove(int CategoryId)
        {
            return CatRepo.Remove(CategoryId);
        }

        public List<CategoryListItem> Search(CategorySearchModel searchModel, out int RecordCount)
        {
            return CatRepo.Search(searchModel, out RecordCount);
        }

        public OperationResult Update(CategoryAddAndEditModel category)
        {
            Category Cat = ToModel(category);

            return CatRepo.Update(Cat);
        }

        public List<Category> GetAll()
        {
            return CatRepo.GetAll();
        }

		public List<CategoryListItem> GetRoots()
		{
			return CatRepo.GetRoots();
		}

        public List<CategoryListItem> GetAllListItem()
        {
            return CatRepo.GetAllListItem();
        }



        //public List<CategoryListItem> GetAllView()
        //{
        //	return CatRepo.GetAllView();
        //}

        //public async Task<List<CategoryListItem>> GetSubCategory(int ParentID)
        //{
        //	return await CatRepo.GetSubCategory(ParentID);
        //}
    }
}
