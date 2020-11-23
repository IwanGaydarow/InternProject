namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Trainings;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Trainings;

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

            return this.View(model);
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

            var user = await this.userManager.GetUserAsync(this.User);
            model.CompanyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            await this.trainingService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int trainingId)
        {
            await this.trainingService.DeleteAsync(trainingId);

            return this.Ok();
        }
    }
}