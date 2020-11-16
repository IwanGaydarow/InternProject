namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;
    public class Department : IDeletableEntity, IAuditInfo
    {
        public Department()
        {
            this.Employess = new HashSet<AppUser>();
            this.Projects = new HashSet<Projects>();
        }

        public int Id { get; set; }

        public string Tittle { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public string DepartmentManager { get; set; }
        public virtual AppUser DepartmentManagerNavigation{ get; set; }

        public virtual ICollection<AppUser> Employess { get; set; }
        public virtual ICollection<Projects> Projects { get; set; }
    }
}
