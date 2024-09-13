using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
    public interface ICategoryApplication
    {
        CategoryAddAndEditModel Get(int CategoryID);

        OperationResult Register(CategoryAddAndEditModel category);

        OperationResult Update(CategoryAddAndEditModel category);

        OperationResult Remove(int CategoryId);

        List<CategoryListItem> Search(CategorySearchModel searchModel, out int RecordCount);

		List<CategoryListForDrp> GetDrp();

		List<Category> GetAll();

		List<CategoryListItem> GetRoots();

         List<CategoryListItem> GetAllListItem();



        //public List<CategoryListItem> GetAllView();
        /*Task<List<CategoryListItem>> GetSubCategory(int ParentID)*/
        //List<FastCategoryListToDropDown> GetDrp();
    }
}
