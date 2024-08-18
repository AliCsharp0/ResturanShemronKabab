using FrameWork.BaseRepository;
using Restaurant.DomainModel.ApplicationModel.Appetizer;
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
    }
}
