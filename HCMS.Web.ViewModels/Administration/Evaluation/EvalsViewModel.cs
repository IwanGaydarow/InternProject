namespace HCMS.Web.ViewModels.Administration.Evaluation
{
    using AutoMapper;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class EvalsViewModel : IMapFrom<Evaluations>, IMapFrom<AppUser>, IHaveCustomMappings
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int Percentage { get; set; }

        public int EvalYear { get; set; }

        public string Notes { get; set; }

        public string EmployeeEmail { get; set; }

        public string JobTittle { get; set; }

        public string EmployeeDepartment { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Evaluations, EvalsViewModel>()
                .ForMember(x => x.Percentage, y => y.MapFrom(e => e.Value))
                .ForMember(x => x.EvalYear, y => y.MapFrom(e => e.CreatedOn.Year));
                //.ForMember(x => x.EmployeeId, y => y.MapFrom(e => e.User.Id))
                //.ForMember(x => x.EmployeeName, y => y.MapFrom(e => e.User.Name))
                //.ForMember(x => x.EmployeeEmail, y => y.MapFrom(e => e.User.Email))
                //.ForMember(x => x.EmployeeDepartment, y => y.MapFrom(e => e.User.Department.Tittle));
        }
    }
}
