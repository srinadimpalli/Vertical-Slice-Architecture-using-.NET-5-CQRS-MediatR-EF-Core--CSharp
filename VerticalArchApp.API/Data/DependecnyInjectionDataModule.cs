using ForEvolve.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace VerticalArchApp.API.Data
{
    public class DependecnyInjectionDataModule : DependencyInjectionModule
    {
        public DependecnyInjectionDataModule(IServiceCollection services) : base(services)
        {
            services.AddDbContext<CompanyEmpContext>(options => options
                .UseInMemoryDatabase("CompanyEmployeeMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                );
            services.AddForEvolveSeeders().Scan<Startup>();
        }
    }
}
