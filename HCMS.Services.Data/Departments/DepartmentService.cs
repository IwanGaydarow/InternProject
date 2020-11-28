using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCMS.Data.Common.Repositories;
using HCMS.Data.Models;
using HCMS.Services.Mapping;
using HCMS.Web.ViewModels.Administration.Departments;
using Microsoft.EntityFrameworkCore;

namespace HCMS.Services.Data.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> departmentRepository;
        private readonly IRepository<AppUser> employeeRepository;

        public DepartmentService(IRepository<Department> departmentRepository,
            IRepository<AppUser> employeeRepository)
        {
            this.departmentRepository = departmentRepository;
            this.employeeRepository = employeeRepository;
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

        public IEnumerable<DepartmentViewModel> GetAllDepartments(int companyId)
        {
            var result = this.departmentRepository.All()
                .Include(x => x.DepartmentManagerNavigation)
                .Include(x => x.Company)
                .Where(x => x.CompanyId == companyId)
                .Select(x => new DepartmentViewModel
                {
                    Id = x.Id,
                    Tittle = x.Tittle,
                    ManagerName = x.DepartmentManagerNavigation.Name,
                    CompanyName = x.Company.Name,
                    Employees = x.Employess.Count()
                });

               return result.ToList();
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

        public IEnumerable<T> GetDepartmentsForSelectList<T>(int? departmentId)
        {
            var companyId = this.GetCompanyIdByDepartmentId(departmentId);
            var departments = this.departmentRepository.All()
                .Where(x => x.CompanyId == companyId);

            return departments.To<T>().ToList();
        }

        public int GetCount(int companyId)
        {
            return this.departmentRepository.All()
                .Where(x => x.CompanyId == companyId)
                .Count();
        }

        public async Task AddManagerAsync(string userId, int departmentId)
        {
            var department = await this.departmentRepository.GetByIdAsync(departmentId);

            department.DepartmentManager = userId;

            this.departmentRepository.Update(department);
            await this.departmentRepository.SaveChangesAsync();
        }
    }
}
