namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Trainings : IDeletableEntity, IAuditInfo
    {
        public Trainings()
        {
            this.TrainingsUsers = new HashSet<TrainingsUsers>();
        }

        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<TrainingsUsers> TrainingsUsers { get; set; }
    }
}
