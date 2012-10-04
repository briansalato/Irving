namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAssetType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssetTypeProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetTypes", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AssetTypeProperties", new[] { "Owner_Id" });
            DropForeignKey("dbo.AssetTypeProperties", "Owner_Id", "dbo.AssetTypes");
            DropTable("dbo.AssetTypeProperties");
            DropTable("dbo.AssetTypes");
        }
    }
}
