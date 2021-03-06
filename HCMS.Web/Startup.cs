namespace HCMS.Web
{
    using System.Reflection;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using HCMS.Data;
    using HCMS.Data.Models;
    using HCMS.Data.Common;
    using HCMS.Data.Seeding;
    using HCMS.Services.Data;
    using HCMS.Web.ViewModels;
    using HCMS.Data.Repository;
    using HCMS.Services.Mapping;
    using HCMS.Service.Messaging;
    using HCMS.Services.Data.Projects;
    using HCMS.Services.Data.Trainings;
    using HCMS.Services.Data.Employees;
    using HCMS.Data.Common.Repositories;
    using HCMS.Services.Data.Departments;
    using HCMS.Services.Data.Salary;
    using HCMS.Services.Data.Company;
    using HCMS.Service.Common;
    using HCMS.Services.Data.Tasks;
    using HCMS.Services.Data.Vocations;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<AppUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews(options =>
            {
                // XSRF prevent
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddRazorPagesOptions(opt => opt.Conventions.AddAreaPageRoute("Identity", "/Account/Login", ""));
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                // This says, that all pages need AUTHORIZATION. But when a controller, 
                // for example the login controller in Login.cshtml.cs, is tagged with
                // [AllowAnonymous] then it is not in need of AUTHORIZATION. :)
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            //Data repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(this.Configuration["SendGrid:ApiKey"]));

            //Data services
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ISalaryService, SalaryService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IEmployeService, EmployeService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IVacationsService, VacationsService>();
            services.AddTransient<IEvaluationService, EvaluationService>();
            services.AddTransient<IDepartmentService, DepartmentService>();

            //Common Services
            services.AddTransient<IYearsForEval, YearsForEval>();
            services.AddTransient<ICityCountry, CityCountry>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Seeding comand
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
