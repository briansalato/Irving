namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastOccurance = c.DateTime(),
                        NextOccurance = c.DateTime(),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Recurrence_Id = c.Int(nullable: false),
                        Asset_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recurrences", t => t.Recurrence_Id, cascadeDelete: true)
                .ForeignKey("dbo.Assets", t => t.Asset_Id)
                .Index(t => t.Recurrence_Id)
                .Index(t => t.Asset_Id);
            
            CreateTable(
                "dbo.Recurrences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Events", new[] { "Asset_Id" });
            DropIndex("dbo.Events", new[] { "Recurrence_Id" });
            DropForeignKey("dbo.Events", "Asset_Id", "dbo.Assets");
            DropForeignKey("dbo.Events", "Recurrence_Id", "dbo.Recurrences");
            DropTable("dbo.Recurrences");
            DropTable("dbo.Events");
        }
    }
}
