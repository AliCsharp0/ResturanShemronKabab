using FrameWork.BaseRepository;
using Restaurant.DomainModel.ApplicationModel.Customer;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
    public interface ICustomerRepository : IBaseRepositorySearchable<Customer , int , CustomerSearchModel , CustomerListItem>
    {
        bool ExistCategoryName(string FirstName , string LastName);

        bool ExistMobileNumber(string Mobile);

        bool ExistUserName(string UserName);
    }
}