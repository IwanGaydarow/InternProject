namespace HCMS.Web.ViewModels.Administration.Departments
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.Web.ViewModels.Administration.Evaluation;

    public class ManagerToDepartmentViewModel
    {
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<EmployeeSelectList> Employees { get; set; }
    }
}
