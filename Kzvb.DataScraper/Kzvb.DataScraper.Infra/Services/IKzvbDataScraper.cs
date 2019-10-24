using Kzvb.DataScraper.Infra.Models;
using System.Collections.Generic;

namespace Kzvb.DataScraper.Infra.Services
{
	public interface IKzvbDataScraper
	{
		IEnumerable<GameResultModel> GetGameResultsForDivision(string division);
	}
}
