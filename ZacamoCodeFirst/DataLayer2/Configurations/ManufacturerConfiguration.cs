using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataLayer2.Configurations
{
    public class ManufacturerConfiguration : EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerConfiguration()
        {
            Property(m => m.Name).IsRequired().HasMaxLength(20);
            Property(m => m.Website).IsRequired();
        }
    }
}
