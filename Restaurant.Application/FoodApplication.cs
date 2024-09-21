using FrameWork.DTOS;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class FoodApplication : IFoodApplication
    {
        private readonly IFoodRepository FoodRepo;

        public FoodApplication(IFoodRepository FoodRepo)
        {
            this.FoodRepo = FoodRepo;
        }

        private Food ToModel(FoodAddAndEditModel FoodAddEdit)
        {
            Food food = new Food
            {
                FoodID = FoodAddEdit.FoodID,
                FoodName = FoodAddEdit.FoodName,
                CategoryID = FoodAddEdit.CategoryID,
                ImageURL = FoodAddEdit.ImageURL,
                Materials = FoodAddEdit.Materials,
                UnitPrice = FoodAddEdit.UnitPrice,
                
            };
            return food;
        }

        private FoodAddAndEditModel ToAddEditModel(Food food)
        {
            FoodAddAndEditModel foodAddEdit = new FoodAddAndEditModel
            {
                FoodID =food.FoodID,
                FoodName = food.FoodName,
                CategoryID = food.CategoryID,
                ImageURL = food.ImageURL,
                Materials = food.Materials,
                UnitPrice = food.UnitPrice,
            };
            return foodAddEdit;
        }

        public FoodAddAndEditModel Get(int FoodID)
        {
            return ToAddEditModel(FoodRepo.Get(FoodID));
        }

        public OperationResult Register(FoodAddAndEditModel food)
        {
            if(FoodRepo.ExistFoodName(food.FoodName))
            {
                return new OperationResult("Register Food").ToFail("Duplicate Food Name");
            }
            if(FoodRepo.ExistImage(food.ImageURL))
            {
				return new OperationResult("Register Food").ToFail("Duplicate Food Image");
			}
			if (food.CategoryID < 0)
			{
				return new OperationResult("Register Food").ToFail("Duplicate Category Name");
			}
            if(string.IsNullOrEmpty(food.ImageURL) || food.ImageURL.ToLower() == @"~/images/noimage.png")
            {
                return new OperationResult("Register Food").ToFail("Please select the food");
            }
			Food f = ToModel(food);
            var OperationFood = FoodRepo.Register(f);
            return OperationFood;
        }

        public OperationResult Remove(int FoodID)
        {
            if(FoodRepo.HasRelatedOrders(FoodID))
            {
                return new OperationResult("Remove Food").ToFail("Food Has Related Orders");
            }
            return FoodRepo.Remove(FoodID);
        }

        public List<FoodListItem> Search(FoodSearchModel sm, out int RecordCount)
        {
            return FoodRepo.Search(sm , out RecordCount);
        }

        public OperationResult Update(FoodAddAndEditModel food)
        {
            if(FoodRepo.ExistNameInUpdate(food.FoodID , food.FoodName))
            {
				return new OperationResult("Update Food").ToFail("Duplicate Food Name");
			}
            if(FoodRepo.ExistImageInUpdate(food.FoodID , food.ImageURL))
            {
				return new OperationResult("Update Food").ToFail("Duplicate Food Image");
			}
			if (food.CategoryID < 0)
			{
				return new OperationResult("Update Food").ToFail("Duplicate Category Name");
			}

			Food f = ToModel(food);
            var operationFood =  FoodRepo.Update(f);
            return operationFood;
        }

        public List<FoodListItem> GetAllListItem()
        {
            return FoodRepo.GetAllListItem();
        }

		public List<FoodListItemUI> GetAllListItemInUI()
		{
            return FoodRepo.GetAllListItemInUI();
        }

        public void RemoveImage(int foodID)
        {
             FoodRepo.RemoveImage(foodID);
        }

    }
}
