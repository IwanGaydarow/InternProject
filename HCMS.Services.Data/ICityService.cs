using HCMS.Web.ViewModels.Administration;
using System.Threading.Tasks;

namespace HCMS.Services.Data
{
    public interface ICityService
    {
        int CheckIfCityExist(string cityName, int countryId);

        Task<int> CreateCityAsync(CreateCityViewModel model, int countryId);
    }
}
