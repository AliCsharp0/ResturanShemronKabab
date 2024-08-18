using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Employee;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Restaurant.EF
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantShemronKababContext db;

        public EmployeeRepository(RestaurantShemronKababContext db)
        {
            this.db = db;
        }

        public bool DateRecruitmentNowBiggerThanNow(DateTime DateRecruitment)
        {
            if (DateRecruitment > DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public bool ExistEmployeeName(string FirstName, string LastName)
        {
            return db.Employees.Any(x => x.FirstName == FirstName && x.LastName == LastName);
        }

        public bool ExistMobileNumber(string Mobile)
        {
            return db.Employees.Any(x => x.Mobile == Mobile);
        }

        public bool ExistUserName(string UserName)
        {
            return db.Employees.Any(x => x.UserName == UserName);
        }

        public Employee Get(int ID)
        {
            return db.Employees.FirstOrDefault(x => x.EmployeeID == ID);
        }

        public List<Employee> GetAll()
        {
            return db.Employees.ToList();
        }

		public List<EmployeeListItem> GetAllListItem()
		{
			var emp =  db.Employees.Select(x => new EmployeeListItem
			{
				Address = x.Address,
                Age = x.Age,
                DateRecruitment = x.DateRecruitment,
                EmployeeID = x.EmployeeID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Mobile = x.Mobile,
                Rights = x.Rights,
                TelHome = x.TelHome,
                Password = x.Password,
                UserName = x.UserName,
			}).ToList();
			return emp;
		}

		public OperationResult Register(Employee Current)
        {
            OperationResult op = new OperationResult("Register Employee");
            try
            {
                db.Employees.Add(Current);
                db.SaveChanges();
                return op.ToSuccess("Registration Employee Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Registration Employee Failed" + ex.Message);
            }
        }

        public OperationResult Remove(int ID)
        {
            OperationResult op = new OperationResult("Remove Employee");
            try
            {
                var emp = Get(ID);
                db.Employees.Remove(emp);
                db.SaveChanges();
                return op.ToSuccess("Remove Employee Success Fully");
            }
            catch (Exception ex)
            {
                return op.ToFail("Remove employee Failed" + ex.Message);
            }
        }

        public List<EmployeeListItem> Search(EmployeeSearchModel searchModel, out int RecordCount)
        {
            if (searchModel.PageSize == 0)
            {
                searchModel.PageSize = 5;
            }
            var q = from emp in db.Employees select emp;
            if (!string.IsNullOrEmpty(searchModel.FirstName))
            {
                q = q.Where(x => x.FirstName == searchModel.FirstName);
            }
            if (!string.IsNullOrEmpty(searchModel.LastName))
            {
                q = q.Where(x => x.LastName == searchModel.LastName);
            }
            if (searchModel.RightsFrom != null)
            {
                q = q.Where(x => x.Rights >= searchModel.RightsFrom);
            }
            if (searchModel.RightsTo != null)
            {
                q = q.Where(x => x.Rights <= searchModel.RightsTo);
            }
            if (searchModel.Age != null)
            {
                q = q.Where(x => x.Age == searchModel.Age);
            }
            RecordCount = q.Count();
            q = q.OrderByDescending(x => x.EmployeeID).Skip(searchModel.PageIndex * searchModel.PageSize).Take(searchModel.PageSize);

            var q2 = from emp in q
                     select new EmployeeListItem
                     {
                         EmployeeID = emp.EmployeeID,
                         Address = emp.Address,
                         DateRecruitment = emp.DateRecruitment,
                         Age = emp.Age,
                         FirstName = emp.FirstName,
                         LastName = emp.LastName,
                         Mobile = emp.Mobile,
                         Rights = emp.Rights,
                         TelHome = emp.TelHome,
                     };
            return q2.ToList();
        }

        public OperationResult Update(Employee Current)
        {

            OperationResult op = new OperationResult("Update Employee");
            try
            {
                db.Employees.Attach(Current);
                db.Entry<Employee>(Current).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return op.ToSuccess("Saved");
            }
            catch(Exception ex)
            {
                return op.ToFail("Update Employee Failed" + ex.Message);
            }
        }

	}
}
