namespace DataLayer2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSavedItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SavedItems", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SavedItems");
        }
    }
}
