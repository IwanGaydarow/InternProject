namespace HCMS.Web.Areas.Employee.Controllers
{
    using System;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Vocations;
    using HCMS.Web.ViewModels.Vocations;

    [Authorize(Roles = GlobalConstant.SystemEmployeeRole)]
    [Area("Employee")]
    public class VacationsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IVacationsService vacationsService;

        public VacationsController(UserManager<AppUser> userManager, IVacationsService vacationsService)
        {
            this.userManager = userManager;
            this.vacationsService = vacationsService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var vacations = this.vacationsService.GetAllForPerson<VacationsViewModel>(user.Id);

            var model = new AllVacationsViewModel { Vacations = vacations };
            return View("/Areas/Manager/Views/Vacations/Index.cshtml", model);
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = new CreateVacationViewModel { UserId = user.Id, FromDate = DateTime.UtcNow.Date, ToDate = DateTime.UtcNow.Date };
            return this.View("/Areas/Manager/Views/Vacations/Create.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVacationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("/Areas/Manager/Views/Vacations/Create.cshtml", model);
            }

            await this.vacationsService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }
    }
}
