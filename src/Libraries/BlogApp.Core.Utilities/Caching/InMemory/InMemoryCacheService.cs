using BlogApp.Core.Utilities.Caching.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BlogApp.Core.Utilities.Caching.InMemory;
public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    public InMemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<T?> Get<T>(string key, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_memoryCache.Get<T>(key));
    }

    public Task Remove(string key, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(() => _memoryCache.Remove(key));
    }

    public Task Set<T>(string key, T value, DateTimeOffset expirationTime, CancellationToken cancellationToken = default)
    {
        var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(expirationTime);

        return Task.FromResult(_memoryCache.Set(key, value, options));
    }
}
