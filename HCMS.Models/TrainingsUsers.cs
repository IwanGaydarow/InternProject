﻿namespace HCMS.Data.Models
{
    using System;

    public partial class TrainingsUsers : IDeletableEntity, IAuditInfo
    {
        public string UserId { get; set; }
        public int TrainingId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual Trainings Training { get; set; }
        public virtual AppUser User { get; set; }
    }
}
