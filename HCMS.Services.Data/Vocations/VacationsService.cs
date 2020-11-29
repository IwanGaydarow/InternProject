using HCMS.Data.Common.Repositories;
namespace HCMS.Services.Data.Vocations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
   
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Web.ViewModels.Vocations;

    public class VacationsService : IVacationsService
    {
        private readonly IRepository<Vacations> vocationsRepository;

        public VacationsService(IRepository<Vacations> vocationsRepository)
        {
            this.vocationsRepository = vocationsRepository;
        }
        public async Task CreateAsync(CreateVacationViewModel model)
        {
            var vocation = new Vacations
            {
                Tittle = model.Tittle,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                UserId = model.UserId
            };

            await this.vocationsRepository.AddAsync(vocation);
            await this.vocationsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllForPerson<T>(string userId)
        {
            return this.vocationsRepository.All()
                .Where(x => x.UserId == userId)
                .To<T>().ToList();
        }
    }
}
