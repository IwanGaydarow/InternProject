namespace HCMS.Services.Data.Company
{
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels;

    public interface ICompanyService
    {
        Task Create(CreateCompanyViewModel model);

        T GetInfo<T>(int companyId);

        int GetIdByName(string name);
    }
}
