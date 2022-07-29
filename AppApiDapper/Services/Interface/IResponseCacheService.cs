namespace AppApiDapper.Services.Interface
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive);
        Task<string> GetCacheResponseAsync(string cacheKey);
    }
}
