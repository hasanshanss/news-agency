using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsAgency.API.Options;
using NewsAgency.DAL.UnitOfWork;
using NewsAgency.API.Services.Abstractions;
using System.Linq;
using System;
using Autofac;
using NewsAgency.API.Helpers;
using Hangfire;

namespace NewsAgency.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllers();

            var installers = typeof(Startup)
                                .Assembly
                                .ExportedTypes
                                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                                .Select(Activator.CreateInstance)
                                .Cast<IServiceInstaller>()
                                .ToList();

            installers.ForEach(installer => installer.InstallServices(services, Configuration));
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<ServicesModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                var swaggerOptions = new SwaggerOptions();
                Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

                app.UseSwagger(options => { options.RouteTemplate = swaggerOptions.JsonRoute; });

                app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerOptions.UIEndpoint,
                                                        swaggerOptions.Description));
            }

            app.UseHttpsRedirection();

            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
