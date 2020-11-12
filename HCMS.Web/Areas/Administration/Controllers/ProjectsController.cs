namespace HCMS.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int model)
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int model)
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}