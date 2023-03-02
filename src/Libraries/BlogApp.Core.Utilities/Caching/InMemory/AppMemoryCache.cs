using Microsoft.Extensions.Caching.Memory;

namespace BlogApp.Core.Utilities.Caching.InMemory;
public class AppMemoryCache
{
    public MemoryCache Cache { get; } = new MemoryCache(
       new MemoryCacheOptions
       {
           SizeLimit = 1024
       });
}
