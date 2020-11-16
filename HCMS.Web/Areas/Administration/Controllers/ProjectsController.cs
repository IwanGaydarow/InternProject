namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Web.Models.Projects;
    using HCMS.Services.Data.Projects;
    using HCMS.Services.Data.Departments;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
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
            var companyId = this.GetCompanyId(user.DepartmentId);

            var projects = this.projectsService.GetAllProjects<ProjectViewModel>(companyId);

            var model = new AllProjectsViewModel { Projects = projects };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = GetCompanyId(user.DepartmentId);
            var departments = this.departmentService.GetAllDepartments<AllDepartmentsViewModel>(companyId);

            var model = new CreateViewModel { Departments = departments };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.projectsService.CreateAsync(model.Tittle,
                    model.Description, model.EstimateWorkHour, model.DepartmentId);

            return this.RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int model)
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        private int GetCompanyId(int? id)
        {
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(id);

            return companyId;
        }
    }
}