namespace HCMS.Services.Data
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using HCMS.Data.Common.Repositories;
    using HCMS.Data.Models;
    using HCMS.Web.ViewModels.Administration;

    public class CityService : ICityService
    {
        private readonly IRepository<City> cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public int CheckIfCityExist(string cityName, int countryId)
        {
            return this.cityRepository.All()
                .Where(x => x.Name.ToLower() == cityName.ToLower() && x.CountryId == countryId)
                .Select(x => x.Id).FirstOrDefault();
        }

        public async Task<int> CreateCityAsync(CreateCityViewModel model, int countryId)
        {
            var city = new City
            {
                Name = model.CityName,
                PostCode = model.PostCode,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                CountryId = countryId
            };

            await this.cityRepository.AddAsync(city);
            await this.cityRepository.SaveChangesAsync();

            return city.Id;
        }
    }
}
