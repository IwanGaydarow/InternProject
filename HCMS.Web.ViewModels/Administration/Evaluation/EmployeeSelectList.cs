namespace HCMS.Web.ViewModels.Administration.Evaluation
{
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class EmployeeSelectList : IMapFrom<AppUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
