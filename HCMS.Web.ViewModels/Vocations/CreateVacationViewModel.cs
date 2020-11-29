using HCMS.GlobalConstants;
using System;
using System.ComponentModel.DataAnnotations;

namespace HCMS.Web.ViewModels.Vocations
{
    public class CreateVacationViewModel
    {
        [Required]
        [StringLength(80, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 4)]
        public string Tittle { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
