namespace HCMS.Data.Models
{
    using System;

    public partial class Evaluations
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}
