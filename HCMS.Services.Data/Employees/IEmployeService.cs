namespace HCMS.Services.Data.Employees
{
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Administration.Employees;
    
    public interface IEmployeService
    {
        Task CreateAsync(CreateViewModel model);
    }
}
