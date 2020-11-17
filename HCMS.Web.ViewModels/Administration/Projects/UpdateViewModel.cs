namespace HCMS.Web.ViewModels.Administration.Projects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.GlobalConstants;

    public class UpdateViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 4)]
        [Display(Name = "Project Tittle")]
        public string Tittle { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = GlobalConstant.MinStringValidation)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = GlobalConstant.NumberValidation)]
        [Display(Name = "Estimated Working Hours")]
        public int EstimatedWorkHours { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public IEnumerable<AllDepartmentsViewModel> Departments { get; set; }
    }
}
