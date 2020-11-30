namespace HCMS.Services.Data.Departments
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using HCMS.Data.Models;
    using HCMS.Web.ViewModels.Administration.Departments;

    public interface IDepartmentService
    {
        IEnumerable<DepartmentViewModel> GetAllDepartments(int companyId);

        Task CreateAsync(string tittle, int companyId, int cityId, string menager = null);

        Task<int> CreateAsyncId(string tittle, int companyId, int cityId, string menager = null);

        Task Delete(int departmentId);

        int GetIdByName(string name);

        T GetDepartmentById<T>(int departmentId);

        Department GetByIdWithCompany(int departmentId);

        int GetCompanyIdByDepartmentId(int? departmentId);

        T GetDepartmentForManager<T>(string managerId);

        bool CheckIfDepartmentExist(string tittle, int companyId);

        Task Update(int departmentId, string tittle);

        IEnumerable<T> GetDepartmentsForSelectList<T>(int? departmentId);

        Task AddManagerAsync(string userId, int departmentId);

        int GetCount(int companyId);
    }
}
