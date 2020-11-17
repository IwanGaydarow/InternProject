namespace HCMS.Web.ViewModels.Administration.Departments
{
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.GlobalConstants;

    public class CreateDepartmentViewModel
    {
        [Required]
        [StringLength(80, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 3)]
        public string Tittle { get; set; }
    }
}
