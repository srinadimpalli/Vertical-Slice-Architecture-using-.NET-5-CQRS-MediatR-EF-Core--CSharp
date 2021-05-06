using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Domain;
using VerticalArchApp.API.Features.Employees;
using Xunit;

namespace VerticalArchApp.Test.Features
{
    public class EmployeeTests : BaseIntegrationTest
    {
        public EmployeeTests() : base(databasename: "employeeTest") { }

        protected async override Task SeedAsync(CompanyEmpContext db)
        {
            await db.Employees.AddAsync(new Employee
            {
                Id = 304,
                Name = "John",
                Age = 33,
                Position = "Developer",
                CompanyId = 100
            });
            await db.Employees.AddAsync(new Employee
            {
                Id = 305,
                Name = "Jain",
                Age = 27,
                Position = "CEO",
                CompanyId = 200
            });
            await db.SaveChangesAsync();
        }

        public class AddEmployeeTest : EmployeeTests
        {
            private const int _employeeid = 306;

            [Fact]
            public async Task Should_Add_Employee_For_Companyid()
            {
                // Arrange
                var serviceProvider = _services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<CompanyEmpContext>();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Act
                //var result = db.Employees.Add(new Employee
                //{
                //    Name = "mike",
                //    Age = 45,
                //    Position = "Architect",
                //    CompanyId = 200
                //});
                //await db.SaveChangesAsync();
                var result = await mediator.Send(new CreateEmployeeForCompany.Command
                {
                    Name = "mike",
                    Age = 45,
                    Position = "Architect",
                    CompanyId = 200
                });

                // Assert
                using var assertScope = serviceProvider.CreateScope();
                var employee = await db.Employees.FindAsync(_employeeid);
                Assert.Equal(306, employee.Id);
                Assert.Equal("mike", employee.Name);
            }
        }

    }
}
