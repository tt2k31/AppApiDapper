using AppApiDapper.Services.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AppApiDapper.Services.Repository
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive)
        {
            if(response == null)
            {
                return;
            }
            var serializedResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeTimeLive,
            });
            
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
            return String.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }
    }
}
