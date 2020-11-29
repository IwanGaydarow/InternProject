namespace HCMS.Services.Data.Vocations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Vocations;

    public interface IVacationsService
    {
        IEnumerable<T> GetAllForPerson<T>(string userId);

        Task CreateAsync(CreateVacationViewModel model);
    }
}
