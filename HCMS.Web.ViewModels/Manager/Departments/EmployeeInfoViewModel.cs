namespace HCMS.Web.ViewModels.Manager.Departments
{
    using AutoMapper;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class EmployeeInfoViewModel : IMapFrom<AppUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string JobTittle { get; set; }

        public string Email { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AppUser, EmployeeInfoViewModel>()
                .ForMember(x => x.FullName,
                y => y.MapFrom(x => x.Name));
        }
    }
}
