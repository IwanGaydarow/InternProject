namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.GlobalConstants;
    using HCMS.Web.ViewModels.Administration.Trainings;
    using HCMS.Services.Data.Trainings;
    using Microsoft.AspNetCore.Identity;
    using HCMS.Data.Models;
    using HCMS.Services.Data.Departments;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class TrainingsController : Controller
    {
        private readonly ITrainingService trainingService;
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;

        public TrainingsController(ITrainingService trainingService, UserManager<AppUser> userManager,
            IDepartmentService departmentService)
        {
            this.trainingService = trainingService;
            this.userManager = userManager;
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var trainings = this.trainingService.GetAll<TrainingViewModel>(companyId);
            var model = new AllTrainingsViewModel { Trainings = trainings };

            return View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainingViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainingService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }
    }
}