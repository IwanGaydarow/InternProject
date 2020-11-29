namespace HCMS.Web.ViewModels.Vocations
{
    using System;
    
    using AutoMapper;
    
    using HCMS.Services.Mapping;
    using HCMS.Data.Models;

    public class VacationsViewModel : IMapFrom<Vacations>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Tittle { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool? Status { get; set; }

        public string Employee { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Vacations, VacationsViewModel>()
                .ForMember(x => x.Employee, y => y.MapFrom(v => v.User.Name));
        }
    }
}
