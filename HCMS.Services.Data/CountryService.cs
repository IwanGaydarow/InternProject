using HCMS.Data.Common.Repositories;
using HCMS.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HCMS.Services.Data
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> countryRepository;

        public CountryService(IRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        /// <summary>
        /// Check if country existс.
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>return id if exist and 0 if not.</returns>
        public int CheckCountryExist(string countryName)
        {
            return this.countryRepository.All()
                .Where(x => x.Name.ToLower() == countryName.ToLower())
                .Select(x => x.Id).FirstOrDefault();
        }

        public async Task<int> CreateCountryAsync(string countryName)
        {
            var country = new Country
            {
                Name = countryName,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await this.countryRepository.AddAsync(country);
            await this.countryRepository.SaveChangesAsync();

            return country.Id;
        }
    }
}
