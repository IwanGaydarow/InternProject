namespace HCMS.Services.Data.Salary
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using HCMS.Web.ViewModels.Administration.Salary;

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
