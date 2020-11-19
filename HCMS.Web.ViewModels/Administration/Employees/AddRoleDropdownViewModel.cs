namespace HCMS.Web.ViewModels.Administration.Employees
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddRoleDropdownViewModel
    {
        [Required]
        public string Role { get; set; }

        public string EmployeeId { get; set; }

        public virtual IEnumerable<string> Rols { get; set; }
    }
}
