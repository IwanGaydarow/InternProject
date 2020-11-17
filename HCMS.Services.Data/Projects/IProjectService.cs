﻿namespace HCMS.Services.Data.Projects
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using HCMS.Web.ViewModels.Administration.Projects;
    using HCMS.Data.Models;

    public interface IProjectService
    {
        IEnumerable<T> GetAllProjects<T>(int companyId);

        Project GetProjectById(int projectId);

        Task CreateAsync(string tittle, string description, int estWorkH, int departmentId);

        Task UpdateAsync(UpdateViewModel model);

        Task<int> ChangeStatus(int projectId);

        int GetProjectDepartmentId(int projectId);
    }
}
