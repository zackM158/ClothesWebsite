using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataLayer2.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(20);
            Property(p => p.Category).IsRequired().HasMaxLength(20);
            Property(p => p.Price).IsRequired();
            Property(p => p.StockAmount).IsRequired();
            Property(p => p.ImagePath).IsRequired();
        }
    }
}
