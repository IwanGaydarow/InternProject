namespace HCMS.Web.ViewModels.Administration.Employees
{
    using HCMS.GlobalConstants;
    using System.ComponentModel.DataAnnotations;

    public class CreateViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 8)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string Address { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string Country { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 4)]
        public string Gender { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string JobTittle { get; set; }
    }
}
