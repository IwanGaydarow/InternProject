namespace HCMS.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using HCMS.Web.ViewModels.Administration.Evaluation;

    public interface IEvaluationService
    {
        bool ChekEvalInYear(int year);

        Task CreateAsync(CreateEvaluationViewModel model);

        IEnumerable<EvalsViewModel> GetAll(int companyId);

        Task DeleteAsync(int id);
    }
}
