namespace HCMS.Web.ViewModels.Vocations
{
    using HCMS.Services.Mapping;
    using HCMS.Data.Models;
    using System;

    public class VacationsViewModel : IMapFrom<Vacations>
    {
        public string Tittle { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool? Status { get; set; }
    }
}
