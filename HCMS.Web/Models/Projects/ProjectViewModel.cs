using AutoMapper;
using HCMS.Data.Models;
using HCMS.Services.Mapping;

namespace HCMS.Web.Models.Projects
{
    public class ProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Tittle { get; set; }

        public int EstimatedWorkHours { get; set; }

        public bool Status { get; set; }

        public string Department { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ProjectViewModel>()
                .ForMember(x => x.Department,
                y => y.MapFrom(x => x.Department.Tittle));
        }
    }
}
