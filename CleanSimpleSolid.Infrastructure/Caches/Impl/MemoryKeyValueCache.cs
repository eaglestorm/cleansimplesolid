using System;
using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;
using MemoryCache = Microsoft.Extensions.Caching.Memory.MemoryCache;

namespace ServiceBase.Infrastructure.Caches.Impl
{
    public class MemoryKeyValueCache<T>: IKeyValueCache<T>
    {
        private readonly MemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _entryOptions;

        public MemoryKeyValueCache()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = 1024
            }) ;
            _entryOptions = new MemoryCacheEntryOptions();
            _entryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }
        
        public void Add(string key, T t)
        {
            _memoryCache.Set(key, t, _entryOptions);
        }

        public T TryGet(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}