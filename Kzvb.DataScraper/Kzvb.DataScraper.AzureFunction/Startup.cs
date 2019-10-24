using Kzvb.DataScraper.Infra.Services;
using Kzvb.DataScraper.Infra.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Kzvb.DataScraper.AzureFunction.Startup))]

namespace Kzvb.DataScraper.AzureFunction
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			builder.Services.AddSingleton<IKzvbDataScraper, KzvbDataScraper>();
			builder.Services.AddSingleton<IPageRequesterService, PageRequesterService>();
			builder.Services.AddSingleton<ICacheService, CacheService>();
		}
	}
}
