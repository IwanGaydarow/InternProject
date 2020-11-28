namespace HCMS.Services.Data.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Administration.Employees;
    using HCMS.Web.ViewModels.Administration.Home;

    public interface IEmployeService
    {
        IEnumerable<T> GetAllEmployees<T>(int companyId);

        int GetCOunt(int companyId);

        T GetById<T>(string id);

        Task DeleteAsync(string employeeId);

        T EmployeeDetails<T>(string id);

        IEnumerable<T> EmployeesNotAssignToTask<T>(int companyId, int trainingId);

        IEnumerable<T> EmployeesNotAssignToTaskManager<T>(int companyId, int trainingId, int departmentId);

        Task UpdateAsync(DetailViewModel model);

        Task ChangeDepartmentAsync(string userId, int departmentId);
    }
}
