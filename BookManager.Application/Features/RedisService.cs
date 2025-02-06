using BookManager.Application.Contracts.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BookManager.Application.Features
{
    public class RedisService(IDistributedCache _cache): IRedisService
    {
        public async Task<T> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            return cachedData == null ? default : JsonSerializer.Deserialize<T>(cachedData);
        }
        public async Task SetCacheAsync<T>(string key, T data, TimeSpan? expiry = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(10)
            };
            var serializedData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, serializedData, options);
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
