using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
    public interface IFoodApplication
    {
        FoodAddAndEditModel Get(int FoodID);

        OperationResult Remove(int FoodID);

        OperationResult Register(FoodAddAndEditModel food);

        OperationResult Update(FoodAddAndEditModel food);

        List<FoodListItem> Search(FoodSearchModel sm, out int RecordCount);

         List<FoodListItem> GetAllListItem();


	}
}
