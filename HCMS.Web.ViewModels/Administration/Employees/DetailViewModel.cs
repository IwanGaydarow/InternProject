namespace HCMS.Web.ViewModels.Administration.Employees
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using AutoMapper;
    
    using HCMS.Data.Models;
    using HCMS.GlobalConstants;
    using HCMS.Services.Mapping;

    public class DetailViewModel : IMapFrom<AppUser>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string JobTittle { get; set; }

        public string Department { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = GlobalConstant.StringLenghtValidation, MinimumLength = 4)]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [NotMapped]
        public string Role { get; set; }

       
        public string Town { get; set; }

        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        public EmailFormViewModel EmailForm { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AppUser, DetailViewModel>()
                .ForMember(x => x.EmployeeName, y =>
                            y.MapFrom(a => a.Name))
                .ForMember(x => x.Department, y =>
                            y.MapFrom(d => d.Department.Tittle))
                .ForMember(x => x.Town, y =>
                            y.MapFrom(c => c.City.Name))
                .ForMember(x => x.Phone, y => 
                            y.MapFrom(a => a.PhoneNumber));
        }
    }
}
