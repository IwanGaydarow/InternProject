namespace HCMS.Services.Data.Trainings
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using HCMS.Web.ViewModels.Administration.Trainings;

    public interface ITrainingService
    {
        Task CreateAsync(CreateTrainingViewModel model);

        IEnumerable<T> GetAll<T>(int companyId);

        Task DeleteAsync(int id);
    }
}
