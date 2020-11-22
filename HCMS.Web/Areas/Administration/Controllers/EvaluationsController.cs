namespace HCMS.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Evaluation;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class EvaluationsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEmployeService employeService;
        private readonly IDepartmentService departmentService;
        private readonly IEvaluationService evaluationService;

        public EvaluationsController(UserManager<AppUser> userManager,
            IEmployeService employeService, IDepartmentService departmentService,
            IEvaluationService evaluationService)
        {
            this.userManager = userManager;
            this.employeService = employeService;
            this.departmentService = departmentService;
            this.evaluationService = evaluationService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var evals = this.evaluationService.GetAll(companyId);

            var model = new AllEvalsViewModel { Evaluations = evals };
            return View(model);
        }

        public async Task<IActionResult> Create(string employeeId = null)
        {
            var model = new CreateEvaluationViewModel();

            var years = this.GetYearsForEval();

            if (employeeId != null)
            {
                var employee = this.employeService.GetById<EmployeeSelectList>(employeeId);
                if (employee == null)
                {
                    return this.NotFound();
                }

                model.EmployeeId = employee.Id;
                model.Name = employee.Name;
            }
            else
            {
                var user = await this.userManager.GetUserAsync(this.User);
                var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

                model.Employees = this.employeService.GetAllEmployees<EmployeeSelectList>(companyId);
            }
            model.Years = years;

            return this.View(model);
        }

        /// <summary>
        /// Return list of last 5 years.
        /// </summary>
        /// <returns></returns>
        private List<int> GetYearsForEval()
        {
            var years = new List<int>();
            var curYear = DateTime.UtcNow.Year;
            for (int i = curYear - 5; i <= curYear; i++)
            {
                years.Add(i);
            }

            return years;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEvaluationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var isEvaluated = this.evaluationService.ChekEvalInYear(model.Year);

            if (isEvaluated)
            {
                this.ModelState.AddModelError(string.Empty, $"Evaluation for {model.Year} alredy exist. If you want to change it, view all Evaluations.");

                var employee = this.employeService.GetById<EmployeeSelectList>(model.EmployeeId);
                model.Name = employee.Name;
               
                var years = this.GetYearsForEval();
                model.Years = years;
                
                return this.View(model);
            }

            await this.evaluationService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int evaluationId)
        {
            await this.evaluationService.DeleteAsync(evaluationId);

            return this.Ok();
        }
    }
}