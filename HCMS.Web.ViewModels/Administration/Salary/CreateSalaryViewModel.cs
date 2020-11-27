namespace HCMS.Web.ViewModels.Administration.Salary
{
    using System.ComponentModel.DataAnnotations;
    
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Mapping;
    using HCMS.Data.Models.Enums;
    
    using AutoMapper;

    public class CreateSalaryViewModel :IMapFrom<Salary>, IHaveCustomMappings
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = GlobalConstant.NumberRangeErrorMessage)]
        public decimal Salary { get; set; }

        [Required]
        public SalaryPeriodicity Periodicity { get; set; }

        [Required]
        public SalaryCurrency Currency { get; set; }

        [Required]
        public string UserId { get; set; }

        [IgnoreMap]
        public bool availableSalary { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Salary, CreateSalaryViewModel>()
                .ForMember(x => x.Salary, y => y.MapFrom(s => s.Salary1));
        }
    }
}
