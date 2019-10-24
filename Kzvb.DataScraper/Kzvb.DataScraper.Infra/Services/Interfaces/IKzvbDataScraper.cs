using Kzvb.DataScraper.Infra.Models;
using System.Collections.Generic;

namespace Kzvb.DataScraper.Infra.Services.Interfaces
{
	public interface IKzvbDataScraper
	{
		IEnumerable<GameResultModel> GetGameResultsForDivision(string division);
		IEnumerable<ClubRankingModel> GetRankingForDivision(string division);
	}
}
