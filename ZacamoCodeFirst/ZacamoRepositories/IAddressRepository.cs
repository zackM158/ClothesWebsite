using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ZacamoRepositories
{
    public interface IAddressRepository
    {
        int CheckAddress(Address address);
        int AddAddress(Address address);
        Address GetAddressById(int id);
    }
}
