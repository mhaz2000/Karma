using Karma.Core.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Karma.Infrastructure.Caching
{
    public class InMemoryCaching : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCaching(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task Clear(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<string?> Get(string key)
        {
            return Task.FromResult(_memoryCache.Get<string>(key));
        }

        public Task Set(string key, string value, int expireTime)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expireTime));
            _memoryCache.Set(key, value, cacheEntryOptions);
            return Task.CompletedTask;
        }
    }
}
