namespace HCMS.Services.Data.Vocations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Vocations;

    public interface IVacationsService
    {
        IEnumerable<T> GetAll<T>(int companyId);

        IEnumerable<T> GetAllForPerson<T>(string userId);

        IEnumerable<T> GetAllForDepartment<T>(int departmentId);

        Task CreateAsync(CreateVacationViewModel model);

        Task ChangeStatusAsync(int vacationId, int status);

        int GetCountOfUnproccessedByDepartment(int departmentId);

        int GetPersonalCount(string userId);

        int GetPersonalCountOfReject(string userId);

        int GetPersonalCountOfAccepted(string userId);
    }
}
