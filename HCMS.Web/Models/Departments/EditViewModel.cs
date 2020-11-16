﻿namespace HCMS.Web.Models.Departments
{
    using HCMS.GlobalConstants;
    using HCMS.Services.Mapping;
    using HCMS.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using System.Runtime.CompilerServices;

    public class EditViewModel : IMapFrom<Department>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 3)]
        [Display(Name = "Department Name")]
        public string Tittle { get; set; }

        public string DepartmentManager { get; set; }

        public string ManagerId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Department, EditViewModel>()
                .ForMember(x => x.ManagerId,
                y => y.MapFrom(x => x.DepartmentManager));

            configuration.CreateMap<Department, EditViewModel>()
                .ForMember(x => x.DepartmentManager,
                y => y.MapFrom(x => x.DepartmentManagerNavigation.Name));
        }
    }
}
