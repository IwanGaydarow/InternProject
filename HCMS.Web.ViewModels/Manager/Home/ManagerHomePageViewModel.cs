namespace HCMS.Web.ViewModels.Manager.Home
{
    public class ManagerHomePageViewModel
    {
        public int EmployeeCount { get; set; }

        public int UnfinishedProjectCount { get; set; }

        public int VacationForProcessing { get; set; }

        public int CountOfUnfinishedTasks { get; set; }

        public int CountOfTrainings { get; set; }

        public int PersonalVacationCreate { get; set; }

        public int PersonalAcceptVacation { get; set; }

        public int PersonalRejectVacation { get; set; }

        public EmailFormViewModel EmailForm { get; set; }
    }
}
