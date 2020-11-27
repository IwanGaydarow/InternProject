namespace HCMS.Services.Data.Trainings
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using HCMS.Web.ViewModels.Administration.Trainings;

    public interface ITrainingService
    {
        Task CreateAsync(CreateTrainingViewModel model);

        Task EditAsync(EditTrainingViewModel model, int trainingId);

        Task AddEmployeeToTraining(string userId, int trainingId);

        IEnumerable<T> GetAll<T>(int companyId);

        T GetById<T>(int trainingId);

        Task DeleteAsync(int id);

        int GetCount(int companyId);
    }
}
