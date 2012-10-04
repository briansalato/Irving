namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropertyTypeToAssetTypeProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssetTypeProperties", "PropertyType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssetTypeProperties", "PropertyType");
        }
    }
}
