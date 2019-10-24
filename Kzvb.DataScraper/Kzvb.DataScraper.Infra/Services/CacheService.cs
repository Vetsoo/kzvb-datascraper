using Kzvb.DataScraper.Infra.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Kzvb.DataScraper.Infra.Services
{
	public class CacheService : ICacheService
	{
		private IMemoryCache _memoryCache;

		public CacheService(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public bool AddToCache<T>(string cacheKey, T cacheEntry)
		{
			// Set cache options.
			var cacheEntryOptions = new MemoryCacheEntryOptions()
				// Keep in cache for this time, reset time if accessed.
				.SetAbsoluteExpiration(TimeSpan.FromHours(6));

			// Save data in cache.
			_memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);

			return true;
		}

		public T GetFromCache<T>(string cacheKey)
		{
			var cacheEntry = _memoryCache.Get<T>(cacheKey);
			return cacheEntry;
		}
	}
}