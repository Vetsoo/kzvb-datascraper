using HtmlAgilityPack;

namespace Kzvb.DataScraper.Infra.Services.Interfaces
{
	public interface IPageRequesterService
	{
		HtmlDocument LoadWebPage(string url);
	}
}
