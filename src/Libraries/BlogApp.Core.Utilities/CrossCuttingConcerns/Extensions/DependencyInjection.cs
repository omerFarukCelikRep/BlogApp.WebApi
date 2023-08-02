using BlogApp.Core.Utilities.Caching.InMemory;
using BlogApp.Core.Utilities.Caching.Interfaces;
using BlogApp.Core.Utilities.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Core.Utilities.Caching.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = nameof(RedisCacheService);
            options.Configuration = configuration["Redis:Host"];
        });

        services.AddMemoryCache();
        services.AddSingleton<ICacheService, InMemoryCacheService>();
        return services;
    }
}