namespace HCMS.Data.Models
{
    using System;

    public partial class UsersTasks : IDeletableEntity, IAuditInfo
    {
        public int ProjectTaskId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ProjectTasks ProjectTask { get; set; }
        public virtual AppUser User { get; set; }
    }
}
