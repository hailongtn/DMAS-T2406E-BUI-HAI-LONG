using System.Collections.Generic;

namespace BattleGame.Functions.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; } = default!;
        public List<PlayerAsset>? PlayerAssets { get; set; }
    }
}
