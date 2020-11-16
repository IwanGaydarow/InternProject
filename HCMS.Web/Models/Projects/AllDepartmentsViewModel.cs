﻿namespace HCMS.Web.Models.Projects
{
    using HCMS.Data.Models;
    using HCMS.Services.Mapping;
    public class AllDepartmentsViewModel : IMapFrom<Department>
    {
        public int Id { get; set; }

        public string Tittle { get; set; }
    }
}
