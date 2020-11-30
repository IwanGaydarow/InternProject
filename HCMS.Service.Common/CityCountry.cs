using HCMS.Services.Data;
using HCMS.Web.ViewModels.Administration;
using System.Threading.Tasks;

namespace HCMS.Service.Common
{
    public class CityCountry : ICityCountry
    {
        private readonly ICountryService countryService;
        private readonly ICityService cityService;

        public CityCountry(ICountryService countryService, ICityService cityService)
        {
            this.countryService = countryService;
            this.cityService = cityService;
        }
        public async Task<int> PrepareCityAndCountry(CreateCityViewModel cityModel, string countryName)
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
