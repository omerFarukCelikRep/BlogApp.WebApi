using BlogApp.Core.Utilities.Caching.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace BlogApp.Core.Utilities.CrossCuttingConcerns.Caching;
public class DistributedCacheService : ICacheService
{
    private readonly IDatabase _database;
    public DistributedCacheService(IConnectionMultiplexer redisConnection)
    {
        _database = redisConnection.GetDatabase();
    }

    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> func, string key, DateTimeOffset expirationTime, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> Get<T>(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var value = await _database.StringGetAsync(key);
        if (string.IsNullOrWhiteSpace(value))
            return default;

        var result = JsonSerializer.Deserialize<T>(value);
        return result;
    }

    public async Task Remove(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var isExist = await _database.KeyExistsAsync(key);
        if (isExist)
            await _database.KeyDeleteAsync(key);
    }

    public Task Set<T>(string key, T value, DateTimeOffset expirationTime, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.FromCanceled<T>(cancellationToken);

        return _database.StringSetAsync(key, JsonSerializer.Serialize(value), expirationTime.Offset);
    }
}