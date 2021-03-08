using NewsAgency.ConsoleClient.Resources;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgency.ConsoleClient.HttpClients.Abstractions
{
    interface INewsAgencyClient
    {
        Task<IEnumerable<News>> GetNewsAsync();
        Task<int> GetNewsCountAsync(); 

    }
}
