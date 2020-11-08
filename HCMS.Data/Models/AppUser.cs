﻿namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public partial class AppUser : IdentityUser<string>
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Departments = new HashSet<Departments>();
            this.Evaluations = new HashSet<Evaluations>();
            this.Salary = new HashSet<Salary>();
            this.TrainingsUsers = new HashSet<TrainingsUsers>();
            this.UsersTasks = new HashSet<UsersTasks>();
            this.Vacations = new HashSet<Vacations>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Departments Department { get; set; }

        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<Evaluations> Evaluations { get; set; }
        public virtual ICollection<Salary> Salary { get; set; }
        public virtual ICollection<TrainingsUsers> TrainingsUsers { get; set; }
        public virtual ICollection<UsersTasks> UsersTasks { get; set; }
        public virtual ICollection<Vacations> Vacations { get; set; }
    }
}
