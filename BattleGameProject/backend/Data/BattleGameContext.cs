using Microsoft.EntityFrameworkCore;
using BattleGame.Functions.Models;

namespace BattleGame.Functions.Data
{
    public class BattleGameContext : DbContext
    {
        public BattleGameContext(DbContextOptions<BattleGameContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Asset> Assets { get; set; } = null!;
        public DbSet<PlayerAsset> PlayerAssets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Asset>().ToTable("Asset");
            modelBuilder.Entity<PlayerAsset>().ToTable("PlayerAsset");

            modelBuilder.Entity<PlayerAsset>()
                .HasOne(pa => pa.Player)
                .WithMany(p => p.PlayerAssets)
                .HasForeignKey(pa => pa.PlayerId);

            modelBuilder.Entity<PlayerAsset>()
                .HasOne(pa => pa.Asset)
                .WithMany(a => a.PlayerAssets)
                .HasForeignKey(pa => pa.AssetId);
        }
    }
}
