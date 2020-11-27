﻿namespace HCMS.Services.Data.Employees
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
   
    using Microsoft.EntityFrameworkCore;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Data.Common.Repositories;
    using HCMS.Web.ViewModels.Administration.Employees;

    public class EmployeService : IEmployeService
    {
        private readonly IRepository<AppUser> employeeRepository;

        public EmployeService(IRepository<AppUser> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task DeleteAsync(string employeeId)
        {
            var employee = await this.employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new NullReferenceException("Employee for delete is not found.");
            }

            employee.IsDeleted = true;
            employee.DeletedOn = DateTime.UtcNow;

            this.employeeRepository.Update(employee);
            await this.employeeRepository.SaveChangesAsync();
        }

        public T Employeedetails<T>(string id)
        {
            return this.employeeRepository.All()
                .Where(x => x.Id == id)
                .Include(x => x.City)
                .Include(x => x.Department).To<T>().First();
        }

        public IEnumerable<T> GetAllEmployees<T>(int companyId)
        {
            return this.employeeRepository.All()
                .Include(x => x.Department)
                .Where(x => x.Department.CompanyId == companyId)
                .To<T>().ToList();
        }

        public IEnumerable<T> EmployeesNotAssignToTask<T>(int companyId, int trainingId)
        {
            return this.employeeRepository.All()
                .Where(x => x.Department.CompanyId == companyId &&
                        x.TrainingsUsers.All(y => y.TrainingId != trainingId))
                .To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            return this.employeeRepository.All()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
        }

        public async Task UpdateAsync(DetailViewModel model)
        {
            var employee = await this.employeeRepository.GetByIdAsync(model.Id);

            if (employee == null)
            {
                throw new NullReferenceException("Emploee for update, is not found.");
            }

            employee.Name = model.EmployeeName;
            employee.JobTittle = model.JobTittle;
            employee.Email = model.Email;
            employee.PhoneNumber = model.Phone;
            employee.Gender = model.Gender;
            employee.ModifiedOn = DateTime.UtcNow;

            this.employeeRepository.Update(employee);
            await this.employeeRepository.SaveChangesAsync();
        }
    }
}
