namespace HCMS.Services.Data.Projects
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using HCMS.Web.ViewModels.Administration.Projects;
    using HCMS.Data.Models;

    public interface IProjectService
    {
        IEnumerable<T> GetAllProjects<T>(int companyId);

        IEnumerable<T> GetAllProjectsByDepartment<T>(int companyId, int departmentId);

        Project GetProjectById(int projectId);

        T GetProjectById<T>(int projectId);

        Task CreateAsync(string tittle, string description, int estWorkH, int departmentId);

        Task UpdateAsync(UpdateViewModel model);

        Task<int> ChangeStatusAsync(int projectId);

        int GetProjectDepartmentId(int projectId);

        Task DeleteAsync(int projectId);

        int GetCount(int companyId);

        int GetCountOfFinished(int companyId);

        int GetCountOfProcessing(int companyId);

        int GetCountByDepartment(int departmentId);
    }
}
