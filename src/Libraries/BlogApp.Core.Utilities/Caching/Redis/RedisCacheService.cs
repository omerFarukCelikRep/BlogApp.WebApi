using BlogApp.Core.Utilities.Caching.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BlogApp.Core.Utilities.Caching.Redis;
public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T?> Get<T>(string key, CancellationToken cancellationToken = default)
    {
        var data = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrEmpty(data))
        {
            return JsonSerializer.Deserialize<T>(data!)!;
        }

        return default;
    }

    public Task Remove(string key, CancellationToken cancellationToken = default)
    {
        return _distributedCache.RemoveAsync(key, cancellationToken);
    }

    public Task Set<T>(string key, T value, DateTimeOffset expirationTime, CancellationToken cancellationToken = default)
    {
        var jsonData = JsonSerializer.Serialize(value);
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        return _distributedCache.SetStringAsync(key, jsonData, options, cancellationToken);
    }
}
