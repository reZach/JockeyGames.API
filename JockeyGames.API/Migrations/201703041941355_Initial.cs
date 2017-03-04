namespace JockeyGames.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Match_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .Index(t => t.Match_Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.Tournament_Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerGames",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlayerId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlayerGames", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Games", "Match_Id", "dbo.Matches");
            DropIndex("dbo.PlayerGames", new[] { "PlayerId" });
            DropIndex("dbo.PlayerGames", new[] { "GameId" });
            DropIndex("dbo.Matches", new[] { "Tournament_Id" });
            DropIndex("dbo.Games", new[] { "Match_Id" });
            DropTable("dbo.Players");
            DropTable("dbo.PlayerGames");
            DropTable("dbo.Tournaments");
            DropTable("dbo.Matches");
            DropTable("dbo.Games");
        }
    }
}
