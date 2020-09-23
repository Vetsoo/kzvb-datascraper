using System.Collections.Generic;
using System.Linq;
using Kzvb.DataScraper.Infra.Models;
using Kzvb.DataScraper.Infra.Services.Interfaces;

namespace Kzvb.DataScraper.Infra.Services
{
	public class KzvbDataScraper : IKzvbDataScraper
	{
		private readonly IPageRequesterService _pageRequesterService;
		private readonly ICacheService _cacheService;

		public KzvbDataScraper(IPageRequesterService pageRequesterService, ICacheService cacheService)
		{
			_pageRequesterService = pageRequesterService;
			_cacheService = cacheService;
		}

		public IEnumerable<GameResultModel> GetGameResultsForDivision(string division)
		{
			var results = _cacheService.GetFromCache<List<GameResultModel>>($"gameresults_{division}");
			if (results != null)
				return results;

			var url = $"https://www.kzvb.be/display/display.aspx?pagetype=Uitslagen_list&Uitslagen_Afdeling={division}";
			var webPage = _pageRequesterService.LoadWebPage(url);

			// Get the root table with the results table inside it.
			var rootTable = webPage.DocumentNode.Descendants("table").Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("Uitslagen_ListGrid")).First();

			// Get the child table with the actual results
			var resultsTable = rootTable.Descendants("table").First();

			// Get the body of the results table and all rows with a bgcolor attribute
			var rows = resultsTable.Descendants("tr").Where(r => r.Attributes.Contains("bgcolor") && r.Attributes["bgcolor"].Value.Contains("white")).ToArray();

			results = new List<GameResultModel>();

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

			_cacheService.AddToCache($"gameresults_{division}", results);
			return results;
		}

		public IEnumerable<ClubRankingModel> GetRankingForDivision(string division)
		{
			var results = _cacheService.GetFromCache<List<ClubRankingModel>>($"clubranking_{division}");
			if (results != null)
				return results;

			var url = $"https://www.kzvb.be/display/display.aspx?pagetype=klassementen_list&Afdeling=2B{division}";
			var webPage = _pageRequesterService.LoadWebPage(url);

			// Get the root table with the results table inside it.
			var rootTable = webPage.DocumentNode.Descendants("table").Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("Klassementen_ListGrid")).First();

			// Get the child table with the actual results
			var resultsTable = rootTable.Descendants("table").First();

			// Get the body of the results table and all rows with a bgcolor attribute
			var rows = resultsTable.Descendants("tr").Where(r => r.Attributes.Contains("bgcolor") && r.Attributes["bgcolor"].Value.Contains("white")).ToArray();

			results = new List<ClubRankingModel>();
			var ranking = 1;

			//go over each row
			foreach (var row in rows)
			{
				// get the list of td nodes from the tr
				var fields = row.Descendants("td");

				//add a new product with the content of every td inner text
				results.Add(new ClubRankingModel
				{
					Ranking = ranking,
					Division = fields.ElementAt(0).InnerText,
					ClubNumber = fields.ElementAt(1).InnerText,
					ClubName = fields.ElementAt(2).InnerText,
					Points = fields.ElementAt(3).InnerText,
					GoalsDifference = fields.ElementAt(4).InnerText,
					GoalsFor = fields.ElementAt(5).InnerText,
					GoalsAgainst = fields.ElementAt(6).InnerText,
					Wins = fields.ElementAt(7).InnerText,
					Losses = fields.ElementAt(8).InnerText,
					Draws = fields.ElementAt(9).InnerText,
					GamesPlayed = fields.ElementAt(10).InnerText,
				});

				ranking++;
			}

			_cacheService.AddToCache($"clubranking_{division}", results);
			return results;
		}
	}
}
