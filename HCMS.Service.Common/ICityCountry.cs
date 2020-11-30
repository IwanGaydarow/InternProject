namespace HCMS.Service.Common
{
    using System.Threading.Tasks;
    
    using HCMS.Web.ViewModels.Administration;

    public interface ICityCountry
    {
        Task<int> PrepareCityAndCountry(CreateCityViewModel cityModel, string countryName);
    }
}
