using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using BattleGame.Functions.Data;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;

namespace BattleGame.Functions.Functions
{
    public static class GetAssetsByPlayer
    {
        [FunctionName("getassetsbyplayer")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getassetsbyplayer")] HttpRequest req,
            ILogger log)
        {
            var conn = Environment.GetEnvironmentVariable("ConnectionStrings__MySql");
            using (var db = DbContextFactory.Create(conn))
            {
                var data = (from p in db.Players
                            join pa in db.PlayerAssets on p.PlayerId equals pa.PlayerId
                            join a in db.Assets on pa.AssetId equals a.AssetId
                            select new
                            {
                                PlayerName = p.PlayerName,
                                Level = p.Level,
                                Age = p.Age,
                                AssetName = a.AssetName
                            }).ToList();

                return new OkObjectResult(data);
            }
        }
    }
}
