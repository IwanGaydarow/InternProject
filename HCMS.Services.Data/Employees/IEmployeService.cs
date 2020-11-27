namespace HCMS.Services.Data.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Administration.Employees;
    
    public interface IEmployeService
    {
        IEnumerable<T> GetAllEmployees<T>(int companyId);

        int GetCOunt(int companyId);

        T GetById<T>(string id);

        Task DeleteAsync(string employeeId);

        T EmployeeDetails<T>(string id);

        IEnumerable<T> EmployeesNotAssignToTask<T>(int companyId, int trainingId);

        Task UpdateAsync(DetailViewModel model);

        decimal EmployeesSalary(int companyId);
    }
}
