namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameToAsset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assets", "Name");
        }
    }
}
