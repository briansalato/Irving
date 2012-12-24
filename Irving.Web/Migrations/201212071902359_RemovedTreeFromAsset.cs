namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTreeFromAsset : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assets", "ParentId", "dbo.Assets");
            DropIndex("dbo.Assets", new[] { "ParentId" });
            DropColumn("dbo.Assets", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assets", "ParentId", c => c.Int());
            CreateIndex("dbo.Assets", "ParentId");
            AddForeignKey("dbo.Assets", "ParentId", "dbo.Assets", "Id");
        }
    }
}
