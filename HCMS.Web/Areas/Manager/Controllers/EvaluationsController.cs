namespace HCMS.Web.Areas.Manager.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    
    using HCMS.Data.Models;
    using HCMS.Services.Data;
    using HCMS.Service.Common;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Evaluation;

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class EvaluationsController : Controller
    {
        private readonly IEvaluationService evaluationService;
        private readonly IEmployeService employeeService;
        private readonly IYearsForEval yearsForEval;
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;

        public EvaluationsController(IEvaluationService evaluationService, IEmployeService employeeService,
            IYearsForEval yearsForEval, UserManager<AppUser> userManager, IDepartmentService departmentService)
        {
            this.evaluationService = evaluationService;
            this.employeeService = employeeService;
            this.yearsForEval = yearsForEval;
            this.userManager = userManager;
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var evals = this.evaluationService.GetAllForManager(companyId, user.DepartmentId.Value);

            var model = new AllEvalsViewModel { Evaluations = evals };

            return View("/Areas/Administration/Views/Evaluations/Index.cshtml", model);
        }

        public IActionResult Create(string employeeId)
        {
            var model = new CreateEvaluationViewModel();

            var years = this.yearsForEval.GetYearsForEval();
            var employee = this.employeeService.GetById<EmployeeSelectList>(employeeId);

            if (employee == null)
            {
                return this.NotFound();
            }

            model.EmployeeId = employee.Id;
            model.Name = employee.Name;
            model.Years = years;

            return View("/Areas/Administration/Views/Evaluations/Create.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEvaluationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var isEvaluated = this.evaluationService.ChekEvalInYear(model.Year, model.EmployeeId);

            if (isEvaluated)
            {
                this.ModelState.AddModelError(string.Empty, $"Evaluation for {model.Year} alredy exist. If you want to change it, view all Evaluations and click Edit button.");

                var employee = this.employeeService.GetById<EmployeeSelectList>(model.EmployeeId);
                model.Name = employee.Name;

                var years = this.yearsForEval.GetYearsForEval();
                model.Years = years;

                return this.View("/Areas/Administration/Views/Evaluations/Create.cshtml", model);
            }

            await this.evaluationService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }
    }
}