namespace HCMS.Web.ViewModels.Employee
{
    using AutoMapper;
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    using System;

    public class EmployeeTrainingViewModel : IMapFrom<TrainingsUsers>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Tittle { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int TrainingHours { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TrainingsUsers, EmployeeTrainingViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(tu => tu.Training.Id))
                .ForMember(x => x.Tittle, y => y.MapFrom(tu => tu.Training.Tittle))
                .ForMember(x => x.Description, y => y.MapFrom(tu => tu.Training.Description))
                .ForMember(x => x.Start, y => y.MapFrom(tu => tu.Training.Start))
                .ForMember(x => x.End, y => y.MapFrom(tu => tu.Training.End))
                .ForMember(x => x.End, y => y.MapFrom(tu => tu.Training.End))
                .ForMember(x => x.TrainingHours, y => y.MapFrom(tu => tu.Training.TrainingHours));
        }
    }
}
