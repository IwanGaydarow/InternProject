namespace HCMS.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.GlobalConstants;

    public class EmailFormViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = GlobalConstant.NotValidEmail)]
        public string From { get; set; }

        public string To { get; set; }

        [Required(ErrorMessage = "Please enter subject.")]
        [StringLength(80, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength =5)]
        public string Subject { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = GlobalConstant.MinStringValidation)]
        public string Content { get; set; }

        public string ReturnUrl { get; set; }
    }
}
