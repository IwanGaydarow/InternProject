namespace HCMS.Web.ViewModels.Administration.Trainings
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.GlobalConstants;

    public class EditTrainingViewModel
    {
        [Required]
        [Display(Name = "Training Tittle")]
        [StringLength(80, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
        public string Tittle { get; set; }

        [Required]
        [Display(Name = "Trainig Description")]
        [MaxLength(500, ErrorMessage = GlobalConstant.MaxStringValidation)]
        public string Description { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        [Range(1, 200, ErrorMessage = GlobalConstant.NumberRangeErrorMessage)]
        public int TrainingHours { get; set; }
    }
}
