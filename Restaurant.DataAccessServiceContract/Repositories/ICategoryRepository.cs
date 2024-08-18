using FrameWork.BaseRepository;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Employee;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
    public interface ICategoryRepository : IBaseRepositorySearchable<Category , int , CategorySearchModel , CategoryListItem>
    {
        bool ExistCategoryName(string CategoryName);

        bool ExistCategoryName(string CategoryName, int CategoryID);

        Task<bool> HasChild(int ID);

		Task<bool> HasFood(int ID);

		Task<bool> HasAppetizer(int ID);

		Task<bool> HasBeverages(int ID);

		Task<List<CategoryListItem>> GetSubCategory(int ParentID);

		List<CategoryListItem> GetRoots();

        List<CategoryListForDrp> GetDrp();

        List<CategoryListItem> GetAllListItem();

        //public List<CategoryListItem> GetAllView();
    }
}