using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZacamoRepositories;
using Entities;
using System.ServiceModel.Activation;

namespace WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ManufacturerService : IManufacturerService
    {
        private ManufacturerRepository repository;

        public ManufacturerService()
        {
            repository = new ManufacturerRepository();
        }

        public int AddManufacturer(Manufacturer manufacturer)
        {
            return repository.AddManufacturer(manufacturer);
        }

        public string DeleteManufacturer(int id)
        {
            return repository.DeleteManufacturer(id);
        }

        public List<ManufacturerDto> GetAllManufacturers()
        {
            List<Manufacturer> manufacturers = repository.GetAllManufacturers();

            List<ManufacturerDto> manufacturersDtos = manufacturers.Select(m => new ManufacturerDto()
            {
                ManufacturerId = m.ManufacturerId,
                Name = m.Name,
                Website = m.Website,
                AmountOfProducts = m.Products.Count
            }).ToList();

            return manufacturersDtos;
        }

        public ManufacturerDto GetManufacturerById(int id)
        {
            Manufacturer manufacturer = repository.GetManufacturerById(id);

            if (manufacturer == null)
                return null;

            ManufacturerDto manufacturerDto = new ManufacturerDto()
            {
                ManufacturerId = manufacturer.ManufacturerId,
                Name = manufacturer.Name,
                Website = manufacturer.Website,
                AmountOfProducts = manufacturer.Products.Count
            };

            return manufacturerDto;
        }

        public string UpdateManufacturer(Manufacturer manufacturer)
        {
            return repository.UpdateManufacturer(manufacturer);
        }
    }
}
