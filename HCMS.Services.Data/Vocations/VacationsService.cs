using HCMS.Data.Common.Repositories;
namespace HCMS.Services.Data.Vocations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
   
    using Microsoft.EntityFrameworkCore;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using HCMS.Web.ViewModels.Vocations;

    public class VacationsService : IVacationsService
    {
        private readonly IRepository<Vacations> vocationsRepository;

        public VacationsService(IRepository<Vacations> vocationsRepository)
        {
            this.vocationsRepository = vocationsRepository;
        }

        public async Task ChangeStatusAsync(int vacationId, int status)
        {
            var vacation = await this.vocationsRepository.GetByIdAsync(vacationId);
            if (vacation == null)
            {
                throw new NullReferenceException($"Vacation with id={vacationId} is not found.");
            }

            if (status == 0)
            {
                vacation.Status = null;
            }
            else if (status == 1)
            {
                vacation.Status = true;
            }
            else
            {
                vacation.Status = false;
            }

            this.vocationsRepository.Update(vacation);
            await this.vocationsRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateVacationViewModel model)
        {
            var vocation = new Vacations
            {
                Tittle = model.Tittle,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                UserId = model.UserId
            };

            await this.vocationsRepository.AddAsync(vocation);
            await this.vocationsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int companyId)
        {
            return this.vocationsRepository.All()
                .Include(x => x.User)
                .Where(x => x.User.Department.CompanyId == companyId)
                .To<T>().ToList();
        }

        public IEnumerable<T> GetAllForDepartment<T>(int departmentId)
        {
            return this.vocationsRepository.All()
                .Include(x => x.User)
                .Where(x => x.User.DepartmentId == departmentId)
                .To<T>().ToList();
        }

        public IEnumerable<T> GetAllForPerson<T>(string userId)
        {
            return this.vocationsRepository.All()
                .Where(x => x.UserId == userId)
                .To<T>().ToList();
        }

        public int GetCountOfUnproccessedByDepartment(int departmentId)
        {
            return this.vocationsRepository.All()
                .Where(x => x.User.DepartmentId == departmentId && x.Status == null)
                .ToList().Count();
                
        }

        public int GetPersonalCount(string userId)
        {
            return this.vocationsRepository.All()
                .Where(x => x.UserId == userId)
                .Count();
        }

        public int GetPersonalCountOfAccepted(string userId)
        {
            return this.vocationsRepository.All()
                .Where(x => x.UserId == userId && x.Status == true)
                .Count();
        }

        public int GetPersonalCountOfReject(string userId)
        {
            return this.vocationsRepository.All()
                .Where(x => x.UserId == userId && x.Status == false)
                .Count();
        }
    }
}
