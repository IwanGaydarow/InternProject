namespace HCMS.Services.Data.Employees
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Administration.Employees;
    
    public interface IEmployeService
    {
        IEnumerable<T> GetAllEmployees<T>(int companyId);

        T GetById<T>(string id);

        Task DeleteAsync(string employeeId);

        T Employeedetails<T>(string id);

        Task UpdateAsync(DetailViewModel model);
    }
}
