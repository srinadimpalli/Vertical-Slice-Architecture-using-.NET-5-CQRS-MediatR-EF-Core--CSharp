using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
