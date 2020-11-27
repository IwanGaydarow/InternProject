using HCMS.Web.ViewModels.Administration.Salary;
using System.Threading.Tasks;

namespace HCMS.Services.Data.Salary
{
    public interface ISalaryService
    {
        T GetSalary<T>(string employeeId);

        Task CreateAsync(CreateSalaryViewModel model);

        Task ReplaceExistingSalaryAsync(CreateSalaryViewModel model);

        bool CheckForSalary(string employeeId);
    }
}
