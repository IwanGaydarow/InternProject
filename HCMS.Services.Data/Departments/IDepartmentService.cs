using HCMS.Data.Models;
using HCMS.Web.ViewModels.Administration.Departments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMS.Services.Data.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentViewModel> GetAllDepartments(int companyId);

        Task CreateAsync(string tittle, int companyId, string menager = null);

        Task Delete(int departmentId);

        T GetDepartmentById<T>(int departmentId);

        Department GetByIdWithCompany(int departmentId);

        int GetCompanyIdByDepartmentId(int? departmentId);

        bool CheckIfDepartmentExist(string tittle);

        Task Update(int departmentId, string tittle);

        IEnumerable<T> GetDepartmentsForSelectList<T>(int? departmentId);

        Task AddManagerAsync(string userId, int departmentId);

        int GetCount(int companyId);
    }
}
