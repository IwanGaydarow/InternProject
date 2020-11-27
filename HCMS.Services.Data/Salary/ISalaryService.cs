using HCMS.Web.ViewModels.Administration.Salary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMS.Services.Data.Salary
{
    public interface ISalaryService
    {
        T GetSalary<T>(string employeeId);

        Task CreateAsync(CreateSalaryViewModel model);

        Task ReplaceExistingSalaryAsync(CreateSalaryViewModel model);

        IEnumerable<T> GetSalaries<T>(int companyId);

        bool CheckForSalary(string employeeId);

        Task DeleteAsync(int salaryId);
    }
}
