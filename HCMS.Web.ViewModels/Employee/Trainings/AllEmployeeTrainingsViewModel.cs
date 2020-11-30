namespace HCMS.Web.ViewModels.Employee
{
    using System.Collections.Generic;

    public class AllEmployeeTrainingsViewModel
    {
        public IEnumerable<EmployeeTrainingViewModel> Trainings { get; set; }
    }
}
