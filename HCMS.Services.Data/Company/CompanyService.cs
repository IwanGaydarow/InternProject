namespace HCMS.Services.Data.Company
{
    using System.Linq;
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Data.Common.Repositories;

    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> companyRepository;

        public CompanyService(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public T GetInfo<T>(int companyId)
        {
            return this.companyRepository.All()
                .Where(x => x.Id == companyId)
                .To<T>().First();
        }
    }
}
