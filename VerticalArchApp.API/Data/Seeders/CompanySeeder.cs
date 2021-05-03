using ForEvolve.EntityFrameworkCore.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalArchApp.API.Domain;

namespace VerticalArchApp.API.Data.Seeders
{
    public class CompanySeeder : ISeeder<CompanyEmpContext>
    {
        public void Seed(CompanyEmpContext db)
        {
            db.Companies.Add(new Company
            {
                Id = 100,
                Name = "IT Solutions Ltd",
                Address = "123 Wall dr. Malta, NY 12345",
                Country = "USA"
            });
            db.Companies.Add(new Company
            {
                Id = 200,
                Name = "Admin Solutions Ltd",
                Address = "232 Corner ave. Wison, CT  52362",
                Country = "USA"
            });

            // employee seed
            db.Employees.Add(new Employee
            {
                Id = 300,
                Name = "John Deo",
                Age = 34,
                Position = "Software Developer",
                CompanyId = 100
            });
            db.Employees.Add(new Employee
            {
                Id = 301,
                Name = "Sam McLeaf",
                Age = 28,
                Position = "Administrator",
                CompanyId = 200
            });

            db.Employees.Add(new Employee
            {
                Id = 302,
                Name = "Dom Sonmaze",
                Age = 45,
                Position = "Software Developer",
                CompanyId = 100
            });

            db.SaveChanges();
        }
    }
}
