using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using ZacamoRepositories;

namespace WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AddressService : IAddressService
    {
        private AddressRepository repository;

        public AddressService()
        {
            repository = new AddressRepository();
        }

        public int AddAddress(Address address)
        {
            return repository.AddAddress(address);
        }

        public int CheckAddress(Address address)
        {
            return repository.CheckAddress(address);
        }

        public Address GetAddressById(int id)
        {
            return repository.GetAddressById(id);
        }
    }
}
