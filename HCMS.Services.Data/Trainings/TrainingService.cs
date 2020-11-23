namespace HCMS.Services.Data.Trainings
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Data.Common.Repositories;
    using HCMS.Web.ViewModels.Administration.Trainings;

    public class TrainingService : ITrainingService
    {
        private readonly IRepository<Trainings> trainingsRepository;

        public TrainingService(IRepository<Trainings> trainingsRepository)
        {
            this.trainingsRepository = trainingsRepository;
        }

        public async Task CreateAsync(CreateTrainingViewModel model)
        {
            var training = new Trainings
            {
                Tittle = model.Tittle,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Start = model.Start,
                End = model.End,
                TrainingHours = model.TrainingHours,
                CompanyId = model.CompanyId
            };

            await this.trainingsRepository.AddAsync(training);
            await this.trainingsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var training = await this.trainingsRepository.GetByIdAsync(id);
            if (training == null)
            {
                throw new NullReferenceException($"Training for delete, with id = {id} is not found.");
            }

            training.IsDeleted = true;
            training.DeletedOn = DateTime.UtcNow;

            this.trainingsRepository.Update(training);
            await this.trainingsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int companyId)
        {
            return this.trainingsRepository.All()
                .Where(x => x.CompanyId == companyId)
                .To<T>().ToList();
        }
    }
}
