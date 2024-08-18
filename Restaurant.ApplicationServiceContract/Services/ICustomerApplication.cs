using FrameWork.DTOS;
using Restaurant.DomainModel.ApplicationModel.Category;
using Restaurant.DomainModel.ApplicationModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ApplicationServiceContract.Services
{
    public interface ICustomerApplication
    {
        CustomerAddAndEditModel Get(int CustomerID);

        OperationResult Register(CustomerAddAndEditModel customer);

        OperationResult Update(CustomerAddAndEditModel customer);

        OperationResult Remove(int CustomerID);

        List<CustomerListItem> Search(CustomerSearchModel searchModel, out int RecordCount);

        List<CustomerListItem> GetAllListItem();

	}
}
