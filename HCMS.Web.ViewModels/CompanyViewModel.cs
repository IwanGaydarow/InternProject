namespace HCMS.Web.ViewModels
{
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class CompanyViewModel :IMapFrom<Company>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FullAddres { get; set; }
    }
}
