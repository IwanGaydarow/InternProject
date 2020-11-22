namespace HCMS.Web.ViewModels.Administration.Evaluation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HCMS.GlobalConstants;

    public class CreateEvaluationViewModel
    {
        [Required]
        public string EmployeeId { get; set; }

        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Evaluation in percentage %:")]
        public int Value { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 0)]
        public string Notes { get; set; }

        public IEnumerable<EmployeeSelectList> Employees { get; set; }
        public IEnumerable<int> Years { get; set; }
    }
}
