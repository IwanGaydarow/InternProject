namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Trainings;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Trainings;
    using HCMS.Web.ViewModels.Administration.Evaluation;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class TrainingsController : Controller
    {
        private readonly ITrainingService trainingService;
        private readonly UserManager<AppUser> userManager;
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

            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainingViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            model.CompanyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            await this.trainingService.CreateAsync(model);

            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int trainingId)
        {
            var trainingToEdit = this.trainingService.GetById<TrainingViewModel>(trainingId);
            if (trainingToEdit == null)
            {
                return this.NotFound();
            }

            this.ViewData["trainingId"] = trainingId;

            return this.View(trainingToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTrainingViewModel model, int trainingId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.trainingService.EditAsync(model, trainingId);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> AddEmployee(int trainingId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);
            var users = this.employeService.EmployeesNotAssignToTraining<EmployeeSelectList>(companyId, trainingId);

            var model = new EmployeeToTrainingViewModel { Employees = users, TrainingId = trainingId };

            return this.PartialView("_AddEmployeePartial", model);
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

        public async Task<IActionResult> Delete(int trainingId)
        {
            await this.trainingService.DeleteAsync(trainingId);

            return this.Ok();
        }
    }
}