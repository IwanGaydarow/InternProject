namespace HCMS.Services.Data.Projects
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Data.Common.Repositories;

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

        public IEnumerable<T> GetAllProjects<T>(int companyId)
        {
            return this.projectRepository.All()
                .Include(x => x.Department)
                .Where(x => x.Department.CompanyId == companyId)
                .To<T>().ToList();
        }
    }
}
