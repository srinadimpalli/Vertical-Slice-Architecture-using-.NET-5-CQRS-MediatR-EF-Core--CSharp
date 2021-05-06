using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;
using VerticalArchApp.API.Domain;
using VerticalArchApp.API.Features.Companies;
using Xunit;

namespace VerticalArchApp.Test.Features
{
    public class CompanieesTest : BaseIntegrationTest
    {
        public CompanieesTest() : base(databasename: "CompanieesTest") { }
        protected async override Task SeedAsync(CompanyEmpContext db)
        {
            await db.Companies.AddAsync(new Company
            {
                Id = 100,
                Name = "Intel Inc",
                Address = "123 state st, albany, NY",
                Country = "USA"
            });
            await db.Companies.AddAsync(new Company
            {
                Id = 200,
                Name = "Microsoft",
                Address = "345 Wilson ct, Malta, NY",
                Country = "USA"
            });
            await db.SaveChangesAsync();
        }
        public class ListAllCompanieesTest : CompanieesTest
        {
            [Fact]
            public async Task Should_return_all_companiees()
            {
                // Arrange
                var serviceProvider = _services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                //Act
                var result = await mediator.Send(new GetAllCompanies.Query());

                //Assert
                using var assertScope = serviceProvider.CreateScope();
                var db = assertScope.ServiceProvider.GetRequiredService<CompanyEmpContext>();
                Assert.Collection(result,
                    companie => Assert.Equal("Intel Inc", companie.Name),
                    companie => Assert.Equal("Microsoft", companie.Name)
                );
            }

        }
    }
}
