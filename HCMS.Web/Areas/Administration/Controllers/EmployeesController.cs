namespace HCMS.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.Web.ViewModels;
    using HCMS.Service.Common;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration;
    using HCMS.Web.ViewModels.Administration.Employees;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class EmployeesController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEmployeService employeService;
        private readonly IDepartmentService departmentService;
        private readonly ICityCountry cityCountryService;

        public EmployeesController(UserManager<AppUser> userManager,
            IEmployeService employeService, IDepartmentService departmentService,
            ICityCountry cityCountryService)
        {
            this.userManager = userManager;
            this.employeService = employeService;
            this.departmentService = departmentService;
            this.cityCountryService = cityCountryService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var employees = this.employeService.GetAllEmployees<EmployeesViewModel>(companyId);
            foreach (var employee in employees)
            {
                var curEmployee = await this.userManager.FindByIdAsync(employee.Id);
                var role = await this.userManager.GetRolesAsync(curEmployee);
                employee.Role = String.Join(", ", role);
            }

            var model = new AllEmployeesViewModel { Employees = employees };
            return View(model);

        }

        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(this.User);
            var departments = this.departmentService
                                    .GetDepartmentsForSelectList<AllDepartmentsViewModel>(user.DepartmentId);
            var model = new CreateViewModel { Departments = departments };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var cityId = await this.cityCountryService.PrepareCityAndCountry(model.City, model.CountryName);

                var employee = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.FullName,
                    Address = model.Address,
                    DepartmentId = model.DepartmentId,
                    Gender = model.Gender.ToLower(),
                    JobTittle = model.JobTittle,
                    CityId = cityId,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };

                var result = await this.userManager.CreateAsync(employee, model.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(employee, GlobalConstant.SystemEmployeeRole);

                    return this.RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.View(model);
        }

        public async Task<IActionResult> Delete(string employeeId)
        {
            await this.employeService.DeleteAsync(employeeId);

            return this.Ok();
        }

        public IActionResult AddRole(string employeeId)
        {
            var rols = new string[] {GlobalConstant.SystemAdministratorRole,
                       GlobalConstant.SystemManagerRole, GlobalConstant.SystemEmployeeRole};

            var model = new AddRoleDropdownViewModel { EmployeeId = employeeId, Rols = rols };

            return this.PartialView("_AddRolePartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleDropdownViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView(model);
            }

            var user = await this.userManager.FindByIdAsync(model.EmployeeId);
            var curRole = await this.userManager.GetRolesAsync(user);

            await this.userManager.RemoveFromRolesAsync(user, curRole);
            await this.userManager.AddToRoleAsync(user, model.Role);

            return this.RedirectToAction("Index");
        }

        public IActionResult Details(string employeeId, string role)
        {
            var model = this.employeService.EmployeeDetails<DetailViewModel>(employeeId);
            model.Role = role;
            model.Id = employeeId;
            model.EmailForm = new EmailFormViewModel();
            model.EmailForm.ReturnUrl = "/Administration/Employees/Index";

            return this.View("DetailEditView", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DetailViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("DetailEditView", model);
            }

            await this.employeService.UpdateAsync(model);

            return this.RedirectToAction("Index");
        }
    }
}