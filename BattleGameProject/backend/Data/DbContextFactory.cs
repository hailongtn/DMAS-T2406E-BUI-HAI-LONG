using Microsoft.EntityFrameworkCore;

namespace BattleGame.Functions.Data
{
    public static class DbContextFactory
    {
        public static BattleGameContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BattleGameContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new BattleGameContext(optionsBuilder.Options);
        }
    }
}
