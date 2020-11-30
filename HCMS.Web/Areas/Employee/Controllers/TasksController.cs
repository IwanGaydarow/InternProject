namespace HCMS.Web.Areas.Employee.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
   
    using HCMS.GlobalConstants;
    using Microsoft.AspNetCore.Identity;
    using HCMS.Data.Models;
    using HCMS.Services.Data.Tasks;
    using System.Threading.Tasks;
    using HCMS.Web.ViewModels.Employee;

    [Authorize(Roles = GlobalConstant.SystemEmployeeRole)]
    [Area("Employee")]
    public class TasksController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITasksService tasksService;

        public TasksController(UserManager<AppUser> userManager, ITasksService tasksService)
        {
            this.userManager = userManager;
            this.tasksService = tasksService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var tasks = this.tasksService.GetAllTaskByUserId<EmployeeTasksViewModel>(user.Id);

            var model = new AllEmployeeTasksViewModel { Tasks = tasks };
            return View(model);
        }
    }
}