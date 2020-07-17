using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IManufacturerService
    {
        [OperationContract]
        List<ManufacturerDto> GetAllManufacturers();

        [OperationContract]
        int AddManufacturer(Manufacturer manufacturer);

        [OperationContract]
        string UpdateManufacturer(Manufacturer manufacturer);

        [OperationContract]
        string DeleteManufacturer(int id);

        [OperationContract]
        ManufacturerDto GetManufacturerById(int id);
    }
}
