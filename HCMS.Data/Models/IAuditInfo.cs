namespace HCMS.Data.Models
{
    using System;

    public interface IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
