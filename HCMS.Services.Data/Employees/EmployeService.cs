using HCMS.Data.Common.Repositories;
using HCMS.Data.Models;
using HCMS.Services.Mapping;
using HCMS.Web.ViewModels.Administration.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HCMS.Services.Data.Employees
{
    public class EmployeService : IEmployeService
    {
        private readonly IRepository<AppUser> employeeRepository;

        public EmployeService(IRepository<AppUser> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<T> GetAllEmployees<T>(int companyId)
        {
            return this.employeeRepository.All()
                .Include(x => x.Department)
                .Where(x => x.Department.CompanyId == companyId)
                .To<T>().ToList();
        }
    }
}
