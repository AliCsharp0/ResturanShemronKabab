using Azure.Core;
using FrameWork.DTOS;
using Microsoft.IdentityModel.Tokens;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Customer;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RestaurantShemronKababContext db;

        public CustomerRepository(RestaurantShemronKababContext db)
        {
            this.db = db;
        }

        public bool ExistCategoryName(string FirstName, string LastName)
        {
            return db.Customers.Any(x => x.FirstName == FirstName && x.LastName == LastName);
        }

        public bool ExistUserName(string UserName)
        {
            return db.Customers.Any(x=>x.UserName == UserName);
        }

        public bool ExistMobileNumber(string Mobile)
        {
            return db.Customers.Any(x => x.Mobile == Mobile);
        }

        public Customer Get(int ID)
        {
            return db.Customers.FirstOrDefault(x => x.CustomerID == ID);
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public List<CustomerListItem> GetAllListItem()
        {
            var custom = db.Customers.Select(x => new CustomerListItem
            {
                Address = x.Address,
                Age = x.Age,
                CustomerID = x.CustomerID,
                FirstName = x.FirstName,
                IsActive = x.IsActive,
                LastName = x.LastName,
                Mobile = x.Mobile,
                TelHome = x.TelHome,
                Password = x.Password,
                UserName = x.UserName,
            }).ToList();
            return custom;
        }


        public OperationResult Register(Customer Current)
        {
            OperationResult op = new OperationResult("Register Customer ");
            try
            {
                db.Customers.Add(Current);
                db.SaveChanges();
                return op.ToSuccess("Registration Customer Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Registration Customer Failed");
            }
        }

        public OperationResult Remove(int ID)
        {
            OperationResult op = new OperationResult("Remove Customer");
            try
            {
                var cus = Get(ID);
                db.Customers.Remove(cus);
                db.SaveChanges();
                return op.ToSuccess("Remove Customer Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Remove Customer Failed");
            }
        }

        public List<CustomerListItem> Search(CustomerSearchModel searchModel, out int RecordCount)
        {
            if (searchModel.PageSize == 0)
            {
                searchModel.PageSize = 5;
            }
            var q = from cus in db.Customers select cus;
            if (!string.IsNullOrEmpty(searchModel.FirstName))
            {
                q = q.Where(x => x.FirstName == searchModel.FirstName);
            }
            if (!string.IsNullOrEmpty(searchModel.LastName))
            {
                q = q.Where(x => x.LastName == searchModel.LastName);
            }
            if (searchModel.CustomerID != null)
            {
                q = q.Where(x => x.CustomerID == searchModel.CustomerID);
            }
            if (searchModel.Age != null)
            {
                q = q.Where(x => x.Age == searchModel.Age);
            }
            RecordCount = q.Count();
            q = q.OrderByDescending(x => x.CustomerID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);

            var q2 = from cus in q
                     select new CustomerListItem
                     {
                         Address = cus.Address,
                         Age = cus.Age,
                         IsActive = cus.IsActive,
                         FirstName = cus.FirstName,
                         LastName = cus.LastName,
                         CustomerID = cus.CustomerID,
                         Mobile = cus.Mobile,
                         TelHome = cus.TelHome,
                     };
            return q2.ToList();
        }

        public OperationResult Update(Customer Current)
        {
            OperationResult op = new OperationResult("Update Customer");
            try
            {
                db.Customers.Attach(Current);
                db.Entry<Customer>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return op.ToSuccess("Update Customer Success Fully");
            }
            catch(Exception ex)
            {
                return op.ToFail("Update Customer Failed" + ex.Message);
            }
        }
    }
}
