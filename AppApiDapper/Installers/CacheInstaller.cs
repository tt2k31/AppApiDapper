using AppApiDapper.Cache;
using AppApiDapper.Services.Interface;
using AppApiDapper.Services.Repository;

namespace AppApiDapper.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);
            if(redisCacheSettings.Enable)
            {
                return;
            }
            services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = redisCacheSettings.ConnectionString;
            });
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }

    }
}
