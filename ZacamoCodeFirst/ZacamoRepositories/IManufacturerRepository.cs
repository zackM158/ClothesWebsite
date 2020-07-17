using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ZacamoRepositories
{
    public interface IManufacturerRepository
    {
        List<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerById(int id);
        int AddManufacturer(Manufacturer manufacturer);
        string UpdateManufacturer(Manufacturer manufacturer);

        string DeleteManufacturer(Manufacturer manufacturer);
        string DeleteManufacturer(int id);
    }
}
