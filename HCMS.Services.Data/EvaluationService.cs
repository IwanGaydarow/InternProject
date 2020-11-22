namespace HCMS.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using Microsoft.EntityFrameworkCore;
    
    using HCMS.Data.Models;
    using HCMS.Data.Common.Repositories;
    using HCMS.Web.ViewModels.Administration.Evaluation;

    public class EvaluationService : IEvaluationService
    {
        private readonly IRepository<Evaluations> evaluationRepository;
        private readonly IRepository<AppUser> employeeRepository;

        public EvaluationService(IRepository<Evaluations> evaluationRepository,
            IRepository<AppUser> employeeRepository)
        {
            this.evaluationRepository = evaluationRepository;
            this.employeeRepository = employeeRepository;
        }

        public bool ChekEvalInYear(int year)
        {
            return this.evaluationRepository.All()
                            .Any(x => x.CreatedOn.Year == year);
        }

        public async Task CreateAsync(CreateEvaluationViewModel model)
        {
            var eval = new Evaluations
            {
                Value = model.Value,
                Notes = model.Notes,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                UserId = model.EmployeeId
            };

            await this.evaluationRepository.AddAsync(eval);
            await this.evaluationRepository.SaveChangesAsync();
        }

        public IEnumerable<EvalsViewModel> GetAll(int companyId)
        {
            return this.evaluationRepository.All()
                .Where(x => x.User.Department.CompanyId == companyId)
                .Include(x => x.User)
                .Include(x => x.User.Department)
                .Select(x => new EvalsViewModel
                {
                    EmployeeId = x.UserId,
                    EmployeeName = x.User.Name,
                    Percentage = x.Value,
                    EvalYear = x.CreatedOn.Year,
                    Notes = x.Notes,
                    EmployeeEmail = x.User.Email,
                    JobTittle = x.User.JobTittle,
                    EmployeeDepartment = x.User.Department.Tittle
                }).ToList();
        }
    }
}
