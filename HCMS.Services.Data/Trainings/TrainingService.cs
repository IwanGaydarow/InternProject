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
        private readonly IRepository<TrainingsUsers> trainingsUsersRepository;

        public TrainingService(IRepository<Trainings> trainingsRepository,
            IRepository<TrainingsUsers> trainingsUsersRepository)
        {
            this.trainingsRepository = trainingsRepository;
            this.trainingsUsersRepository = trainingsUsersRepository;
        }

        public async Task AddEmployeeToTraining(string userId, int trainingId)
        {
            var trainingUser = new TrainingsUsers
            {
                UserId = userId,
                TrainingId = trainingId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await this.trainingsUsersRepository.AddAsync(trainingUser);
            await this.trainingsUsersRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateTrainingViewModel model)
        {
            var training = new Trainings
            {
                Tittle = model.TrainingInfo.Tittle,
                Description = model.TrainingInfo.Description,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                Start = model.TrainingInfo.Start,
                End = model.TrainingInfo.End,
                TrainingHours = model.TrainingInfo.TrainingHours,
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

            await this.DeteleAsyncTrainingsTasks(id);

            this.trainingsRepository.Update(training);
            await this.trainingsRepository.SaveChangesAsync();
        }

        private async Task DeteleAsyncTrainingsTasks(int trainingId)
        {
            var trainings = this.trainingsUsersRepository.All()
                .Where(x => x.TrainingId == trainingId)
                .ToList();

            if (trainings.Count > 0)
            {
                foreach (var training in trainings)
                {
                    training.IsDeleted = true;
                    training.DeletedOn = DateTime.UtcNow;

                    this.trainingsUsersRepository.Update(training);
                    await this.trainingsUsersRepository.SaveChangesAsync();
                }
            }
        }

        public async Task EditAsync(EditTrainingViewModel model, int trainingId)
        {
            var trainingToEdit = await this.trainingsRepository.GetByIdAsync(trainingId);
            if(trainingToEdit == null)
            {
                throw new NullReferenceException($"Trainings with id = {trainingId} for edit is not found.");
            }

            trainingToEdit.Tittle = model.Tittle;
            trainingToEdit.Description = model.Description;
            trainingToEdit.Start = model.Start;
            trainingToEdit.End = model.End;
            trainingToEdit.TrainingHours = model.TrainingHours;
            trainingToEdit.ModifiedOn = DateTime.UtcNow;

            this.trainingsRepository.Update(trainingToEdit);
            await this.trainingsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int companyId)
        {
            return this.trainingsRepository.All()
                .Where(x => x.CompanyId == companyId)
                .To<T>().ToList();
        }

        public T GetById<T>(int trainingId)
        {
            return this.trainingsRepository.All()
                .Where(x => x.Id == trainingId)
                .To<T>().FirstOrDefault();
        }

        public int GetCount(int companyId)
        {
            return this.trainingsRepository.All()
                .Where(x => x.CompanyId == companyId)
                .Count();
        }
    }
}
