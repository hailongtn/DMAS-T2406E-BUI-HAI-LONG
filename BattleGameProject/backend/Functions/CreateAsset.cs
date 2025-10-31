using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BattleGame.Functions.Models;
using BattleGame.Functions.Data;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace BattleGame.Functions.Functions
{
    public static class CreateAsset
    {
        [FunctionName("createasset")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createasset")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var asset = JsonConvert.DeserializeObject<Asset>(requestBody);
            if (asset == null) return new BadRequestObjectResult("Invalid payload");

            var conn = Environment.GetEnvironmentVariable("ConnectionStrings__MySql");
            using (var db = DbContextFactory.Create(conn))
            {
                db.Assets.Add(asset);
                await db.SaveChangesAsync();
            }

            return new OkObjectResult(new { message = "Asset created", assetId = asset.AssetId });
        }
    }
}
