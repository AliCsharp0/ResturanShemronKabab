﻿using FrameWork.DTOS;
using Microsoft.AspNetCore.Mvc;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Employee;
using ResturanShemronKabab.Helper;

namespace ResturanShemronKabab.Controllers
{
    public class EmployeeManagementController : Controller
    {
        private readonly IEmployeeApplication employeeApplication;
        private readonly IWebHostEnvironment env;

        public EmployeeManagementController(IEmployeeApplication employeeApplication, IWebHostEnvironment env)
        {
            this.employeeApplication = employeeApplication;
            this.env = env;
        }
        public IActionResult Index(EmployeeSearchModel sm)
        {
            return View(sm);
        }
        public IActionResult List()
        {
            var emp = employeeApplication.GetAllListItem();
            return View(emp);
        }
        [HttpPost]
        public JsonResult Remove(int ID)
        {
            var op = employeeApplication.Remove(ID);
            return Json(op);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(EmployeeAddAndEditModel model)
        {
            var op = employeeApplication.Register(model);
            return Json(op);
        }

        public IActionResult Update(int id)
        {

            var cat = employeeApplication.Get(id);
            return View(cat);
        }
        [HttpPost]
        public JsonResult Update(EmployeeAddAndEditModel emp)
        {
            var op = employeeApplication.Update(emp);
            return Json(op);
        }

        public IActionResult Search(EmployeeSearchModel sm)
        {
            var emp = employeeApplication.Search(sm, out var recordCount);
            return PartialView("List", emp);
        }
    }
}
