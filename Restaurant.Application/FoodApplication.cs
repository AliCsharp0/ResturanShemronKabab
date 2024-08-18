using FrameWork.DTOS;
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
                Image = FoodAddEdit.Image,
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
                Image = food.Image,
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
            if (FoodRepo.ExistMaterials(food.Materials))
            {
                return new OperationResult("Register Food").ToFail("Duplicate Food Materials");
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
            Food f = ToModel(food);
            return FoodRepo.Update(f);
        }

        public List<FoodListItem> GetAllListItem()
        {
            return FoodRepo.GetAllListItem();
        }
	}
}
