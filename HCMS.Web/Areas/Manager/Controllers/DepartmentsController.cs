namespace HCMS.Web.Areas.Manager.Controllers
{
    using System.Threading.Tasks;
   
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Employees;
    using HCMS.Services.Data.Departments;
    using HCMS.Web.ViewModels.Manager.Departments;
    using HCMS.Web.ViewModels.Administration.Departments;
    

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class DepartmentsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeService employeService;

        public DepartmentsController(UserManager<AppUser> userManager, IDepartmentService departmentService,
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

            return View("/Areas/Administration/Views/Departments/Index.cshtml", model);
        }

        public async Task<IActionResult> MyDepartment()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = this.departmentService.GetDepartmentForManager<DepartmentInfoViewModel>(user.Id);

            return this.View(model);
        }

        public IActionResult Details(string employeeId)
        {
            var model = this.employeService.EmployeeDetails<EmployeeInfoManagerViewModel>(employeeId);

            return this.PartialView("_DetailsPartial", model);
        }
    }
}