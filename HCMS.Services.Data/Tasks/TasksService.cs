﻿using HCMS.Data.Common.Repositories;
using HCMS.Data.Models;
using HCMS.Services.Mapping;
using HCMS.Web.ViewModels.Manager.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMS.Services.Data.Tasks
{
    public class TasksService : ITasksService
    {
        private readonly IRepository<ProjectTasks> tasksRepository;
        private readonly IRepository<UsersTasks> userTasksRepository;

        public TasksService(IRepository<ProjectTasks> tasksRepository, IRepository<UsersTasks> userTasksRepository)
        {
            this.tasksRepository = tasksRepository;
            this.userTasksRepository = userTasksRepository;
        }

        public async Task CreateAsync(CreateTaskViewModel model)
        {
            var task = new ProjectTasks
            {
                Tittle = model.Tittle,
                Description = model.Description,
                EstimatedWorkHours = model.EstWorkHours,
                Status = model.Status,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                ProjectId = model.ProjectId
            };

            await this.tasksRepository.AddAsync(task);
            await this.tasksRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int taskId)
        {
            var task = this.tasksRepository.All()
                .Where(x => x.Id == taskId).FirstOrDefault();
            if (task == null)
            {
                throw new NullReferenceException($"Task for delte with id={taskId} is not found.");
            }

            task.IsDeleted = true;
            task.DeletedOn = DateTime.UtcNow;

            await this.DeleteUserTasksAsync(taskId);

            this.tasksRepository.Update(task);
            await this.tasksRepository.SaveChangesAsync();
        }

        private async Task DeleteUserTasksAsync(int taskId)
        {
            var tasksUser = this.userTasksRepository.All()
                .Where(x => x.ProjectTaskId == taskId)
                .ToList();

            if (tasksUser.Count > 0)
            {
                foreach (var taskUser in tasksUser)
                {
                    taskUser.IsDeleted = true;
                    taskUser.DeletedOn = DateTime.UtcNow;

                    this.userTasksRepository.Update(taskUser);
                    await this.userTasksRepository.SaveChangesAsync();
                }
            }
        }

        public IEnumerable<T> GetAllTasks<T>(int companyId)
        {
            return this.tasksRepository.All()
                .Include(x => x.Project)
                .Where(x => x.Project.Department.CompanyId == companyId)
                .To<T>().ToList();
        }

        public async Task<int> ChangeStatusAsync(int taskId)
        {
            var task = await this.tasksRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                throw new NullReferenceException($"Task with id={taskId} for changing status is not found.");
            }

            if (task.Status)
            {
                return 0;
            }

            task.Status = true;

            this.tasksRepository.Update(task);

            return this.tasksRepository.SaveChangesAsync().Result;
        }

        public ProjectTasks GetById(int taskId)
        {
            return this.tasksRepository.GetById(taskId);
        }

        public async Task UpdateAsync(EditTaskViewModel model)
        {
            var task = await this.tasksRepository.GetByIdAsync(model.Id);

            task.Tittle = model.Tittle;
            task.Description = model.Description;
            task.Status = model.Status;
            task.ModifiedOn = DateTime.UtcNow;

            this.tasksRepository.Update(task);
            await this.tasksRepository.SaveChangesAsync();
        }

        public async Task AddEmployeeToTask(int taskId, string userId)
        {
            var usersTask = new UsersTasks
            {
                ProjectTaskId = taskId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await this.userTasksRepository.AddAsync(usersTask);
            await this.userTasksRepository.SaveChangesAsync();
        }

        public string GetUserAssingToTask(int taskId)
        {
            return this.userTasksRepository.All()
                .Where(x => x.ProjectTaskId == taskId)
                .Select(x => x.User.Name)
                .FirstOrDefault();
        }
    }
}
