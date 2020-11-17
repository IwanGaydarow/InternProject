namespace HCMS.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Administration.Departments;

    [Authorize(Roles = GlobalConstant.SystemAdministratorRole)]
    [Area("Administration")]
    public class DepartmentsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;

        public DepartmentsController(UserManager<AppUser> userManager ,IDepartmentService departmentService)
        {
            this.userManager = userManager;
            this.departmentService = departmentService;
        }


        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var companyId = this.departmentService.GetCompanyIdByDepartmentId(user.DepartmentId);
            var departments = this.departmentService.GetAllDepartments<DepartmentViewModel>(companyId);

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

            //TODO: check if manager exist if no error when implement Employees

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