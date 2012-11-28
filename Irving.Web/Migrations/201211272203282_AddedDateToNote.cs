namespace Irving.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateToNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "Date");
        }
    }
}
