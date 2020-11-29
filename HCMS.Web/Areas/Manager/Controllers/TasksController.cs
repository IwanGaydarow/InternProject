namespace HCMS.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.GlobalConstants;
    using Microsoft.AspNetCore.Identity;
    using HCMS.Data.Models;
    using HCMS.Services.Data.Departments;
    using HCMS.Services.Data.Projects;
    using System.Threading.Tasks;
    using HCMS.Web.ViewModels.Manager.Tasks;
    using System.Collections.Generic;
    using HCMS.Services.Data.Tasks;
    using HCMS.Web.ViewModels.Administration.Evaluation;
    using HCMS.Services.Data.Employees;
    using HCMS.Web.ViewModels.Administration.Trainings;

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class TasksController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;
        private readonly IProjectService projectService;
        private readonly ITasksService tasksService;
        private readonly IEmployeService employeService;

        public TasksController(UserManager<AppUser> userManager, IDepartmentService departmentService,
            IProjectService projectService, ITasksService tasksService,
            IEmployeService employeService)
        {
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.projectService = projectService;
            this.tasksService = tasksService;
            this.employeService = employeService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var tasks = this.tasksService.GetAllTasks<TasksViewModel>(companyId, user.DepartmentId.Value);

            foreach (var task in tasks)
            {
                task.AssignedEmployee = this.tasksService.GetUserAssingToTask(task.Id);
            }

            var model = new ProjectTasksViewModel { Tasks = tasks };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var projects = this.projectService.GetAllProjectsByDepartment<ProjectViewModel>(companyId, user.DepartmentId.Value);

            var model = new CreateTaskViewModel { Projects = projects };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var project = this.projectService.GetProjectById<ProjectViewModel>(model.ProjectId);
                if (project == null)
                {
                    this.ModelState.AddModelError(string.Empty, "Selected project does not exist.");

                    var user = await this.userManager.GetUserAsync(this.User);
                    var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

                    var allProjects = this.projectService.GetAllProjectsByDepartment<ProjectViewModel>(companyId, user.DepartmentId.Value);
                    model.Projects = allProjects;

                    return this.View(model);
                }

                var projects = new List<ProjectViewModel>();
                projects.Add(project);

                model.Projects = projects;

                return this.View(model);
            }

            await this.tasksService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int taskId)
        {
            var task = this.tasksService.GetById(taskId);

            var model = new EditTaskViewModel
            {
                Id = task.Id,
                Tittle = task.Tittle,
                Description = task.Description,
                EstWorkHours = task.EstimatedWorkHours,
                Status = task.Status
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTaskViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.tasksService.UpdateAsync(model);

            return this.RedirectToAction("Index");
        }

        public async Task<int> Status(int taskId)
        {
            var result = await this.tasksService.ChangeStatusAsync(taskId);

            return result;
        }

        public async Task<IActionResult> AddEmployee(int taskId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);
            var users = this.employeService.EmployeesOfDepartment<EmployeeSelectList>(companyId, user.DepartmentId.Value);

            var model = new EmployeeToTaskViewModel { Employees = users, TaskId = taskId };

            return this.PartialView("_AddEmployeeToTaskPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeToTaskViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                if (model.EmployeeId == null)
                {
                    return await this.AddEmployee(model.TaskId);
                }

                var employees = new List<EmployeeSelectList>();
                var employee = this.employeService
                                            .GetById<EmployeeSelectList>(model.EmployeeId);
                employees.Add(employee);
                model.Employees = employees;

                return this.PartialView("_AddEmployeeToTaskPartial", model);
            }

            await this.tasksService.AddEmployeeToTask(model.TaskId, model.EmployeeId);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int taskId)
        {
            await this.tasksService.DeleteAsync(taskId);

            return this.Ok();
        }
    }
}