using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Kzvb.DataScraper.Infra.Services.Interfaces;
using System;

namespace Kzvb.DataScraper.AzureFunction
{
	public class HttpTrigger
	{
		private readonly IKzvbDataScraper _kzvbDataScraper;

		public HttpTrigger(IKzvbDataScraper kzvbDataScraper)
		{
			_kzvbDataScraper = kzvbDataScraper;
		}

		[FunctionName("GetResultsForDivision")]
		public IActionResult GetResultsForDivision(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "results")] HttpRequest req,
			ILogger log)
		{
			try
			{
				log.LogInformation("C# HTTP trigger function processed a GetResultsForDivision request.");

				string division = req.Query["division"];
				var results = _kzvbDataScraper.GetGameResultsForDivision(division);

				return new OkObjectResult(results);
			}
			catch (Exception exception)
			{
				log.LogError($"{DateTime.Now}: An error occured: {exception.Message}. Stacktrace: {exception.StackTrace}");
				return new StatusCodeResult(500);
			}
		}

		[FunctionName("GetRankingForDivision")]
		public IActionResult GetRankingForDivision(
			[HttpTrigger(AuthorizationLevel.Function, "get", Route = "ranking")] HttpRequest req,
			ILogger log)
		{
			try
			{
				log.LogInformation("C# HTTP trigger function processed a GetRankingForDivision request.");

				string division = req.Query["division"];
				var ranking = _kzvbDataScraper.GetRankingForDivision(division);

				return new OkObjectResult(ranking);
			}
			catch (Exception exception)
			{
				log.LogError($"{DateTime.Now}: An error occured: {exception.Message}. Stacktrace: {exception.StackTrace}");
				return new StatusCodeResult(500);
			}
		}
	}
}
