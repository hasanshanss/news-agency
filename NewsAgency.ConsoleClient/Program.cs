using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsAgency.ConsoleClient.HttpClients;
using NewsAgency.ConsoleClient.HttpClients.Abstractions;
using NewsAgency.ConsoleClient.Resources;
using Polly;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NewsAgency.ConsoleClient
{
    class Program
    {
        static  async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var newsAgencyClient = provider.GetRequiredService<INewsAgencyClient>();

            //var httpClient = HttpClientFactory.CreateClient("NewsAgencyAPI");

            foreach (News news in await newsAgencyClient.GetNewsAsync())
            {
                Console.WriteLine(news.Title);
                Console.WriteLine(news.Content);
            }

            //Console.WriteLine(await newsAgencyClient.GetNewsCountAsync());

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHttpClient<INewsAgencyClient, NewsAgencyClient>()
                            .AddTransientHttpErrorPolicy(
                        p => p.WaitAndRetryAsync(new[]
                        {
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(10)
                        })));
    }
}
