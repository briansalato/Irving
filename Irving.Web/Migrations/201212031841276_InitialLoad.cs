namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialLoad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Year = c.Int(),
                        Make = c.String(),
                        Model = c.String(),
                        Bought = c.DateTime(),
                        InitialMileage = c.Int(),
                        CurrentMileage = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOccured = c.DateTime(),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.Car_Id);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assets", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(),
                        AssetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assets", t => t.AssetId, cascadeDelete: true)
                .Index(t => t.AssetId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Notes", new[] { "AssetId" });
            DropIndex("dbo.Assets", new[] { "ParentId" });
            DropIndex("dbo.Events", new[] { "Car_Id" });
            DropForeignKey("dbo.Notes", "AssetId", "dbo.Assets");
            DropForeignKey("dbo.Assets", "ParentId", "dbo.Assets");
            DropForeignKey("dbo.Events", "Car_Id", "dbo.Cars");
            DropTable("dbo.Notes");
            DropTable("dbo.Assets");
            DropTable("dbo.Events");
            DropTable("dbo.Cars");
        }
    }
}
