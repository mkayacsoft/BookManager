using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Domain.Options
{
    public class RedisCacheOptions
    {
        public string ConnectionString { get; set; } // Redis bağlantı dizesi
        public int CacheTimeoutMinutes { get; set; }
    }
}
