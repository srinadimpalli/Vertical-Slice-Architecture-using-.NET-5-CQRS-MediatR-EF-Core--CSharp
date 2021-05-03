using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalArchApp.API;
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
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
