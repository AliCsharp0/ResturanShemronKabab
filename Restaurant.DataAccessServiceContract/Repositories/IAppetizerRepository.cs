using FrameWork.BaseRepository;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
using Restaurant.DomainModel.ApplicationModel.Food;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
    public interface IAppetizerRepository:IBaseRepositorySearchable<Appetizer , int , AppetizerSearchModel , AppetizerListItem>
    {
        bool ExistAppetizerName(string AppetizerName);

        bool ExistImage(string Image);

        bool HasRelatedOrders(int AppetizerID);//دارای سفارش مرتبط

		List<AppetizerListItemUI> GetAllListItemInUI();

        bool ExistNameInUpdate(int ID ,  string Name);

        bool ExistImageInUpdate(int ID , string Image);

        void RemoveImage(int appetizerID);
	}
}
