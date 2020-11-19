using System.Collections.Generic;

namespace HCMS.Web.ViewModels.Administration.Employees
{
    public class AllEmployeesViewModel
    {
        public IEnumerable<EmployeesViewModel> Employees { get; set; }
    }
}
