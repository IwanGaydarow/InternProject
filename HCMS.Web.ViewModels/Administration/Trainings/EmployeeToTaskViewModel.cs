namespace HCMS.Web.ViewModels.Administration.Trainings
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.Web.ViewModels.Administration.Evaluation;

    public class EmployeeToTaskViewModel
    {
        [Required]
        public int TrainingId { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        public virtual IEnumerable<EmployeeSelectList> Employees { get; set; }
    }
}
