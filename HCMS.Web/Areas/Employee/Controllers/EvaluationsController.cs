namespace HCMS.Web.Areas.Employee.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    
    using HCMS.GlobalConstants;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using HCMS.Data.Models;
    using HCMS.Services.Data;
    using HCMS.Web.ViewModels.Employee;

    [Authorize(Roles = GlobalConstant.SystemEmployeeRole)]
    [Area("Employee")]
    public class EvaluationsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEvaluationService evaluationService;

        public EvaluationsController(UserManager<AppUser> userManager, IEvaluationService evaluationService)
        {
            this.userManager = userManager;
            this.evaluationService = evaluationService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var evals = this.evaluationService.GetAllByUserId<EmployeeEvalsViewModel>(user.Id);

            var model = new AllEmployeeEvalsViewModel { Evals = evals };
            return View(model);
        }
    }
}