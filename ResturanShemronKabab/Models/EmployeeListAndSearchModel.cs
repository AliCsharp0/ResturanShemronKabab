using Restaurant.DomainModel.ApplicationModel.Employee;

namespace ResturanShemronKabab.Models
{
    public class EmployeeListAndSearchModel
    {
        public EmployeeSearchModel sm { get; set; }

        public List<EmployeeListItem> EmployeeListItems { get; set; }
    }
}
