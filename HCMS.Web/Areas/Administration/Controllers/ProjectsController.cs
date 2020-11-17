namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Projects;
    using HCMS.Services.Data.Departments;
    using System.Collections.Generic;
    using System.Security.Claims;
    using HCMS.Web.ViewModels.Administration.Projects;

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
            var departments = await this.GetDepartments<AllDepartmentsViewModel>(this.User);

            var model = new CreateViewModel { Departments = departments as IEnumerable<AllDepartmentsViewModel> };

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

        public async Task<IActionResult> Edit(int projectId)
        {
            var departments = await this.GetDepartments<AllDepartmentsViewModel>(this.User);
            var curentDepartment = this.projectsService.GetProjectById(projectId);

            var model = new UpdateViewModel 
            {
                ProjectId = curentDepartment.Id,
                Tittle = curentDepartment.Tittle,
                Description = curentDepartment.Description,
                EstimatedWorkHours = curentDepartment.EstimatedWorkHours,
                DepartmentId = curentDepartment.DepartmentId,
                Departments = departments 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.projectsService.UpdateAsync(model);

            return this.RedirectToAction("Index");
        }

        public IActionResult Delete()
        {
            return View();
        }

        public Task<int> Status(int projectId)
        {
            var result = this.projectsService.ChangeStatus(projectId);

            return result;
        }

        private async Task<IEnumerable<T>> GetDepartments<T>(ClaimsPrincipal User)
        {
            var user = await this.userManager.GetUserAsync(User);
            var companyId = GetCompanyId(user.DepartmentId);
            var departments = this.departmentService.GetAllDepartments<T>(companyId);

            return departments;
        }

        private int GetCompanyId(int? id)
        {
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(id);

            return companyId;
        }
    }
}