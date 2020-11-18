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
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = GlobalConstant.MinPasswordLenght)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = GlobalConstant.ConfirmPasswordNotMatch)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string Address { get; set; }

        [Required]
        public CreateCityViewModel City { get; set; }

        [Required]
        [Display(Name = "Country")]
        [StringLength(50, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string CountryName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 4)]
        public string Gender { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string JobTittle { get; set; }
    }
}
