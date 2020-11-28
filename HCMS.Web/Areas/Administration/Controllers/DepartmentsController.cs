namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Departments;
    using HCMS.Web.ViewModels.Administration.Evaluation;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class DepartmentsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeService employeService;

        public DepartmentsController(UserManager<AppUser> userManager ,IDepartmentService departmentService,
            IEmployeService employeService)
        {
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.employeService = employeService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);
            var departments = this.departmentService.GetAllDepartments(companyId);

            var model = new DepartmentsViewModel { Departments = departments };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (this.departmentService.CheckIfDepartmentExist(model.Tittle))
            {
                this.ModelState.AddModelError(string.Empty, GlobalConstant.DepartmentExistErrorMsg);

                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);

            await this.departmentService.CreateAsync(model.Tittle, companyId);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> AddManager(int departmentId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);
            var employees = this.employeService.GetAllEmployees<EmployeeSelectList>(companyId);

            var model = new ManagerToDepartmentViewModel { DepartmentId = departmentId, Employees = employees };
            
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddManager(ManagerToDepartmentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByIdAsync(model.UserId);
            var role = await this.userManager.GetRolesAsync(user);
            if (role[0] != GlobalConstant.SystemManagerRole)
            {
                await this.userManager.RemoveFromRoleAsync(user, role[0]);
                await this.userManager.AddToRoleAsync(user, GlobalConstant.SystemManagerRole);
            }

            await this.departmentService.AddManagerAsync(model.UserId, model.DepartmentId);
            await this.employeService.ChangeDepartmentAsync(model.UserId, model.DepartmentId);

            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int departmentId)
        {
            var model = this.departmentService.GetDepartmentById<EditViewModel>(departmentId);

            return this.PartialView("_EditPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_EditPartial", model);
            }

            await this.departmentService.Update(model.Id, model.Tittle);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int departmentId)
        {
            await this.departmentService.Delete(departmentId);

           return this.Ok();
        }
    }
}