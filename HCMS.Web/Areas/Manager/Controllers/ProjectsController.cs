namespace HCMS.Web.Areas.Manager.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Projects;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Projects;

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class ProjectsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IProjectService projectsService;
        private readonly IDepartmentService departmentService;

        public ProjectsController(UserManager<AppUser> userManager,
            IProjectService projectsService,
            IDepartmentService departmentService)
        {
            this.userManager = userManager;
            this.projectsService = projectsService;
            this.departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var projects = this.projectsService.GetAllProjectsByDepartment<ProjectViewModel>(companyId, user.DepartmentId.Value);
            var model = new AllProjectsViewModel { Projects = projects };

            return View("/Areas/Administration/Views/Projects/Index.cshtml", model);
        }
    }
}