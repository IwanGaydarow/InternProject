namespace HCMS.Web.Areas.Employee.Controllers
{
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Tasks;
    using HCMS.Services.Data.Vocations;
    using HCMS.Web.ViewModels;
    using HCMS.Web.ViewModels.Employee.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstant.SystemEmployeeRole)]
    [Area("Employee")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IVacationsService vacationsService;
        private readonly ITasksService tasksService;

        public HomeController(UserManager<AppUser> userManager, IVacationsService vacationsService,
            ITasksService tasksService)
        {
            this.userManager = userManager;
            this.vacationsService = vacationsService;
            this.tasksService = tasksService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = new EmployeeHomePageViewModel
            {
                PersonalVacationCreate = this.vacationsService.GetPersonalCount(user.Id),
                PersonalAcceptVacation = this.vacationsService.GetPersonalCountOfAccepted(user.Id),
                PersonalRejectVacation = this.vacationsService.GetPersonalCountOfReject(user.Id),
                CountOfUnfinishedTasks = this.tasksService.CountOfUnfinishedTasksByUserId(user.Id),
                CountOfTasks = this.tasksService.CountOfTasksByUserId(user.Id),
                CountOfFinishedTasks = this.tasksService.CountOfFinishedTasksByUserId(user.Id),
                EmailForm = new EmailFormViewModel
                                {
                                    ReturnUrl = "/Employee",
                                    From = user.Email
                                }
            };

            return View(model);
        }
    }
}