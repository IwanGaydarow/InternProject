namespace HCMS.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using HCMS.Data.Models;
    using HCMS.Service.Common;
    using HCMS.Web.ViewModels;
    using HCMS.GlobalConstants;
    using HCMS.Services.Data.Company;
    using HCMS.Web.ViewModels.Administration;
    using HCMS.Services.Data.Departments;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICityCountry cityCountryService;
        private readonly ICompanyService companyService;
        private readonly IDepartmentService departmentService;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ICityCountry cityCountryService,
            ICompanyService companyService,
            IDepartmentService departmentService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.cityCountryService = cityCountryService;
            this.companyService = companyService;
            this.departmentService = departmentService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Required]
            public string Adress { get; set; }

            [Required]
            [StringLength(10, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 4)]
            public string Gender { get; set; }

            [Required]
            [StringLength(25, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
            public string JobTittle { get; set; }

            [Required]
            public CreateCityViewModel City { get; set; }

            [Required]
            [Display(Name = "Country")]
            [StringLength(50, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 5)]
            public string CountryName { get; set; }

            [Required]
            public CreateCompanyViewModel Company { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = GlobalConstant.MinPasswordLenght)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = GlobalConstant.ConfirmPasswordNotMatch)]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var cityId = await this.cityCountryService.PrepareCityAndCountry(Input.City, Input.CountryName);
                await this.companyService.Create(Input.Company);
                var companyId = this.companyService.GetIdByName(Input.Company.Name);
                var id = await this.departmentService.CreateAsyncId("Leader", companyId, cityId);

                var user = new AppUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    Address = Input.Adress,
                    Gender = Input.Gender,
                    JobTittle = Input.JobTittle,
                    DepartmentId = id,
                    CityId = cityId,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };
                    
                var result = await this._userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(user, GlobalConstant.SystemAdministratorRole);

                    await this.departmentService.AddManagerAsync(user.Id, id);

                    return RedirectToPage("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();

        }
    }
}
