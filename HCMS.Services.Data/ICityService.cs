namespace HCMS.Services.Data
{
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Administration;

    public interface ICityService
    {
        int CheckIfCityExist(string cityName, int countryId);

        Task<int> CreateCityAsync(CreateCityViewModel model, int countryId);
    }
}
