using HtmlAgilityPack;
using Kzvb.DataScraper.Infra.Services.Interfaces;
using System.Text;

namespace Kzvb.DataScraper.Infra.Services
{
	public class PageRequesterService : IPageRequesterService
	{
		public HtmlDocument LoadWebPage(string url)
		{
			HtmlWeb website = new HtmlWeb();
			website.AutoDetectEncoding = false;
			website.OverrideEncoding = Encoding.Default;
			return website.Load(url);
		}
	}
}
