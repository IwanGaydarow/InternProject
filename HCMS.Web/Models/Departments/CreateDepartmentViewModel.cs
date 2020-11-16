namespace HCMS.Web.Models.Department
{
    using System.ComponentModel.DataAnnotations;
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Mapping;

    public class CreateDepartmentViewModel
    {
        [Required]
        [StringLength(80, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 3)]
        public string Tittle { get; set; }
    }
}
