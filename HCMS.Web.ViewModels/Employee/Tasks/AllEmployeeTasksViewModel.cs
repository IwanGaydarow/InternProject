namespace HCMS.Web.ViewModels.Employee
{
    using System.Collections.Generic;
 
    public class AllEmployeeTasksViewModel
    {
        public IEnumerable<EmployeeTasksViewModel> Tasks { get; set; }
    }
}
