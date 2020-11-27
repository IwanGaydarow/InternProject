namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company : IDeletableEntity, IAuditInfo
    {
        public Company()
        {
            this.Departments = new HashSet<Department>();
            this.Trainings = new HashSet<Trainings>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        public string FullAddres { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Trainings> Trainings { get; set; }
    }
}
