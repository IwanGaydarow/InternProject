﻿namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Projects;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration;
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
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var projects = this.projectsService.GetAllProjects<ProjectViewModel>(companyId);
            var model = new AllProjectsViewModel { Projects = projects };

            return this.View(model);
        }

        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(this.User);
            var departments = this.departmentService
                                    .GetDepartmentsForSelectList<AllDepartmentsViewModel>(user.DepartmentId);

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

        public async Task<IActionResult> Edit(int projectId)
        {
            var user = await userManager.GetUserAsync(this.User);
            var departments = this.departmentService
                                    .GetDepartmentsForSelectList<AllDepartmentsViewModel>(user.DepartmentId);
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

            return this.View(model);
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

        public async Task<IActionResult> Delete(int projectId)
        {
            await this.projectsService.DeleteAsync(projectId);
           
            return this.Ok();
        }

        public async Task<int> Status(int projectId)
        {
            var result = await this.projectsService.ChangeStatusAsync(projectId);

            return result;
        }
    }
}