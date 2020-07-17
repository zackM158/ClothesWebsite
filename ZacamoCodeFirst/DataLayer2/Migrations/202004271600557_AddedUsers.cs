namespace DataLayer2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        EmailAddress = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false),
                        Salt = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        IsPremiumUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.EmailAddress, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "EmailAddress" });
            DropTable("dbo.Users");
        }
    }
}
