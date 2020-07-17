namespace DataLayer2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddressToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Address", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Address");
        }
    }
}
