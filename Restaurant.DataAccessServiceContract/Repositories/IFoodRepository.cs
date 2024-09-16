using FrameWork.BaseRepository;
using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
    public interface IFoodRepository : IBaseRepositorySearchable<Food , int , FoodSearchModel , FoodListItem>
    {
        bool ExistFoodName(string FoodName);

        bool ExistImage(string Image);

        bool HasRelatedOrders(int FoodID);//دارای سفارش مرتبط

		List<FoodListItemUI> GetAllListItemInUI();

		bool ExistNameInUpdate(int ID, string foodName);

        bool ExistImageInUpdate(int ID, string Image);

        //void RemoveImage (int foodID);  

        void RemoveImage (int foodID);
    }
}
