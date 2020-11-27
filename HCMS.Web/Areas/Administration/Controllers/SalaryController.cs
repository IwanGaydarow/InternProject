namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.GlobalConstants;
    using HCMS.Web.ViewModels.Administration.Salary;
    using HCMS.Services.Data.Salary;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class SalaryController : Controller
    {
        private readonly ISalaryService salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string employeeId)
        {
            var availableSalary = this.salaryService.CheckForSalary(employeeId);
            if (availableSalary)
            {
                var currentSalary = this.salaryService.GetSalary<CreateSalaryViewModel>(employeeId);
                currentSalary.availableSalary = availableSalary;
               
                return this.View(currentSalary);
            }

            var model = new CreateSalaryViewModel { UserId = employeeId, availableSalary = availableSalary };
            
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalaryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.availableSalary)
            {
                await this.salaryService.ReplaceExistingSalaryAsync(model);
            }
            else
            {
                await this.salaryService.CreateAsync(model);
            }

            return this.RedirectToAction("Index", "Employees", new { area = "Administration" });
        }
    }
}