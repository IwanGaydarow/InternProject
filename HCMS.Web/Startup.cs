namespace HCMS.Web
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using HCMS.Data.Models;
    using HCMS.Data.Repository;
    using HCMS.Service.Messaging;
    using HCMS.Data;
    using HCMS.Data.Seeding;
    using HCMS.Data.Common.Repositories;
    using HCMS.Services.Data.Departments;
    using HCMS.Services.Mapping;
    using HCMS.Web.Models;
    using System.Reflection;

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
                options/*.UseLazyLoadingProxies()*/.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

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

            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(this.Configuration["SendGrid:ApiKey"]));


            services.AddTransient<IDepartmentService, DepartmentService>();
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
