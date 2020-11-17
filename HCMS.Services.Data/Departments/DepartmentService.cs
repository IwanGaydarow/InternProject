using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using HCMS.Data.Common.Repositories;
using HCMS.Data.Models;
using HCMS.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace HCMS.Services.Data.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> departmentRepository;

        public DepartmentService(IRepository<Department> departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task CreateAsync(string tittle, int companyId, string menager = null)
        {
            var department = new Department
            {
                Tittle = tittle,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                DepartmentManager = menager,
                CompanyId = companyId
            };

            await this.departmentRepository.AddAsync(department);
            await this.departmentRepository.SaveChangesAsync();
        }

        public async Task Delete(int departmentId)
        {
            var department = await this.departmentRepository.GetByIdAsync(departmentId);

            if(department == null)
            {
                throw new NullReferenceException(nameof(department));
            }

            this.departmentRepository.Delete(department);
            await this.departmentRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllDepartments<T>(int companyId)
        {
            return this.departmentRepository.All()
                .Include(x => x.Company)
                .Where(x => x.CompanyId == companyId)
                .To<T>().ToList();
        }

        public T GetDepartmentById<T>(int departmentId)
        {
            return this.departmentRepository.All()
                .Include(x => x.DepartmentManagerNavigation)
                .Where(x => x.Id == departmentId)
                .To<T>().FirstOrDefault();
        }

        public Department GetByIdWithCompany(int departmentId)
        {
            return this.departmentRepository.All()
                .Include(x => x.Company)
                .Where(x => x.Id == departmentId)
                .FirstOrDefault();
        }

        public int GetCompanyIdByDepartmentId(int? departmentId)
        {
            if(departmentId == null)
            {
                throw new ArgumentNullException(nameof(departmentId));
            }

            return this.departmentRepository.All()
                    .Where(x => x.Id == departmentId)
                    .Select(x => x.CompanyId).FirstOrDefault();
        }

        public  bool CheckIfDepartmentExist(string tittle)
        {
            return this.departmentRepository.AllWithDeleted().Any(x => x.Tittle == tittle);
        }

        public async Task Update(int departmentId, string tittle)
        {
            var departmentToEdit = await this.departmentRepository.GetByIdAsync(departmentId);

            if (departmentToEdit == null)
            {
                throw new NullReferenceException();
            }

            departmentToEdit.Tittle = tittle;
            departmentToEdit.ModifiedOn = DateTime.UtcNow;

            this.departmentRepository.Update(departmentToEdit);
            await this.departmentRepository.SaveChangesAsync();
        }
    }
}
