namespace HCMS.Services.Data.Projects
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IProjectService
    {
        IEnumerable<T> GetAllProjects<T>(int companyId);

        Task CreateAsync(string tittle, string description, int estWorkH, int departmentId);
    }
}
