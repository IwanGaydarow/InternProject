namespace HCMS.Web.ViewModels.Administration.Salary
{
    using AutoMapper;
    using HCMS.Data.Models;
    using HCMS.Data.Models.Enums;
    using HCMS.Services.Mapping;

    public class SalaryEmployeeViewModel : IMapFrom<Salary>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string EmployeeName { get; set; }

        public string JobTittle { get; set; }

        public string Department { get; set; }

        public decimal BasicSalary { get; set; }

        public SalaryPeriodicity Periodicity { get; set; }

        public SalaryCurrency Currency { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Salary, SalaryEmployeeViewModel>()
                .ForMember(x => x.EmployeeName, y => y.MapFrom(e =>
                            e.User.Name))
                .ForMember(x => x.JobTittle, y => y.MapFrom(e =>
                            e.User.JobTittle))
                .ForMember(x => x.Department, y => y.MapFrom(e =>
                            e.User.Department.Tittle))
                .ForMember(x => x.UserId, y => y.MapFrom(e =>
                            e.UserId))
                .ForMember(x => x.BasicSalary, y => y.MapFrom(s =>
                            s.Salary1));
        }
    }
}
