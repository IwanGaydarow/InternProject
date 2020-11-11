namespace HCMS.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using HCMS.Data.Models;

    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
    }
}
