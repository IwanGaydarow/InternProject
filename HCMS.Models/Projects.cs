namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Projects : IDeletableEntity, IAuditInfo
    {
        public Projects()
        {
            this.ProjectTasks = new HashSet<ProjectTasks>();
        }

        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public int EstimatedWorkHours { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<ProjectTasks> ProjectTasks { get; set; }
    }
}
