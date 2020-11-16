using System.Threading.Tasks;

namespace HCMS.Services.Data.Projects
{
    public interface IProjectService
    {
        T GetAllProjects<T>();

        Task CreateAsync(string tittle, string description, int estWorkH, int departmentId);
    }
}
