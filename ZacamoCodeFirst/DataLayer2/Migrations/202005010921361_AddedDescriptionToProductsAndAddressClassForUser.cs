namespace DataLayer2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDescriptionToProductsAndAddressClassForUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        HouseNumber = c.Int(nullable: false),
                        StreetName = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId);
            
            AddColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Users", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "AddressId");
            AddForeignKey("dbo.Users", "AddressId", "dbo.Addresses", "AddressId", cascadeDelete: true);
            DropColumn("dbo.Users", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Address", c => c.String(nullable: false, maxLength: 250));
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropColumn("dbo.Users", "AddressId");
            DropColumn("dbo.Products", "Description");
            DropTable("dbo.Addresses");
        }
    }
}
