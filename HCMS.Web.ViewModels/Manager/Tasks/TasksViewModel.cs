namespace HCMS.Web.ViewModels.Manager.Tasks
{
    using AutoMapper;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class TasksViewModel : IMapFrom<ProjectTasks>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Tittle { get; set; }

        public string Description { get; set; }

        public int EstWorkHours { get; set; }

        public bool Status { get; set; }

        public string ProjectTittle { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProjectTasks, TasksViewModel>()
                .ForMember(x => x.EstWorkHours, y => y.MapFrom(pt => pt.EstimatedWorkHours))
                .ForMember(x => x.ProjectTittle, y => y.MapFrom(pt => pt.Project.Tittle));
        }
    }
}
