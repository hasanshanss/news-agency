using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsAgency.API.Options;
using NewsAgency.API.Services.Abstractions;


namespace NewsAgency.API.Services.ServiceInstallers
{
    public class RedisInstaller : IServiceInstaller
    {

        public void InstallServices(IServiceCollection services, 
                                    IConfiguration configuration)
        {
            var redisOptions = new RedisOptions();
            configuration.GetSection(nameof(RedisOptions)).Bind(redisOptions);

            services.AddSingleton(redisOptions);

            if (redisOptions.Enabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = redisOptions.ConnectionString);
                services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            }
        }
    }
}
