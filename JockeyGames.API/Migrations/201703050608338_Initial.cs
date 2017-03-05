namespace JockeyGames.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        G1P1Score = c.Int(nullable: false),
                        G1P2Score = c.Int(nullable: false),
                        G2P1Score = c.Int(nullable: false),
                        G2P2Score = c.Int(nullable: false),
                        G3P1Score = c.Int(nullable: false),
                        G3P2Score = c.Int(nullable: false),
                        PlayerId1_Id = c.Int(),
                        PlayerId2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId1_Id)
                .ForeignKey("dbo.Players", t => t.PlayerId2_Id)
                .Index(t => t.PlayerId1_Id)
                .Index(t => t.PlayerId2_Id);
            
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
            DropForeignKey("dbo.Matches", "PlayerId2_Id", "dbo.Players");
            DropForeignKey("dbo.Matches", "PlayerId1_Id", "dbo.Players");
            DropIndex("dbo.Matches", new[] { "PlayerId2_Id" });
            DropIndex("dbo.Matches", new[] { "PlayerId1_Id" });
            DropTable("dbo.Players");
            DropTable("dbo.Matches");
        }
    }
}
