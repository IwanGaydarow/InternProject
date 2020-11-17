namespace HCMS.Web.ViewModels.Administration.Departments
{
    using AutoMapper;
    using HCMS.Data.Models;

    using HCMS.Services.Mapping;
    using System.Linq;

    public class DepartmentViewModel : IMapFrom<Department>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Tittle { get; set; }

        public string DepartmentMenager { get; set; }

        public string CompanyName { get; set; }

        public int Employees { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Department, DepartmentViewModel>()
                .ForMember(
                x => x.DepartmentMenager,
                y => y.MapFrom(x => x.DepartmentManagerNavigation.Name));

            configuration.CreateMap<Department, DepartmentViewModel>()
                .ForMember(x => x.CompanyName,
                y => y.MapFrom(x => x.Company.Name));

            configuration.CreateMap<Department, DepartmentViewModel>()
                .ForMember(x => x.Employees,
                y => y.MapFrom(x => x.Employess.Count()));
        }
    }
}
