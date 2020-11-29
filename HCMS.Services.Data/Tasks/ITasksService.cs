namespace HCMS.Services.Data.Tasks
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using HCMS.Web.ViewModels.Manager.Tasks;
    using HCMS.Data.Models;

    public interface ITasksService
    {
        Task CreateAsync(CreateTaskViewModel model);

        Task AddEmployeeToTask(int taskId, string userId);

        IEnumerable<T> GetAllTasks<T>(int companyId, int departmentId);

        ProjectTasks GetById(int taskId);

        Task DeleteAsync(int taskId);

        Task UpdateAsync(EditTaskViewModel model);

        Task<int> ChangeStatusAsync(int taskId);

        string GetUserAssingToTask(int taskId);

        int CountOfUnfinishedTaskByDepartment(int departmentId);
    }
}
