using BlogApp.Core.Utilities.Caching.Interfaces;
using BlogApp.Core.Utilities.Constants;
using BlogApp.Core.Utilities.CrossCuttingConcerns.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Core.Utilities.Caching.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = nameof(DistributedCacheService);
            options.Configuration = configuration[DatabaseConstants.RedisConnection];
        });

        //services.AddMemoryCache();
        services.AddSingleton<ICacheService, DistributedCacheService>();
        return services;
    }
}