namespace HCMS.Web.ViewModels.Employee
{
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class EmployeeEvalsViewModel : IMapFrom<Evaluations>
    {
        public string Value { get; set; }

        public string Notes { get; set; }

        public int EvaluationYear { get; set; }
    }
}
