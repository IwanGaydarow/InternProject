namespace HCMS.Web.ViewModels.Administration.Home
{
    public class HomePageViewModel
    {
        public CompanyViewModel Company { get; set;}

        public int CountOfDepartments { get; set; }

        public int EmployeeCount { get; set; }

        public int ProjectCount { get; set; }

        public int CountOfFinishProject { get; set; }

        public int CountOfProcessingProject { get; set; }

        public int CountOfTrainings { get; set; }
    }
}
