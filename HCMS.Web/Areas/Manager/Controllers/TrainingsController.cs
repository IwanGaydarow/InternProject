namespace HCMS.Web.Areas.Manager.Controllers
{
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Departments;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Trainings;
    using HCMS.Web.ViewModels.Administration.Evaluation;
    using HCMS.Web.ViewModels.Administration.Trainings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class TrainingsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITrainingService trainingService;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeService employeService;

        public TrainingsController(ITrainingService trainingService, UserManager<AppUser> userManager,
            IDepartmentService departmentService, IEmployeService employeService)
        {
            this.trainingService = trainingService;
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.employeService = employeService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var trainings = this.trainingService.GetAll<TrainingViewModel>(companyId);
            var model = new AllTrainingsViewModel { Trainings = trainings };

            return View("/Areas/Administration/Views/Trainings/Index.cshtml", model);
        }

        public async Task<IActionResult> AddEmployee(int trainingId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            var users = this.employeService.EmployeesNotAssignToTrainingManager<EmployeeSelectList>(companyId, trainingId, user.DepartmentId.Value);

            var model = new EmployeeToTrainingViewModel { Employees = users, TrainingId = trainingId };

            return this.PartialView("/Areas/Administration/Views/Trainings/_AddEmployeePartial.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeToTrainingViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                if (model.EmployeeId == null)
                {
                    return await this.AddEmployee(model.TrainingId);
                }

                var employees = new List<EmployeeSelectList>();
                var employee = this.employeService
                                            .GetById<EmployeeSelectList>(model.EmployeeId);
                employees.Add(employee);
                model.Employees = employees;

                return this.PartialView("_AddEmployeePartial", model);
            }

            await this.trainingService.AddEmployeeToTraining(model.EmployeeId, model.TrainingId);

            return this.RedirectToAction("Index");
        }
    }
}