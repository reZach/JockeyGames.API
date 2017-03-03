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

        public System.Data.Entity.DbSet<JockeyGames.Models.Shared.Player> Players { get; set; }

        public System.Data.Entity.DbSet<JockeyGames.Models.PingPong.PlayerGame> PlayerGames { get; set; }

        public System.Data.Entity.DbSet<JockeyGames.Models.PingPong.Game> Games { get; set; }

        public System.Data.Entity.DbSet<JockeyGames.Models.PingPong.Match> Matches { get; set; }

        public System.Data.Entity.DbSet<JockeyGames.Models.PingPong.Tournament> Tournaments { get; set; }
    }
}
