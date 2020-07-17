using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace WcfService
{
    [ServiceContract]
    public interface IAddressService
    {
        [OperationContract]
        int AddAddress(Address address);

        [OperationContract]
        int CheckAddress(Address address);

        [OperationContract]
        Address GetAddressById(int id);
    }
}
