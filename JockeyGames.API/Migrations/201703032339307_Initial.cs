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
                        PlayerGameId1_Id = c.Int(),
                        PlayerGameId2_Id = c.Int(),
                        Match_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlayerGames", t => t.PlayerGameId1_Id)
                .ForeignKey("dbo.PlayerGames", t => t.PlayerGameId2_Id)
                .ForeignKey("dbo.Matches", t => t.Match_Id)
                .Index(t => t.PlayerGameId1_Id)
                .Index(t => t.PlayerGameId2_Id)
                .Index(t => t.Match_Id);
            
            CreateTable(
                "dbo.PlayerGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        PlayerId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId_Id)
                .Index(t => t.PlayerId_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PlayerId1_Id = c.Int(),
                        PlayerId2_Id = c.Int(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId1_Id)
                .ForeignKey("dbo.Players", t => t.PlayerId2_Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.PlayerId1_Id)
                .Index(t => t.PlayerId2_Id)
                .Index(t => t.Tournament_Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Matches", "PlayerId2_Id", "dbo.Players");
            DropForeignKey("dbo.Matches", "PlayerId1_Id", "dbo.Players");
            DropForeignKey("dbo.Games", "Match_Id", "dbo.Matches");
            DropForeignKey("dbo.Games", "PlayerGameId2_Id", "dbo.PlayerGames");
            DropForeignKey("dbo.Games", "PlayerGameId1_Id", "dbo.PlayerGames");
            DropForeignKey("dbo.PlayerGames", "PlayerId_Id", "dbo.Players");
            DropIndex("dbo.Matches", new[] { "Tournament_Id" });
            DropIndex("dbo.Matches", new[] { "PlayerId2_Id" });
            DropIndex("dbo.Matches", new[] { "PlayerId1_Id" });
            DropIndex("dbo.PlayerGames", new[] { "PlayerId_Id" });
            DropIndex("dbo.Games", new[] { "Match_Id" });
            DropIndex("dbo.Games", new[] { "PlayerGameId2_Id" });
            DropIndex("dbo.Games", new[] { "PlayerGameId1_Id" });
            DropTable("dbo.Tournaments");
            DropTable("dbo.Matches");
            DropTable("dbo.Players");
            DropTable("dbo.PlayerGames");
            DropTable("dbo.Games");
        }
    }
}
