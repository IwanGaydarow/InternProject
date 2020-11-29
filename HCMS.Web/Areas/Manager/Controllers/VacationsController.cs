namespace HCMS.Web.Areas.Manager.Controllers
{
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Vocations;
    using HCMS.Web.ViewModels.Vocations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class VacationsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IVacationsService vocationService;

        public VacationsController(UserManager<AppUser> userManager,
            IVacationsService vocationService)
        {
            this.userManager = userManager;
            this.vocationService = vocationService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var vocations = this.vocationService.GetAllForPerson<VacationsViewModel>(user.Id);

            var model = new AllVacationsViewModel { Vocations = vocations };
            return View(model);
        }

        public async  Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = new CreateVacationViewModel { UserId = user.Id, FromDate = DateTime.UtcNow.Date, ToDate = DateTime.UtcNow.Date };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVacationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.vocationService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }
    }
}