using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataLayer2.Configurations;
using Entities;
using System.Data.Common;

namespace DataLayer2
{
    public class ProductsContext : DbContext
    {
        public ProductsContext() : base("Zacamo")
        {
            
        }

        public ProductsContext(DbConnection connection)
            : base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ManufacturerConfiguration());
            modelBuilder.Entity<Product>().HasRequired(p => p.Manufacturer).WithMany(m => m.Products).WillCascadeOnDelete(false);
        }
    }
}
