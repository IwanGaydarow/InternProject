namespace HCMS.Services.Data
{
    using System.Threading.Tasks;

    public interface ICountryService
    {
        Task<int> CreateCountryAsync(string countryName);

        int CheckCountryExist(string countryName);
    }
}
