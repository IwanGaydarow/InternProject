using HCMS.Data.Common.Repositories;
using HCMS.Data.Models;
using System;
using System.Threading.Tasks;

namespace HCMS.Services.Data.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> projectRepository;

        public ProjectService(IRepository<Project> projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task CreateAsync(string tittle, string description, int estWorkH, int departmentId)
        {
            var project = new Project
            {
                Tittle = tittle,
                Description = description,
                EstimatedWorkHours = estWorkH,
                Status = false,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                DepartmentId = departmentId
            };

            await this.projectRepository.AddAsync(project);
            await this.projectRepository.SaveChangesAsnyc();
        }

        public T GetAllProjects<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
