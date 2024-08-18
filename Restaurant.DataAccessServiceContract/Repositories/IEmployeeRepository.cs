using FrameWork.BaseRepository;
using Restaurant.DomainModel.ApplicationModel.Employee;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccessServiceContract.Repositories
{
    public interface IEmployeeRepository : IBaseRepositorySearchable<Employee , int , EmployeeSearchModel , EmployeeListItem>
    {
        bool ExistEmployeeName(string FirstName , string LastName);

        bool ExistMobileNumber(string Mobile);

        bool DateRecruitmentNowBiggerThanNow(DateTime DateRecruitment);

        bool ExistUserName(string UserName);

    }
}
