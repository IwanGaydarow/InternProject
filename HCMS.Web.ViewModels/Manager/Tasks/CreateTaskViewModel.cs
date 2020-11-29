namespace HCMS.Web.ViewModels.Manager.Tasks
{
    using HCMS.GlobalConstants;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateTaskViewModel
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string Tittle { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = GlobalConstant.NumberRangeErrorMessage)]
        public int EstWorkHours { get; set; }

        [Required]
        public bool Status => false;

        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}
