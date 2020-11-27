namespace HCMS.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.Services.Data;
    using HCMS.Web.ViewModels;
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
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly IDepartmentService departmentService;

        public EmployeesController(UserManager<AppUser> userManager,
            IEmployeService employeService, ICityService cityService,
            ICountryService countryService, IDepartmentService departmentService)
        {
            this.userManager = userManager;
            this.employeService = employeService;
            this.cityService = cityService;
            this.countryService = countryService;
            this.departmentService = departmentService;
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
                var cityId = await this.PrepareCityAndCountry(model.City, model.CountryName);

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

        /// <summary>
        /// Check if country exist. If not create it.Check if city exist in this country. If no create it.
        /// </summary>
        /// <param name="cityModel"></param>
        /// <param name="countryName"></param>
        /// <returns>Return city id.</returns>
        /// 

        //TODO: Test!!!!
        private async Task<int> PrepareCityAndCountry(CreateCityViewModel cityModel, string countryName)
        {
            var countryId = this.countryService.CheckCountryExist(countryName);
            if (countryId == 0)
            {
                countryId = await this.countryService.CreateCountryAsync(countryName);
            }

            var cityId = this.cityService.CheckIfCityExist(cityModel.CityName, countryId);
            if (cityId == 0)
            {
                cityId = await this.cityService.CreateCityAsync(cityModel, countryId);
            }

            return cityId;
        }
    }
}