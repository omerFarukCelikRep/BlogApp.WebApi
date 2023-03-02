using BlogApp.Core.Utilities.Caching.InMemory;
using BlogApp.Core.Utilities.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Core.Utilities.Caching.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = nameof(RedisCacheService);
            options.Configuration = "127.0.0.1:6379";
        });

        services.AddSingleton<AppMemoryCache>();
        return services;
    }
}
