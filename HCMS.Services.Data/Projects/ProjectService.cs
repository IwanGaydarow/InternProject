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
    using HCMS.Web.ViewModels.Administration.Projects;

    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> projectRepository;

        public ProjectService(IRepository<Project> projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<int> ChangeStatus(int projectId)
        {
            var project = await this.projectRepository.GetByIdAsync(projectId);

            if (project == null)
            {
                throw new NullReferenceException(nameof(project));
            }

            if (project.Status)
            {
                return 0;
            }

            project.Status = true;

            this.projectRepository.Update(project);
            var result = this.projectRepository.SaveChangesAsync().Result;
            return result;
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
            await this.projectRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllProjects<T>(int companyId)
        {
            return this.projectRepository.All()
                .Include(x => x.Department)
                .Where(x => x.Department.CompanyId == companyId)
                .To<T>().ToList();
        }

        public Project GetProjectById(int projectId)
        {
            return this.projectRepository.GetById(projectId);
        }

        public async Task UpdateAsync(UpdateViewModel model)
        {
            var projectToEdit = await this.projectRepository.GetByIdAsync(model.ProjectId);
            if (projectToEdit == null)
            {
                throw new NullReferenceException(nameof(projectToEdit));
            }

            projectToEdit.Tittle = model.Tittle;
            projectToEdit.Description = model.Description;
            projectToEdit.EstimatedWorkHours = model.EstimatedWorkHours;
            projectToEdit.ModifiedOn = DateTime.UtcNow;
            projectToEdit.DepartmentId = model.DepartmentId;

            this.projectRepository.Update(projectToEdit);
            await this.projectRepository.SaveChangesAsync();
        }

        public int GetProjectDepartmentId(int projectId)
        {
            var project = this.projectRepository.GetById(projectId);

            return project.DepartmentId;
        }

        public async Task DeleteAsync(int projectId)
        {
            var projectToDelete = await this.projectRepository.GetByIdAsync(projectId);

            if (projectToDelete == null)
            {
                throw new NullReferenceException(nameof(projectToDelete));
            }

            projectToDelete.IsDeleted = true;
            projectToDelete.DeletedOn = DateTime.UtcNow;

            this.projectRepository.Update(projectToDelete);
            await this.projectRepository.SaveChangesAsync();
        }
    }
}
