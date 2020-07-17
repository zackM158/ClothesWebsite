using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer2;
using Entities;
using NUnit.Framework;
using ZacamoRepositories;

namespace RepositoryTests
{
    [TestFixture]
    public class AddressRepositoryTests
    {
        private ProductsContext dbBefore;
        private ProductsContext dbAfter;
        private ProductRepository service;
        private AddressRepository addressRepository;

        Address address1;
        Address address2;

        [SetUp]
        public void Setup()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            dbBefore = new ProductsContext(connection);
            dbAfter = new ProductsContext(connection);
            ProductsContext dbService = new ProductsContext(connection);
            service = new ProductRepository(dbService);
            addressRepository = new AddressRepository(dbService);

            address1 = new Address
            {
                HouseNumber = 64,
                StreetName = "zoo lane",
                City = "sheffield",
                Postcode = "s10 1er"
            };

            address2 = new Address
            {
                HouseNumber = 21,
                StreetName = "West Street",
                City = "Leeds",
                Postcode = "LE1 4NQ"
            };
        }

        [Test]
        public void AddAddress_returnsAddressId_WhenAddressIsAlreadyInDatabase()
        {
            //Arrange

            //Act
            int address1Id = addressRepository.AddAddress(address1);

            //Assert
            int address1IdAfter = addressRepository.AddAddress(address1);
            Assert.AreEqual(1, address1IdAfter);
        }

        [Test]
        public void AddAddress_AddsAddressToDatabase_WhenAddressIsNotInDatabase()
        {
            //Arrange

            //Act
            int address1Id = addressRepository.AddAddress(address1);

            //Assert
            Address address = dbAfter.Addresses.Find(address1Id);
            Assert.That(address, Is.Not.Null);
            Assert.AreEqual(64, address.HouseNumber);
            Assert.AreEqual("Zoo Lane", address.StreetName);
            Assert.AreEqual("Sheffield", address.City);
            Assert.AreEqual("S101ER", address.Postcode);

        }

        [Test]
        public void GetAddressById_RetrievesTheRightAddress_WhenCalled()
        {
            //Arrange
            int address1Id = addressRepository.AddAddress(address1);
            int address2Id = addressRepository.AddAddress(address2);

            //Act
            Address address = addressRepository.GetAddressById(address2Id);

            //Assert
            Assert.That(address, Is.Not.Null);
            Assert.AreEqual(21, address.HouseNumber);
            Assert.AreEqual("West Street", address.StreetName);
            Assert.AreEqual("Leeds", address.City);
            Assert.AreEqual("LE14NQ", address.Postcode);
        }

        [Test]
        public void CheckAddress_returns0_WhenAddressIsNotInDatabase()
        {
            //Arrange
            int address1Id = addressRepository.AddAddress(address1);

            //Act
            int checkAddress = addressRepository.CheckAddress(address2);

            //Assert
            Assert.AreEqual(0, checkAddress);
        }

        [Test]
        public void CheckAddress_returnsAddressId_WhenAddressIsInDatabase()
        {
            //Arrange
            int address1Id = addressRepository.AddAddress(address1);
            int address2Id = addressRepository.AddAddress(address2);

            //Act
            int checkAddress = addressRepository.CheckAddress(address2);

            //Assert
            Assert.AreEqual(2, checkAddress);

        }
    }
}
