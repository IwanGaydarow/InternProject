namespace HCMS.Web.Areas.Manager.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
   
    using HCMS.Data.Models;
    using HCMS.Web.ViewModels;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Tasks;
    using HCMS.Services.Data.Projects;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Trainings;
    using HCMS.Services.Data.Vocations;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Manager.Home;

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;
        private readonly IProjectService projectService;
        private readonly IVacationsService vacationsService;
        private readonly ITasksService tasksService;
        private readonly ITrainingService trainingService;
        private readonly IEmployeService employeService;

        public HomeController(UserManager<AppUser> userManager, IDepartmentService departmentService,
            IProjectService projectService, IVacationsService vacationsService,
            ITasksService tasksService, ITrainingService trainingService,
            IEmployeService employeService)
        {
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.projectService = projectService;
            this.vacationsService = vacationsService;
            this.tasksService = tasksService;
            this.trainingService = trainingService;
            this.employeService = employeService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var model = new ManagerHomePageViewModel();
            model.EmployeeCount = 
                            this.employeService.GetEmployeeCountOfDepartment(user.DepartmentId.Value);
            model.UnfinishedProjectCount = 
                            this.projectService.GetCountByDepartment(user.DepartmentId.Value);
            model.VacationForProcessing = 
                            this.vacationsService.GetCountOfUnproccessedByDepartment(user.DepartmentId.Value);
            model.CountOfUnfinishedTasks =
                            this.tasksService.CountOfUnfinishedTaskByDepartment(user.DepartmentId.Value);
            model.CountOfTrainings =
                            this.trainingService.GetCount(companyId);
            model.PersonalVacationCreate = 
                            this.vacationsService.GetPersonalCount(user.Id);
            model.PersonalAcceptVacation =
                            this.vacationsService.GetPersonalCountOfAccepted(user.Id);
            model.PersonalRejectVacation =
                            this.vacationsService.GetPersonalCountOfReject(user.Id);
            model.EmailForm = new EmailFormViewModel { ReturnUrl = "/Manager", From = user.Email };

            return View(model);
        }
    }
}