namespace HCMS.Web.ViewModels.Administration.Evaluation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
   
    using AutoMapper;
   
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Mapping;

    public class EditEvalViewModel : IMapFrom<Evaluations>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Evaluation in percentage %:")]
        public int Value { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 0)]
        public string Notes { get; set; }

        public IEnumerable<int> Years { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Evaluations, EditEvalViewModel>()
                .ForMember(x => x.EmployeeName, y => y.MapFrom(e => e.User.Name))
                .ForMember(x => x.Year, y => y.MapFrom(e => e.EvaluationYear));
        }
    }
}
