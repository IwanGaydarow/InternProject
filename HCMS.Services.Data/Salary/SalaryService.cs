namespace HCMS.Services.Data.Salary
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Data.Common.Repositories;
    using HCMS.Web.ViewModels.Administration.Salary;

    public class SalaryService : ISalaryService
    {
        private readonly IRepository<Salary> salaryRepository;

        public SalaryService(IRepository<Salary> salaryRepository)
        {
            this.salaryRepository = salaryRepository;
        }

        public bool CheckForSalary(string employeeId)
        {
            return this.salaryRepository.All()
                .Any(x => x.UserId == employeeId);
        }

        public async Task CreateAsync(CreateSalaryViewModel model)
        {
            var salary = new Salary
            {
                Salary1 = model.Salary,
                Periodicity = model.Periodicity,
                Currency = model.Currency,
                UserId = model.UserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await this.salaryRepository.AddAsync(salary);
            await this.salaryRepository.SaveChangesAsync();
        }

        public async Task ReplaceExistingSalaryAsync(CreateSalaryViewModel model)
        {
            var existingSalary = this.salaryRepository.All()
                .Where(x => x.UserId == model.UserId).First();
            existingSalary.EffectiveTo = DateTime.UtcNow;

            this.salaryRepository.Update(existingSalary);
            await this.salaryRepository.SaveChangesAsync();

            await this.CreateAsync(model);
        }

        public T GetSalary<T>(string employeeId)
        {
            return this.salaryRepository.All()
                 .Where(x => x.UserId == employeeId
                        && x.EffectiveTo == null)
                 .To<T>().First();
        }
    }
}
