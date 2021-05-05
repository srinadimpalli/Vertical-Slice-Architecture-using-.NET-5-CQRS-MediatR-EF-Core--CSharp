using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalArchApp.API;
using VerticalArchApp.API.Data;
using Xunit;

namespace VerticalArchApp.Test
{
    public abstract class BaseIntegrationTest : IAsyncLifetime
    {
        protected readonly IServiceCollection _services;
        private readonly string _databaseName;

        public BaseIntegrationTest(string databasename)
        {
            _databaseName = databasename ?? throw new ArgumentNullException(nameof(databasename));
            var services = _services = new ServiceCollection();
            new Startup().ConfigureServices(services);
            RemoveDbContext<CompanyEmpContext>();

            _services.AddDbContext<CompanyEmpContext>(options => options
                .UseInMemoryDatabase(databasename)
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            );
        }

        protected void RemoveDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            var dbContextDescriptor = _services.FirstOrDefault(x => x.ServiceType == typeof(TDbContext));
            var optionsDescriptor = _services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<TDbContext>));
            _services.Remove(dbContextDescriptor);
            _services.Remove(optionsDescriptor);

        }
        private static object _initLock = new object();
        private static ConcurrentDictionary<string, bool> _dbInit = new ConcurrentDictionary<string, bool>();

        protected abstract Task SeedAsync(CompanyEmpContext db);
        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            if (_dbInit.ContainsKey(_databaseName)) { return; }
            lock (_initLock)
            {
                if (_dbInit.ContainsKey(_databaseName)) { return; }
                _dbInit[_databaseName] = true;
            }
            var db = _services
                    .BuildServiceProvider()
                    .CreateScope().ServiceProvider
                    .GetRequiredService<CompanyEmpContext>();
            await SeedAsync(db);

        }
    }
}
