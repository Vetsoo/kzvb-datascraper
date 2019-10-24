using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Kzvb.DataScraper.Infra.Models;

namespace Kzvb.DataScraper.Infra.Services
{
	public class KzvbDataScraper : IKzvbDataScraper
	{
		public IEnumerable<GameResultModel> GetGameResultsForDivision(string division)
		{
			HtmlWeb website = new HtmlWeb();
			website.AutoDetectEncoding = false;
			website.OverrideEncoding = Encoding.Default;
			var url = $"http://www.kzvb.be/(S(w2uz1u45vxotxf55ihxffz45))/display/display.aspx?pagetype=Uitslagen_list&Uitslagen_Afdeling={division}";
			HtmlDocument doc = website.Load(url);

			// Get the root table with the results table inside it.
			var rootTable = doc.DocumentNode.Descendants("table").Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("Uitslagen_ListGrid")).First();

			// Get the child table with the actual results
			var resultsTable = rootTable.Descendants("table").First();

			// Get the body of the results table and all rows with a bgcolor attribute
			var rows = resultsTable.Descendants("tr").Where(r => r.Attributes.Contains("bgcolor") && r.Attributes["bgcolor"].Value.Contains("white")).ToArray();

			var results = new List<GameResultModel>();

			//go over each row
			foreach (var row in rows)
			{
				// get the list of td nodes from the tr
				var fields = row.Descendants("td");

				//add a new product with the content of every td inner text
				results.Add(new GameResultModel
				{
					Wdn = fields.ElementAt(0).InnerText,
					Division = fields.ElementAt(1).InnerText,
					Date = fields.ElementAt(2).InnerText,
					Home = fields.ElementAt(3).InnerText,
					Visitors = fields.ElementAt(4).InnerText,
					Score = $"{fields.ElementAt(5).InnerText}-{fields.ElementAt(7).InnerText}"
				});
			}

			return results;
		}
	}
}
