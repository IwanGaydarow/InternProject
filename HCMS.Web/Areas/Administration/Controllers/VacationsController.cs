namespace HCMS.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.GlobalConstants;
    using System.Threading.Tasks;
    using HCMS.Web.ViewModels.Vocations;
    using HCMS.Services.Data.Vocations;
    using HCMS.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using HCMS.Services.Data.Departments;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class VacationsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IVacationsService vacationsService;
        private readonly IDepartmentService departmentService;

        public VacationsController(UserManager<AppUser> userManager, IVacationsService vacationsService,
            IDepartmentService departmentService)
        {
            this.userManager = userManager;
            this.vacationsService = vacationsService;
            this.departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var vacations = this.vacationsService.GetAll<VacationsViewModel>(companyId);

            var model = new AllVacationsViewModel { Vacations = vacations };

            return View("/Areas/Manager/Views/Vacations/Employee.cshtml", model);
        }

        public IActionResult ChangeStatus(int vacationId)
        {
            var model = new ChangeStatusViewModel { VacationId = vacationId, Role = "Administration" };

            return this.PartialView("/Areas/Manager/Views/Vacations/_ChangeStatusPartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var returnModel = new ChangeStatusViewModel { VacationId = model.VacationId };
                return this.PartialView("/Areas/Manager/Views/Vacations/_ChangeStatusPartial.cshtml", returnModel);
            }

            await this.vacationsService.ChangeStatusAsync(model.VacationId, model.Status);

            return this.RedirectToAction("Index");
        }
    }
}