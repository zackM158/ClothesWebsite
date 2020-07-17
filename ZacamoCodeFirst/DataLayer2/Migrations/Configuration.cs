using Entities;

namespace DataLayer2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer2.ProductsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataLayer2.ProductsContext context)
        {

            // Manufacturers

            context.Manufacturers.AddOrUpdate(m => m.Name, new Manufacturer()
            {
                Name = "Asos",
                Website = "www.asos.com"
            });

            context.Manufacturers.AddOrUpdate(m => m.Name, new Manufacturer()
            {
                Name = "Burton",
                Website = "www.burton.com"
            });

            context.Manufacturers.AddOrUpdate(m => m.Name, new Manufacturer()
            {
                Name = "Topman",
                Website = "www.topman.com"
            });

            context.Manufacturers.AddOrUpdate(m => m.Name, new Manufacturer()
            {
                Name = "Vans",
                Website = "www.vans.com"
            });

            context.Manufacturers.AddOrUpdate(m => m.Name, new Manufacturer()
            {
                Name = "Adidas",
                Website = "www.adidas.com"
            });

            context.Manufacturers.AddOrUpdate(m => m.Name, new Manufacturer()
            {
                Name = "Nike",
                Website = "www.nike.com"
            });

            context.Manufacturers.AddOrUpdate(m => m.Name, new Manufacturer()
            {
                Name = "Converse",
                Website = "www.converse.com"
            });

            //Addresses

            context.Addresses.AddOrUpdate(a => a.AddressId, new Address()
            {
                AddressId = 1,
                HouseNumber = 64,
                StreetName = "Zoo Lane",
                City = "Sheffield",
                Postcode = "S101ER"
            });

            context.Addresses.AddOrUpdate(a => a.AddressId, new Address()
            {
                AddressId = 2,
                HouseNumber = 21,
                StreetName = "Abbey Road",
                City = "Liverpool",
                Postcode = "L40TE"
            });

            context.SaveChanges();

            //Shirts

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Green Tshirt",
                Category = "Shirts",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Asos"),
                Price = 19.99,
                StockAmount = 56,
                ImagePath = "tshirtGreen.jpg",
                Description = "100% Cotton, Muscle Fit, Machine Washable, Sustainably Sourced"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Blue Tshirt",
                Category = "Shirts",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Burton"),
                Price = 13.99,
                StockAmount = 73,
                ImagePath = "tshirtBlue.jpg",
                Description = "100% Cotton, Muscle Fit, Machine Washable, Sustainably Sourced"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Raglan Tshirt",
                Category = "Shirts",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Asos"),
                Price = 40,
                StockAmount = 129,
                ImagePath = "tshirtRaglan.jpg",
                Description = "100% Cotton, Muscle Fit, Machine Washable, Sustainably Sourced"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Pink Tshirt",
                Category = "Shirts",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Topman"),
                Price = 30,
                StockAmount = 129,
                ImagePath = "tshirtPink.jpg",
                Description = "100% Cotton, Muscle Fit, Machine Washable, Sustainably Sourced"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Casual Shirt",
                Category = "Shirts",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Topman"),
                Price = 45,
                StockAmount = 320,
                ImagePath = "tshirtCasual.jpg",
                Description = "100% Cotton, Muscle Fit, Machine Washable, Sustainably Sourced"
            });

            //Suits

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Black Suit",
                Category = "Suits",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Topman"),
                Price = 300,
                StockAmount = 44,
                ImagePath = "suitBlack.jpg",
                Description = "50% Cotton 50% Nylon, Dry Clean Only, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Navy Suit",
                Category = "Suits",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Burton"),
                Price = 300,
                StockAmount = 87,
                ImagePath = "suitNavy.jpg",
                Description = "50% Cotton 50% Nylon, Dry Clean Only, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Blue Suit",
                Category = "Suits",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Burton"),
                Price = 199,
                StockAmount = 18,
                ImagePath = "suitBlue.jpg",
                Description = "50% Cotton 50% Nylon, Dry Clean Only, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Grey Suit",
                Category = "Suits",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Asos"),
                Price = 159.99,
                StockAmount = 25,
                ImagePath = "suitGrey.jpg",
                Description = "50% Cotton 50% Nylon, Dry Clean Only, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Checked Suit",
                Category = "Suits",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Topman"),
                Price = 159.99,
                StockAmount = 102,
                ImagePath = "suitChecked.jpg",
                Description = "50% Cotton 50% Nylon, Dry Clean Only, Skinny Fit, 4 Pockets"
            });

            //Trousers

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Blue Jeans",
                Category = "Trousers",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Asos"),
                Price = 40,
                StockAmount = 66,
                ImagePath = "trousersJeans.jpg",
                Description = "100% Denim, Machine Washable, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Blue Ripped Jeans",
                Category = "Trousers",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Topman"),
                Price = 45,
                StockAmount = 65,
                ImagePath = "trousersRippedBlue.jpg",
                Description = "100% Denim, Machine Washable, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Black Jeans",
                Category = "Trousers",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Asos"),
                Price = 35,
                StockAmount = 27,
                ImagePath = "trousersJeansBlack.jpg",
                Description = "100% Denim, Machine Washable, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Black Ripped Jeans",
                Category = "Trousers",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Topman"),
                Price = 45,
                StockAmount = 66,
                ImagePath = "trousersJeansBlackRipped.jpg",
                Description = "100% Denim, Machine Washable, Skinny Fit, 4 Pockets"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Trackies",
                Category = "Trousers",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Adidas"),
                Price = 29.99,
                StockAmount = 76,
                ImagePath = "trousersTrackies.jpg",
                Description = "100% Nylon, Machine Washable, Skinny Fit, 4 Pockets"
            });

            //Shoes

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Air Max",
                Category = "Shoes",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Nike"),
                Price = 120,
                StockAmount = 435,
                ImagePath = "shoesAirMax.jpg",
                Description = "Rubber Soles, Outdoor Safe, Spare Laces, Comfortable Fit"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Jordans",
                Category = "Shoes",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Nike"),
                Price = 160,
                StockAmount = 1,
                ImagePath = "shoesJordans.jpg",
                Description = "Rubber Soles, Outdoor Safe, Spare Laces, Comfortable Fit"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Superstars",
                Category = "Shoes",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Adidas"),
                Price = 90,
                StockAmount = 123,
                ImagePath = "shoesSuperstar.jpg",
                Description = "Rubber Soles, Outdoor Safe, Spare Laces, Comfortable Fit"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "All Stars",
                Category = "Shoes",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Converse"),
                Price = 75,
                StockAmount = 63,
                ImagePath = "shoesConverse.jpg",
                Description = "Rubber Soles, Outdoor Safe, Spare Laces, Comfortable Fit"
            });

            context.Products.AddOrUpdate(p => p.Name, new Product()
            {
                Name = "Old Skool",
                Category = "Shoes",
                Manufacturer = context.Manufacturers.Single(m => m.Name == "Vans"),
                Price = 85,
                StockAmount = 42,
                ImagePath = "shoesOldSkool.jpg",
                Description = "Rubber Soles, Outdoor Safe, Spare Laces, Comfortable Fit"
            });

            //Users
            context.Users.AddOrUpdate(u=>u.UserId, new User()
            {
                UserId = 1,
                FirstName = "Zack",
                LastName = "Mitchell",
                EmailAddress = "zack@zacamo.com",
                AddressId = 1,
                IsAdmin = true,
                IsPremiumUser = true,
                Password = "1bc1a361f17092bc7af4b2f82bf9194ea9ee2ca49eb2e53e39f555bc1eeaed74",
                Salt = "salt"
            });

            context.Users.AddOrUpdate(u => u.UserId, new User()
            {
                UserId = 2,
                FirstName = "Joe",
                LastName = "Bloggs",
                EmailAddress = "joebloggs@gmail.com",
                AddressId = 2,
                IsAdmin = false,
                IsPremiumUser = false,
                Password = "1bc1a361f17092bc7af4b2f82bf9194ea9ee2ca49eb2e53e39f555bc1eeaed74",
                Salt = "salt"
            });

            context.SaveChanges();
        }
    }
}
