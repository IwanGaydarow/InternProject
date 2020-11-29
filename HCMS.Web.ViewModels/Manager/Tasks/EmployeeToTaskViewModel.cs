namespace HCMS.Web.ViewModels.Manager.Tasks
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.Web.ViewModels.Administration.Evaluation;

    public class EmployeeToTaskViewModel
    {
        [Required]
        public int TaskId { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        public virtual IEnumerable<EmployeeSelectList> Employees { get; set; }
    }
}
