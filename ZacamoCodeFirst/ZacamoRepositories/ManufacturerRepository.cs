using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer2;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ZacksLibrary;

namespace ZacamoRepositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private ProductsContext context;
        public ManufacturerRepository(ProductsContext db)
        {
            context = db; 
        }

        public ManufacturerRepository()
        {
            context = new ProductsContext();
        }

        public int AddManufacturer(Manufacturer manufacturer)
        {
            manufacturer.Name = ExtraMethods.TitleString(manufacturer.Name);
            manufacturer.Website = manufacturer.Website.ToLower();
            manufacturer.Products = new List<Product>();

            context.Manufacturers.Add(manufacturer);
            context.SaveChanges();
            return manufacturer.ManufacturerId;
        }

        public string DeleteManufacturer(Manufacturer manufacturer)
        {
            return DeleteManufacturer(manufacturer.ManufacturerId);
        }

        public string DeleteManufacturer(int id)
        {
            Manufacturer manufacturer = context.Manufacturers.Find(id);

            try
            {
                if (manufacturer != null)
                {
                    context.Manufacturers.Remove(manufacturer);
                    context.SaveChanges();
                    return "Manufacturer With ID " + id + " Successfully Removed";
                }
                else
                {
                    return "No Manufacturer With ID Of " + id;
                }
            }
            catch (DbUpdateException ex)
            {
                return "Manufacturer has products so can not be deleted";
            }
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return context.Manufacturers.ToList();
        }

        public string UpdateManufacturer(Manufacturer manufacturer)
        {
            Manufacturer manufacturerToUpdate = context.Manufacturers.Find(manufacturer.ManufacturerId);

            manufacturer.Name = ExtraMethods.TitleString(manufacturer.Name);
            manufacturer.Website = manufacturer.Website.ToLower();

            if (manufacturerToUpdate != null)
            {
                manufacturerToUpdate.Name = manufacturer.Name;
                manufacturerToUpdate.Website = manufacturer.Website;
                context.SaveChanges();
                return "Updated Manufacturer With ID " + manufacturer.ManufacturerId;
            }
            else
            {
                return "There Is No Manufacturer With ID Of " + manufacturer.ManufacturerId;
            }
        }

        public Manufacturer GetManufacturerById(int id)
        {
            return context.Manufacturers.Find(id);
        }
    }
}
