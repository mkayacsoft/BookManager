using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Application.Contracts.Persistence
{
    public interface IRedisService
    {
        Task<T> GetFromCacheAsync<T>(string key);


        Task SetCacheAsync<T>(string key, T data, TimeSpan? expiry = null);


        Task RemoveCacheAsync(string key);
    }
}
