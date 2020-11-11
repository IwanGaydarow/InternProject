namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ProjectTasks : IDeletableEntity, IAuditInfo
    {
        public ProjectTasks()
        {
            this.UsersTasks = new HashSet<UsersTasks>();
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
        public int ProjectId { get; set; }

        public virtual Projects Project { get; set; }
        public virtual ICollection<UsersTasks> UsersTasks { get; set; }
    }
}
