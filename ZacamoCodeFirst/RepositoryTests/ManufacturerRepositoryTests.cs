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
    public class ManufacturerRepositoryTests
    {
        private ProductsContext dbBefore;
        private ProductsContext dbAfter;
        private ManufacturerRepository service;

        [SetUp]
        public void SetUp()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            dbBefore = new ProductsContext(connection);
            dbAfter = new ProductsContext(connection);
            ProductsContext dbService = new ProductsContext(connection);
            service = new ManufacturerRepository(dbService);
        }

        [Test]
        public void AddManufacturer_AddsAManufacturerInCorrectFormToDatabase_WhenCalled()
        {
            //Arrange
            string name = "asos";
            string website = "www.ASos.com";
            
            Manufacturer newManufacturer = new Manufacturer()
            {
                Name = name,
                Website = website
            };

            //Act
            int manufacturerId = service.AddManufacturer(newManufacturer);

            //Assert
            Manufacturer manufacturer  = dbAfter.Manufacturers.Find(manufacturerId);
            Assert.That(manufacturer, Is.Not.Null);
            Assert.AreEqual("Asos",manufacturer.Name);
            Assert.AreEqual("www.asos.com",manufacturer.Website);
        }

        [Test]
        public void DeleteManufacturer_DeletesManufacturerFromDatabase_WhenCalled()
        {
            //Arrange
            Manufacturer manufacturer = dbBefore.Manufacturers.Add(new Manufacturer()
            {
                Name = "manufacturer",
                Website = "www.manufacturer.com"
            });

            dbBefore.SaveChanges();

            //Act
            service.DeleteManufacturer(manufacturer.ManufacturerId);


            //Assert
            Manufacturer manufacturerAfter = dbAfter.Manufacturers.Find(manufacturer.ManufacturerId);
            Assert.That(manufacturerAfter, Is.Null);
        }

        [Test]
        public void GetAllManufacturers_ReturnsAListOfAllManufacturersInDatabase_WhenCalled()
        {
            //Arrange
            Manufacturer manufacturer1 = dbBefore.Manufacturers.Add(new Manufacturer()
            {
                Name = "Manufacturer1",
                Website = "www.manufacturer1.com"
            });

            Manufacturer manufacturer2 = dbBefore.Manufacturers.Add(new Manufacturer()
            {
                Name = "Manufacturer2",
                Website = "www.manufacturer2.com"
            });

            Manufacturer manufacturer3 = dbBefore.Manufacturers.Add(new Manufacturer()
            {
                Name = "Manufacturer3",
                Website = "www.manufacturer3.com"
            });

            dbBefore.SaveChanges();

            //Act
            List<Manufacturer> manufacturers = service.GetAllManufacturers();

            //Assert
            Assert.AreEqual(3, manufacturers.Count); 
            Assert.AreEqual("Manufacturer1",manufacturers[0].Name);
            Assert.AreEqual("www.manufacturer1.com",manufacturers[0].Website);
            Assert.AreEqual("Manufacturer2",manufacturers[1].Name);
            Assert.AreEqual("www.manufacturer2.com", manufacturers[1].Website);
            Assert.AreEqual("Manufacturer3",manufacturers[2].Name);
            Assert.AreEqual("www.manufacturer3.com", manufacturers[2].Website);

        }

        [Test]
        public void UpdateManufacturer_ChangesManufacturerDetailsInDatabase_WhenCalled()
        {
            //Arrange
            string manufacturerAName = "Manufacturer A";
            string manufacturerBName = "Manufacturer B";

            string websiteBefore = "www.MANUFACTURERA.com";
            string websiteAfter = "www.MANUFACTURERB.com";

            Manufacturer manufacturerA = dbBefore.Manufacturers.Add(new Manufacturer()
            {
                Name = manufacturerAName,
                Website = websiteBefore
            });

            dbBefore.SaveChanges();

            //Act
            service.UpdateManufacturer(new Manufacturer()
            {
                ManufacturerId = manufacturerA.ManufacturerId,
                Name = manufacturerBName,
                Website = websiteAfter
            });

            //Assert
            Manufacturer manufacturerAfter = dbAfter.Manufacturers.Find(manufacturerA.ManufacturerId);

            Assert.That(manufacturerAfter, Is.Not.Null);
            Assert.AreEqual(manufacturerBName, manufacturerAfter.Name);
            Assert.AreEqual(websiteAfter.ToLower(), manufacturerAfter.Website);
        }
    }
}
