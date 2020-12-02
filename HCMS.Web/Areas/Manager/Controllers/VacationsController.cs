namespace HCMS.Web.Areas.Manager.Controllers
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

            var vacations = this.vocationService.GetAllForPerson<VacationsViewModel>(user.Id);

            var model = new AllVacationsViewModel { Vacations = vacations };
            return View(model);
        }

        public async Task<IActionResult> Employee()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var vacantions = this.vocationService.GetAllForDepartment<VacationsViewModel>(user.DepartmentId.Value, user.Id);

            var model = new AllVacationsViewModel { Vacations = vacantions };
            return this.View(model);
        }

        public IActionResult ChangeStatus(int vacationId)
        {
            var model = new ChangeStatusViewModel { VacationId = vacationId, Role = "Manager" };

            return this.PartialView("_ChangeStatusPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var returnModel = new ChangeStatusViewModel { VacationId = model.VacationId };
                return this.PartialView("_ChangeStatusPartial", returnModel);
            }

            await this.vocationService.ChangeStatusAsync(model.VacationId, model.Status);

            return this.RedirectToAction("Employee");
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