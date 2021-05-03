using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VerticalArchApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup()
        {
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var currentAssembly = GetType().Assembly;
            services.AddAutoMapper(currentAssembly);
            services.AddMediatR(currentAssembly);
            services.AddDependencyInjectionModules(currentAssembly);
            services.AddControllers(options => options.Filters.Add<FluentValidationExceptionFilter>())
                .AddFluentValidation(config => config.RegisterValidatorsFromAssembly(currentAssembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ThrowFluentValidationExceptionBehavior<,>));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VerticalArchApp.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VerticalArchApp.API v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            app.Seed<Data.CompanyEmpContext>();
        }
    }
}
