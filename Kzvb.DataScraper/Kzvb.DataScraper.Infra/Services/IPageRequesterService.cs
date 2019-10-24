using HtmlAgilityPack;

namespace Kzvb.DataScraper.Infra.Services
{
	public interface IPageRequesterService
	{
		HtmlDocument LoadWebPage(string url);
	}
}
