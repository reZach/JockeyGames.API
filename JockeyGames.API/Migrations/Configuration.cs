namespace JockeyGames.API.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<JockeyGames.API.Models.JockeyGamesAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JockeyGames.API.Models.JockeyGamesAPIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /*context.Players.AddOrUpdate(
                new Player
                {
                    Id = 1,
                    Name = "test"
                },
                new Player
                {
                    Id = 2,
                    Name = "test"
                }
            );

            context.Matches.AddOrUpdate(
                new Match
                {
                    Id = 1,
                    DateTime = DateTime.Now,
                    Players = new List<Player>()
                    {
                        new Player
                        {
                            Id = 1,
                            Name = "test"
                        },
                        new Player
                        {
                            Id = 2,
                            Name = "test"
                        }
                    }
                }
            );*/
        }
    }
}
