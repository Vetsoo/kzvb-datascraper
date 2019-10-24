namespace Kzvb.DataScraper.Infra.Services.Interfaces
{
	public interface ICacheService
	{
		T GetFromCache<T>(string cacheKey);
		bool AddToCache<T>(string cacheKey, T cacheEntry);
	}
}
