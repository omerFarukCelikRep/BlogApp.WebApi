namespace BlogApp.Core.Utilities.Caching.Interfaces;
public interface ICacheService
{
    Task<T?> Get<T>(string key, CancellationToken cancellationToken = default);
    Task Set<T>(string key, T value, DateTimeOffset expirationTime, CancellationToken cancellationToken = default);
    Task Remove(string key, CancellationToken cancellationToken = default);
    Task<TResult> ExecuteAsync<TResult>(Func<TResult> func, string key, DateTimeOffset expirationTime, CancellationToken cancellationToken = default);
}
