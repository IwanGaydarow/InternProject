using HCMS.GlobalConstants;
using System.ComponentModel.DataAnnotations;

namespace HCMS.Web.ViewModels
{
    public class CreateCompanyViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256, ErrorMessage = GlobalConstant.MaxStringValidation)]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = GlobalConstant.MaxStringValidation)]
        public string PhoneNumber { get; set; }

        [Required]
        public string FullAddres { get; set; }
    }
}
