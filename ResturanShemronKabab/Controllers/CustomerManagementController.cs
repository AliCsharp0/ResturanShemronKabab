using Microsoft.AspNetCore.Mvc;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Customer;

namespace ResturanShemronKabab.Controllers
{
    public class CustomerManagementController : Controller
    {
        private readonly ICustomerApplication CustomerApplication;

        public CustomerManagementController(ICustomerApplication CustomerApplication)
        {
            this.CustomerApplication = CustomerApplication;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var custom = CustomerApplication.GetAllListItem();
            return View(custom);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            var op  =  CustomerApplication.Remove(id);
            return Json(op);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add(CustomerAddAndEditModel model)
        {
            var op = CustomerApplication.Register(model);
            return Json(op);
        }

        public IActionResult Update(int id)
        {
            var custom = CustomerApplication.Get(id);
            return View(custom);
        }

        [HttpPost]
        public JsonResult Update(CustomerAddAndEditModel model)
        {
            var op = CustomerApplication.Update(model);
            return Json(op);
        }
    }
}
