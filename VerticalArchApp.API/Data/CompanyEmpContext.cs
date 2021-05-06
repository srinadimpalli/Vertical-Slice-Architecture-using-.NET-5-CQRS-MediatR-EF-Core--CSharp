using Microsoft.EntityFrameworkCore;
using VerticalArchApp.API.Domain;

namespace VerticalArchApp.API.Data
{
    public class CompanyEmpContext : DbContext
    {
        public CompanyEmpContext(DbContextOptions<CompanyEmpContext> options) : base(options) { }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
