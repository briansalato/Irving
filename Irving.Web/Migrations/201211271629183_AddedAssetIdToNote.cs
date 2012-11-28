namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAssetIdToNote : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "AssetFor_Id", "dbo.Assets");
            DropIndex("dbo.Notes", new[] { "AssetFor_Id" });
            AddColumn("dbo.Notes", "AssetId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Notes", "AssetId", "dbo.Assets", "Id", cascadeDelete: true);
            CreateIndex("dbo.Notes", "AssetId");
            DropColumn("dbo.Notes", "AssetFor_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notes", "AssetFor_Id", c => c.Int(nullable: false));
            DropIndex("dbo.Notes", new[] { "AssetId" });
            DropForeignKey("dbo.Notes", "AssetId", "dbo.Assets");
            DropColumn("dbo.Notes", "AssetId");
            CreateIndex("dbo.Notes", "AssetFor_Id");
            AddForeignKey("dbo.Notes", "AssetFor_Id", "dbo.Assets", "Id", cascadeDelete: true);
        }
    }
}
