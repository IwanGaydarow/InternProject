namespace HCMS.Data.Models
{
    using HCMS.Data.Models.Enums;
    using System;

    public partial class Salary : IDeletableEntity, IAuditInfo
    {
        public int Id { get; set; }

        public decimal Salary1 { get; set; }

        public SalaryPeriodicity Periodicity { get; set; }

        public DateTime EffectiveTo { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}
