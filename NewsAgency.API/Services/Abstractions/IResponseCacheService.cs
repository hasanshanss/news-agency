using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgency.API.Services.Abstractions
{
    interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeLive);
        Task<string> GetCacheResponseAsync(string cacheKey);
    }
}
