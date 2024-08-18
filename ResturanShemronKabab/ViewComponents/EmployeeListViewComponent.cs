using Microsoft.AspNetCore.Mvc;
using Restaurant.ApplicationServiceContract.Services;
using Restaurant.DomainModel.ApplicationModel.Employee;

namespace ResturanShemronKabab.ViewComponents
{
    [ViewComponent(Name = "EmployeeList")]
    public class EmployeeListViewComponent : ViewComponent
    {
        private readonly IEmployeeApplication EmployeeApp;

        public EmployeeListViewComponent(IEmployeeApplication EmployeeApp)
        {
            this.EmployeeApp = EmployeeApp;
        }

        public IViewComponentResult Invoke(EmployeeSearchModel sm)
        {
            int rc = 0;
            var Employees = EmployeeApp.Search(sm, out rc);
            Models.EmployeeListAndSearchModel esm = new Models.EmployeeListAndSearchModel { sm = sm , EmployeeListItems = Employees };
            if (sm.PageSize == 0)
            {
                sm.PageSize = 5;
            }
            if (sm.RecordCount % sm.PageSize == 0)
            {
                sm.PageCount = sm.RecordCount / sm.PageSize;
            }
            else
            {
                sm.PageCount = sm.RecordCount / sm.PageSize + 1;

            }
            return View(esm);
        }
    }
}
