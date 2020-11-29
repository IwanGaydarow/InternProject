namespace HCMS.Web.Areas.Employee.Controllers
{
    using HCMS.GlobalConstants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstant.SystemEmployeeRole)]
    [Area("Employee")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}