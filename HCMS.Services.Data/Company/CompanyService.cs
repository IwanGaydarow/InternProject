namespace HCMS.Services.Data.Company
{
    using System.Linq;
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Data.Common.Repositories;
    using System.Threading.Tasks;
    using HCMS.Web.ViewModels;
    using System;

    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> companyRepository;

        public CompanyService(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task Create(CreateCompanyViewModel model)
        {
            var company = new Company
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullAddres = model.FullAddres,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await this.companyRepository.AddAsync(company);
            await this.companyRepository.SaveChangesAsync();
        }

        public int GetIdByName(string name)
        {
            return this.companyRepository.All()
                .Where(x => x.Name == name)
                .Select(x => x.Id)
                .First();
        }

        public T GetInfo<T>(int companyId)
        {
            return this.companyRepository.All()
                .Where(x => x.Id == companyId)
                .To<T>().First();
        }
    }
}
