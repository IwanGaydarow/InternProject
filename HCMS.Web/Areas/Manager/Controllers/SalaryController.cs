namespace HCMS.Web.Areas.Manager.Controllers
{
    using HCMS.GlobalConstants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstant.SystemManagerRole)]
    [Area("Manager")]
    public class SalaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}