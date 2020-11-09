namespace HCMS.Data.Models
{
    using System;

    public partial class Vacations : IDeletableEntity
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}
