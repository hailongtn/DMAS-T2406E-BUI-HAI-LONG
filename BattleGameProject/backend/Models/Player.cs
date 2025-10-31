using System.Collections.Generic;

namespace BattleGame.Functions.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = default!;
        public string? FullName { get; set; }
        public int Age { get; set; }
        public int Level { get; set; }

        public List<PlayerAsset>? PlayerAssets { get; set; }
    }
}
