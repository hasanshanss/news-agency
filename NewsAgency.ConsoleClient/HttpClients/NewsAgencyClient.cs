using NewsAgency.ConsoleClient.Extensions;
using NewsAgency.ConsoleClient.HttpClients.Abstractions;
using NewsAgency.ConsoleClient.Resources;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgency.ConsoleClient.HttpClients
{
    class NewsAgencyClient : INewsAgencyClient
    {
        private const string BaseUrl = "https://localhost:44359/";
        private HttpClient _client;

        public NewsAgencyClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<News>> GetNewsAsync()
        {
            HttpResponseMessage response =  await _client.GetAsync(BaseUrl + "api/News");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<News[]>();
            }

            return null;
        }

        public async Task<int> GetNewsCountAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(BaseUrl + "api/News/Count");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<int>();
            }

            return 0;
        }
    }
}
