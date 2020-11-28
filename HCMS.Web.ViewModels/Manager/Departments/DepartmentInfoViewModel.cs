namespace HCMS.Web.ViewModels.Manager.Departments
{
    using AutoMapper;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    
    using System.Collections.Generic;

    public class DepartmentInfoViewModel : IMapFrom<Department>, IHaveCustomMappings
    {
        public string Tittle { get; set; }

        public IEnumerable<EmployeeInfoViewModel> Employees { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Department, DepartmentInfoViewModel>()
                .ForMember(x => x.Employees, y => y.MapFrom(d => d.Employess));
        }
    }
}
