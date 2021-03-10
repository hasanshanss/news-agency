using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NewsAgency.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NewsAgency.API.Services.ServiceInstallers
{
    public class SwaggerInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services,
                                   IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsAgency.API", Version = "v1" });
            });
        }

       
    }
}
