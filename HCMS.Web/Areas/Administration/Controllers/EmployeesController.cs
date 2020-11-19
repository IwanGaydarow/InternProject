namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Services.Data;
    using HCMS.GlobalConstants;
    using HCMS.Web.ViewModels.Administration;
    using HCMS.Web.ViewModels.Administration.Employees;
    using HCMS.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using HCMS.Services.Data.Employees;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using HCMS.Services.Data.Departments;

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

        public IActionResult Index()
        {
            return View();

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
            ;
            if (!this.ModelState.IsValid)
            {
                var cityId = await this.PrepareCityAndCountry(model.City, model.CountryName);

                var employee = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.FullName,
                    Address = model.Address,
                    Gender = model.Gender.ToLower(),
                    JobTittle = model.JobTittle,
                    CityId = cityId,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };

                var result = await this.userManager.CreateAsync(employee, model.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(employee, GlobalConstant.SystemAdministratorRole);

                    return this.RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.View(model);
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