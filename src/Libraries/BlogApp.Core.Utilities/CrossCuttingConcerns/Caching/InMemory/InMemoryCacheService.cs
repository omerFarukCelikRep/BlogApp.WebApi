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

    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> func, string key, DateTimeOffset expirationTime, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        var hasValue = _memoryCache.TryGetValue(key, out TResult cachedResult);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (hasValue && cachedResult is not null)
            return Task.FromResult(cachedResult);

        TResult? result = func();
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = expirationTime
        };

        _memoryCache.Set(key, cacheEntryOptions);
        return Task.FromResult(result);
    }

    public Task<T?> Get<T>(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
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