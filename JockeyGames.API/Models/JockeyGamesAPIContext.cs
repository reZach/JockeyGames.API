using JockeyGames.Models.PingPong;
using System.Data.Entity;

namespace JockeyGames.API.Models
{
    public class JockeyGamesAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public JockeyGamesAPIContext() : base("name=JockeyGamesAPIContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Matches { get; set; }
    }
}
