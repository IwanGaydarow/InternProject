namespace HCMS.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Country : IDeletableEntity, IAuditInfo
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedOn { get; set; }
        
        public IEnumerable<City> Cities { get; set; }
    }
}
