using FrameWork.DTOS;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DataAccessServiceContract.Repositories;
using Restaurant.DomainModel.ApplicationModel.Employee;
using Restaurant.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class EmployeeApplication : IEmployeeApplication
    {
        private readonly IEmployeeRepository EmpRepo;

        public EmployeeApplication(IEmployeeRepository EmpRepo)
        {
            this.EmpRepo = EmpRepo;
        }

        private Employee ToModel(EmployeeAddAndEditModel empAddEdit)
        {
            Employee emp = new Employee
            {
                EmployeeID = empAddEdit.EmployeeID,
                Address = empAddEdit.Address,
                Age = empAddEdit.Age,
                DateRecruitment = DateTime.Now,
                FirstName = empAddEdit.FirstName,
                LastName = empAddEdit.LastName,
                Mobile = empAddEdit.Mobile,
                Rights = empAddEdit.Rights,
                TelHome = empAddEdit.TelHome,
                Password = empAddEdit.Password,
                UserName = empAddEdit.UserName,
            };
            return emp;
        }
        private EmployeeAddAndEditModel ToAddEditModel(Employee employee)
        {
            EmployeeAddAndEditModel empAddEdit = new EmployeeAddAndEditModel
            {
                Address = employee.Address,
                EmployeeID = employee.EmployeeID,
                Age = employee.Age,
                DateRecruitment = DateTime.Now,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Mobile = employee.Mobile,
                Rights = employee.Rights,
                TelHome = employee.TelHome,
                Password = employee.Password,
                UserName = employee.UserName,
            };
            return empAddEdit;
        }

        public EmployeeAddAndEditModel Get(int EmployeeID)
        {
            return ToAddEditModel(EmpRepo.Get(EmployeeID));
        }

        public OperationResult Register(EmployeeAddAndEditModel employee)
        {
            if (EmpRepo.ExistMobileNumber(employee.Mobile))
            {
                return new OperationResult("Register Employee").ToFail("Duplicate Employee Mobile");
            }
            if (EmpRepo.ExistEmployeeName(employee.FirstName, employee.LastName))
            {
                return new OperationResult("Register Employee").ToFail("Duplicate Employee Name");
            }
            if (EmpRepo.ExistUserName(employee.UserName))
            {
                return new OperationResult("Register employee").ToFail("Duplicate employee User Name");
            }
            Employee emp = ToModel(employee);
            var OperationEmployee = EmpRepo.Register(emp);
            return OperationEmployee;
        }

        public OperationResult Remove(int EmployeeID)
        {
            return EmpRepo.Remove(EmployeeID);
        }

        public List<EmployeeListItem> Search(EmployeeSearchModel searchModel, out int RecordCount)
        {
            return EmpRepo.Search(searchModel, out RecordCount);
        }

        public OperationResult Update(EmployeeAddAndEditModel employee)
        {
            if (EmpRepo.ExistMobileNumberInUpdate(employee.EmployeeID, employee.Mobile))
            {
                return new OperationResult("Update Customer ").ToFail("Duplicate Mobile");
            }
            if (EmpRepo.ExistNameInUpdate(employee.EmployeeID, employee.FirstName, employee.LastName))
            {
                return new OperationResult("Update Customer").ToFail("Duplicate Name");
            }
            if (EmpRepo.ExistUserNameInUpdate(employee.EmployeeID, employee.UserName))
            {
                return new OperationResult("Update User Name").ToFail("Duplicate User Name");
            }
            Employee emp = ToModel(employee);
            return EmpRepo.Update(emp);
        }

        public List<EmployeeListItem> GetAllListItem()
        {
            return EmpRepo.GetAllListItem();
        }

		public bool Login(string userName, string password)
		{
			if(EmpRepo.ExistLogin(userName, password))
            {
                return true;
            }
            return false;
		}

    }
}
