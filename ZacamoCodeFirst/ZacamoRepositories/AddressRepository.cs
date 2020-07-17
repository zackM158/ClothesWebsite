using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer2;
using ZacksLibrary;

namespace ZacamoRepositories
{
    public class AddressRepository : IAddressRepository
    {
        private ProductsContext context;

        public AddressRepository()
        {
            context = new ProductsContext();
        }

        public AddressRepository(ProductsContext db)
        {
            context = db;

        }

        public Address GetAddressById(int id)
        {
            return context.Addresses.Find(id);
        }

        public int AddAddress(Address address)
        {
            address.StreetName = ExtraMethods.TitleString(address.StreetName);
            address.City = ExtraMethods.TitleString(address.City);

            address.Postcode = address.Postcode.Replace(" ", "").ToUpper();

            int addressId = CheckAddress(address);

            if (addressId != 0)
            {
                return addressId;
            }

            context.Addresses.Add(address);
            context.SaveChanges();
            return address.AddressId;
        }

        public int CheckAddress(Address address)
        {
            Address foundAddress = context.Addresses.SingleOrDefault(a =>
                a.HouseNumber == address.HouseNumber && a.StreetName == address.StreetName && a.City == address.City &&
                a.Postcode == address.Postcode);

            if (foundAddress == null)
            {
                return 0;
            }

            return foundAddress.AddressId;
        }
    }
}
