using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
    public interface IEmployeeApplication
    {
        EmployeeAddAndEditModel Get(int EmployeeID);

        OperationResult Register(EmployeeAddAndEditModel employee);

        OperationResult Update(EmployeeAddAndEditModel employee);

        OperationResult Remove(int EmployeeID);

        List<EmployeeListItem> Search(EmployeeSearchModel searchModel, out int RecordCount);

        List<EmployeeListItem> GetAllListItem();

	}
}
