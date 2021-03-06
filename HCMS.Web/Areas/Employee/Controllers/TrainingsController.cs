﻿namespace HCMS.Web.Areas.Employee.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Trainings;
    using HCMS.Web.ViewModels.Employee;

    [Authorize(Roles = GlobalConstant.SystemEmployeeRole)]
    [Area("Employee")]
    public class TrainingsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITrainingService trainingService;

        public TrainingsController(UserManager<AppUser> userManager, ITrainingService trainingService)
        {
            this.userManager = userManager;
            this.trainingService = trainingService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var trainings = this.trainingService.GetAllAssignedToEmployee<EmployeeTrainingViewModel>(user.Id);

            var model = new AllEmployeeTrainingsViewModel { Trainings = trainings };

            return View(model);
        }
    }
}