namespace HCMS.Services.Data.Company
{
    public interface ICompanyService
    {
        T GetInfo<T>(int companyId);
    }
}
