using System;
namespace HCMS.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.GlobalConstants;

    public class CreateCityViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string CityName { get; set; }

        [Display(Name = "City Post Code")]
        public int? PostCode { get; set; }
    }
}
