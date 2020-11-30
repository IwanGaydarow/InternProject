namespace HCMS.Web.ViewModels.Employee
{
    using AutoMapper;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;


    public class EmployeeTasksViewModel : IMapFrom<UsersTasks>, IHaveCustomMappings
    {
        public string Tittle { get; set; }

        public string Description { get; set; }

        public int EstimatedWorkHours { get; set; }

        public bool Status { get; set; }

        public string Project { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UsersTasks, EmployeeTasksViewModel>()
                .ForMember(x => x.Tittle, y => y.MapFrom(ut => ut.ProjectTask.Tittle))
                .ForMember(x => x.Description, y => y.MapFrom(ut => ut.ProjectTask.Description))
                .ForMember(x => x.EstimatedWorkHours, y => y.MapFrom(ut => ut.ProjectTask.EstimatedWorkHours))
                .ForMember(x => x.Status, y => y.MapFrom(ut => ut.ProjectTask.Status))
                .ForMember(x => x.Project, y => y.MapFrom(ut => ut.ProjectTask.Project.Tittle));

        }
    }
}
