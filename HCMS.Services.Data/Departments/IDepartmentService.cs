using HCMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMS.Services.Data.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<T> GetAllDepartments<T>(int companyId);

        Task CreateAsync(string tittle, int companyId, string menager = null);

        Task Delete(int departmentId);

        T GetDepartmentById<T>(int departmentId);

        Department GetByIdWithCompany(int departmentId);

        int GetCompanyIdByDepartmentId(int? departmentId);

        bool CheckIfDepartmentExist(string tittle);

        Task Update(int departmentId, string tittle);
    }
}
