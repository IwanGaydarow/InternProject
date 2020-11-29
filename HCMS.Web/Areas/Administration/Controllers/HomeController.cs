namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.Web.ViewModels;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Company;
    using HCMS.Services.Data.Projects;
    using HCMS.Services.Data.Trainings;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Home;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;
        private readonly IProjectService projectService;
        private readonly ITrainingService trainingService;
        private readonly IEmployeService employeService;
        private readonly ICompanyService companyService;

        public HomeController(UserManager<AppUser> userManager, IDepartmentService departmentService,
            IProjectService projectService, ITrainingService trainingService,
            IEmployeService employeService, ICompanyService companyService)
        {
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.projectService = projectService;
            this.trainingService = trainingService;
            this.employeService = employeService;
            this.companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var departmentsCount = this.departmentService.GetCount(companyId);
            var employeeCount = this.employeService.GetCOunt(companyId);
            var projectCOunt = this.projectService.GetCount(companyId);
            var finishedProject = this.projectService.GetCountOfFinished(companyId);
            var processingProject = this.projectService.GetCountOfProcessing(companyId);
            var trainingsCount = this.trainingService.GetCount(companyId);
            var companyModel = this.companyService.GetInfo<CompanyViewModel>(companyId);


            var model = new HomePageViewModel
            {
                CountOfDepartments = departmentsCount,
                EmployeeCount = employeeCount,
                ProjectCount = projectCOunt,
                CountOfFinishProject = finishedProject,
                CountOfProcessingProject = processingProject,
                CountOfTrainings = trainingsCount,
                Company = companyModel
            };

            return this.View(model);
        }
    }
}