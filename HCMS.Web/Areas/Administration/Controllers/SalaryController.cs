namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Salary;
    using Microsoft.AspNetCore.Identity;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Salary;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class SalaryController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ISalaryService salaryService;
        private readonly IDepartmentService departmentService;

        public SalaryController(UserManager<AppUser> userManager, ISalaryService salaryService,
            IDepartmentService departmentService)
        {
            this.userManager = userManager;
            this.salaryService = salaryService;
            this.departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var salaries = this.salaryService.GetSalaries<SalaryEmployeeViewModel>(companyId);

            var model = new AllSalaries { Salaries = salaries };
            
            return this.View(model);
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

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int salaryId)
        {
            await this.salaryService.DeleteAsync(salaryId);

            return this.Ok();
        }
    }
}