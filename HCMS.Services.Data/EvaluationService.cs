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

        public EvaluationService(IRepository<Evaluations> evaluationRepository)
        {
            this.evaluationRepository = evaluationRepository;
        }

        public bool ChekEvalInYear(int year)
        {
            return this.evaluationRepository.All()
                            .Any(x => x.EvaluationYear == year);
        }

        public async Task CreateAsync(CreateEvaluationViewModel model)
        {
            var eval = new Evaluations
            {
                Value = model.Value,
                Notes = model.Notes,
                CreatedOn = DateTime.UtcNow,
                EvaluationYear = model.Year,
                IsDeleted = false,
                UserId = model.EmployeeId
            };

            await this.evaluationRepository.AddAsync(eval);
            await this.evaluationRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var eval = await this.evaluationRepository.GetByIdAsync(id);
            if (eval == null)
            {
                throw new NullReferenceException("Evaluation for delete is not found");
            }

            eval.IsDeleted = true;
            eval.DeletedOn = DateTime.UtcNow;

            this.evaluationRepository.Update(eval);
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
                    Id = x.Id,
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
