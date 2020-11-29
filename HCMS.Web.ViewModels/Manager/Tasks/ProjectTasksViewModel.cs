namespace HCMS.Web.ViewModels.Manager.Tasks
{
    using System.Collections.Generic;
    using AutoMapper;
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class ProjectTasksViewModel
    {
        public IEnumerable<TasksViewModel> Tasks { get; set; }
    }
}
