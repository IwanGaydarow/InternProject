namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Services.Data;
    using HCMS.GlobalConstants;
    using HCMS.Web.ViewModels.Administration;
    using HCMS.Web.ViewModels.Administration.Employees;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class EmployeesController : Controller
    {
        private readonly ICityService cityService;
        private readonly ICountryService countryService;

        public EmployeesController(ICityService cityService, ICountryService countryService)
        {
            this.cityService = cityService;
            this.countryService = countryService;
        }

        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var cityId = await this.PrepareCityAndCountry(model.City, model.CountryName);

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