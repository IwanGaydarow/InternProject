namespace HCMS.Web.ViewModels.Administration.Trainings
{
    using System;
    
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;

    public class TrainingViewModel : IMapFrom<Trainings>
    {
        public string Tittle { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TrainingsHours { get; set; }
    }
}
