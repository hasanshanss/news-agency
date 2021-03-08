using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NewsAgency.API.Options;
using NewsAgency.Services;
using NewsAgency.Services.Abstraction;
using NewsAgency.DAL.Entities;
using NewsAgency.DAL;
using NewsAgency.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NewsAgency.API.Services.Abstractions;
using NewsAgency.API.Services;

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
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });


            var redisOptions = new RedisOptions();
            Configuration.GetSection(nameof(RedisOptions)).Bind(redisOptions);

            services.AddSingleton(redisOptions);

            if (redisOptions.Enabled)
            {
                services.AddStackExchangeRedisCache(options => options.Configuration = redisOptions.ConnectionString);
                services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            }

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsService<News, NewsCategory>, NewsService>();

            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsAgency.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
