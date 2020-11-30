namespace HCMS.Web.ViewModels.Employee.Home
{
    public class EmployeeHomePageViewModel
    {
        public int PersonalVacationCreate { get; set; }

        public int PersonalAcceptVacation { get; set; }

        public int PersonalRejectVacation { get; set; }

        public int CountOfTasks { get; set; }

        public int CountOfFinishedTasks { get; set; }

        public int CountOfUnfinishedTasks { get; set; }

        public EmailFormViewModel EmailForm { get; set; }
    }
}
