using FrameWork.DTOS;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Customer;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerRepository CusRepo;

        public CustomerApplication(ICustomerRepository CusRepo)
        {
            this.CusRepo = CusRepo;
        }

        private Customer ToModel(CustomerAddAndEditModel CusAddEdit)
        {
            Customer Cus = new Customer
            {
                FirstName = CusAddEdit.FirstName,
                LastName = CusAddEdit.LastName,
                Age = CusAddEdit.Age,
                Address = CusAddEdit.Address,
                CustomerID = CusAddEdit.CustomerID,
                Mobile = CusAddEdit.Mobile,
                TelHome = CusAddEdit.TelHome,
                Password = CusAddEdit.Password,
                UserName = CusAddEdit.UserName,
            };
            return Cus;
        }

        private CustomerAddAndEditModel ToAddEditModel(Customer customer)
        {
            CustomerAddAndEditModel CusAddEdit = new CustomerAddAndEditModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Age = customer.Age,
                CustomerID = customer.CustomerID,
                Mobile = customer.Mobile,
                TelHome = customer.TelHome,
                Password = customer.Password,
                UserName = customer.UserName,
            };
            return CusAddEdit;
        }

        public CustomerAddAndEditModel Get(int CustomerID)
        {
            return ToAddEditModel(CusRepo.Get(CustomerID));
        }

        public OperationResult Register(CustomerAddAndEditModel customer)
        {
            if (CusRepo.ExistMobileNumber(customer.Mobile))
            {
                return new OperationResult("Register Customer ").ToFail("Duplicate Customer Mobile");
            }
            if(CusRepo.ExistCategoryName(customer.FirstName , customer.LastName))
            {
                return new OperationResult("Register Customer").ToFail("Duplicate customer Name");
            }
            if(CusRepo.ExistUserName(customer.UserName))
            {
                return new OperationResult("Register Customer").ToFail("Duplicate Customer User Name");
            }
            Customer cus = ToModel(customer);
            var OperationCustomer = CusRepo.Register(cus);
            return OperationCustomer;
        }

        public OperationResult Remove(int CustomerID)
        {
            return CusRepo.Remove(CustomerID);
        }

        public List<CustomerListItem> Search(CustomerSearchModel searchModel, out int RecordCount)
        {
            return CusRepo.Search(searchModel, out RecordCount);
        }

        public OperationResult Update(CustomerAddAndEditModel customer)
        {
            var Custom = ToModel(customer);
            return CusRepo.Update(Custom);
        }

        public List<CustomerListItem> GetAllListItem()
        {
            return CusRepo.GetAllListItem();
        }
    }
}
