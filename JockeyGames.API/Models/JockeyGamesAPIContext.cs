using JockeyGames.Models.PingPong;
using JockeyGames.Models.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
            // Playergame
            modelBuilder.Entity<Player>().HasKey(p => p.Id).HasMany(p => p.PlayerGames).WithRequired(p => p.Player).HasForeignKey(p => p.PlayerId);
            modelBuilder.Entity<Game>().HasKey(g => g.Id).HasMany(g => g.PlayerGames).WithRequired(g => g.Game).HasForeignKey(g => g.GameId);

            // Matches
            modelBuilder.Entity<Match>().HasKey(m => m.Id).HasMany<Game>(m => m.Games).WithRequired(g => g.Match);
            modelBuilder.Entity<Match>().HasMany<Player>(m => m.Players).WithMany(p => p.Matches);

            // Tournament
            modelBuilder.Entity<Tournament>().HasKey(t => t.Id).HasMany(t => t.Matches);
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerGame> PlayerGames { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }
    }
}
