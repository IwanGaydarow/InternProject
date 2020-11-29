using HCMS.Data.Models;
using HCMS.Services.Mapping;

namespace HCMS.Web.ViewModels.Manager.Tasks
{
    public class ProjectViewModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Tittle { get; set; }
    }
}
