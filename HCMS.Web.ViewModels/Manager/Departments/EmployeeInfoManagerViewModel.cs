using AutoMapper;
using HCMS.Data.Models;
using HCMS.Services.Mapping;

namespace HCMS.Web.ViewModels.Manager.Departments
{
    public class EmployeeInfoManagerViewModel : IMapFrom<AppUser>, IHaveCustomMappings
    {
        public string EmployeeName { get; set; }

        public string JobTittle { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AppUser, EmployeeInfoManagerViewModel>()
                .ForMember(x => x.EmployeeName, y => y.MapFrom(e => e.Name))
                .ForMember(x => x.Town, y => y.MapFrom(e => e.City.Name));
        }
    }
}
