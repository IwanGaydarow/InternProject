﻿namespace HCMS.Services.Data
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using HCMS.Web.ViewModels.Administration.Evaluation;

    public interface IEvaluationService
    {
        bool ChekEvalInYear(int year, string employeeId);

        Task CreateAsync(CreateEvaluationViewModel model);

        Task UpdateAsync(EditEvalViewModel model);

        IEnumerable<EvalsViewModel> GetAll(int companyId);

        IEnumerable<T> GetAllByUserId<T>(string id);

        IEnumerable<EvalsViewModel> GetAllForManager(int companyId, int departmentId);
        
        T GetById<T>(int evalId);

        Task DeleteAsync(int id);
    }
}
