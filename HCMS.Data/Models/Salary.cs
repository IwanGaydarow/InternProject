namespace HCMS.Data.Models
{
    using System;

    public partial class Salary
    {
        public int Id { get; set; }
        public decimal Salary1 { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}
