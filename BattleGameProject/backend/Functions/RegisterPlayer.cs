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
    public static class RegisterPlayer
    {
        [FunctionName("registerplayer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "registerplayer")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var player = JsonConvert.DeserializeObject<Player>(requestBody);
            if (player == null) return new BadRequestObjectResult("Invalid payload");

            var conn = Environment.GetEnvironmentVariable("ConnectionStrings__MySql");
            using (var db = DbContextFactory.Create(conn))
            {
                db.Players.Add(player);
                await db.SaveChangesAsync();
            }

            return new OkObjectResult(new { message = "Player registered", playerId = player.PlayerId });
        }
    }
}
