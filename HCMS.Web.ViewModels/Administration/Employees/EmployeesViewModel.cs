using AutoMapper;
using HCMS.Data.Models;
using HCMS.Services.Mapping;
using System.Linq;

namespace HCMS.Web.ViewModels.Administration.Employees
{
    public class EmployeesViewModel : IMapFrom<AppUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string JobTittle { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }

        public string Role { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AppUser, EmployeesViewModel>()
                .ForMember(x => x.Department,
                y => y.MapFrom(u => u.Department.Tittle))
                .ForMember(x => x.FullName,
                y => y.MapFrom(x => x.Name));
        }
    }
}
