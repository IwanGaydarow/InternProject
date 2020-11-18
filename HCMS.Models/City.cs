namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class City : IDeletableEntity, IAuditInfo
    {
        public City()
        {
            this.Employees = new HashSet<AppUser>();
            this.Departments = new HashSet<Department>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int? PostCode { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual IEnumerable<AppUser> Employees { get; set; }

        public virtual IEnumerable<Department> Departments { get; set; }
    }
}
