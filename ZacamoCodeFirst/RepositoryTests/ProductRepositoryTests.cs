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
    public class ProductRepositoryTests
    {
        private ProductsContext dbBefore;
        private ProductsContext dbAfter;
        private ProductRepository service;
        private ManufacturerRepository manufacturerRepository;

        private Product product1;
        private Product product2;
        private Product product3;
        private string description;

        [SetUp]
        public void SetUp()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            dbBefore = new ProductsContext(connection);
            dbAfter = new ProductsContext(connection);
            ProductsContext dbService = new ProductsContext(connection);
            service = new ProductRepository(dbService);
            manufacturerRepository = new ManufacturerRepository(dbService);

            Manufacturer manufacturer1 = dbBefore.Manufacturers.Add(new Manufacturer()
            {
                Name = "Asos",
                Website = "www.asos.com"
            });

            Manufacturer manufacturer2 = dbBefore.Manufacturers.Add(new Manufacturer()
            {
                Name = "Burton",
                Website = "www.burton.com"
            });

            description = "Description 1, Description 2, Desctription 3";

            dbBefore.SaveChanges();

            string product1Name = "product1";
            string product1Category = "shirt";
            double poduct1Price = 16.9911;

            product1 = new Product()
            {
                Name = product1Name,
                Category = product1Category,
                Price = poduct1Price,
                ManufacturerId = 1,
                StockAmount = 100,
                ImagePath = "testPath1.jpg",
                Description = description
            };

            string product2Name = "Product2";
            string product2Category = "Trousers";
            double product2Price = 34.99;

            product2 = new Product()
            {
                Name = product2Name,
                Category = product2Category,
                Price = product2Price,
                ManufacturerId = 2,
                StockAmount = 50,
                ImagePath = "testPath2.jpg",
                Description = description
            };

            string product3Name = "Product3";
            string product3Category = "Shirts";
            double product3Price = 44.99;

            product3 = new Product()
            {
                Name = product3Name,
                Category = product3Category,
                Price = product3Price,
                ManufacturerId = 2,
                StockAmount = 105,
                ImagePath = "testPath3.jpg",
                Description = description
            };
        }

        [Test]
        public void AddProduct_AddsAProductToDatabaseInCorrectFormat_WhenCalled()
        {
            //Arrange

            //Act
            int product1Id = service.AddProduct(product1, ".jpg");

            //Assert
            Product product = dbAfter.Products.Find(product1Id);
            Assert.That(product, Is.Not.Null);
            Assert.AreEqual("Product1", product.Name);
            Assert.AreEqual("Shirts", product.Category);
            Assert.AreEqual("Asos", product.Manufacturer.Name);
            Assert.AreEqual(100, product.StockAmount);
            Assert.AreEqual("Shirts_ID_1.jpg", product.ImagePath);
            Assert.AreEqual(description, product.Description);
            Assert.AreEqual(16.99, product.Price);
        }

        [Test]
        public void GetAllProducts_GetsAListOfAllProductsInDatabase_WhenCalled()
        {
            //Arrange
           int product1Id = service.AddProduct(product1, ".jpg");
           int product2Id = service.AddProduct(product2, ".jpg");
           int product3Id = service.AddProduct(product3, ".jpg");

            //Act
            List<Product> allProducts = service.GetAllProducts();

            //Assert
            Assert.AreEqual(3, allProducts.Count);
            Assert.AreEqual("Product1", allProducts[0].Name);
            Assert.AreEqual("Shirts", allProducts[0].Category);
            Assert.AreEqual("Asos", allProducts[0].Manufacturer.Name);
            Assert.AreEqual(16.99, allProducts[0].Price); 
            Assert.AreEqual(100, allProducts[0].StockAmount);
            Assert.AreEqual("Shirts_ID_1.jpg", allProducts[0].ImagePath);
            Assert.AreEqual(description, allProducts[0].Description);

            Assert.AreEqual(product2.Name, allProducts[1].Name);
            Assert.AreEqual(product2.Category, allProducts[1].Category);
            Assert.AreEqual(product2.Manufacturer.Name, allProducts[1].Manufacturer.Name);
            Assert.AreEqual(product2.Price, allProducts[1].Price);
            Assert.AreEqual(50, allProducts[1].StockAmount);
            Assert.AreEqual("Trousers_ID_2.jpg", allProducts[1].ImagePath);
            Assert.AreEqual(description, allProducts[1].Description);

            Assert.AreEqual(product3.Name, allProducts[2].Name);
            Assert.AreEqual(product3.Category, allProducts[2].Category);
            Assert.AreEqual(product3.Manufacturer.Name, allProducts[2].Manufacturer.Name);
            Assert.AreEqual(product3.Price, allProducts[2].Price);
            Assert.AreEqual(105, allProducts[2].StockAmount);
            Assert.AreEqual("Shirts_ID_3.jpg", allProducts[2].ImagePath);
            Assert.AreEqual(description, allProducts[2].Description);
        }

        [Test]
        public void UpdateProduct_ChangesProductDetailsInDatabase_WhenCalled()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            Product productUpdate = new Product()
            {
                ProductId = product1Id,
                Name = product2.Name,
                Category = product2.Category,
                ManufacturerId = product2.ManufacturerId,
                Price = product2.Price,
                StockAmount = product2.StockAmount,
                ImagePath = "Shirts_ID_1.jpg",
                Description = description
            };

            //Act
            service.UpdateProduct(productUpdate);

            //Assert
            Product productAfter = dbAfter.Products.Find(product1Id);

            Assert.That(productAfter, Is.Not.Null);
            Assert.AreEqual(product2.Name, productAfter.Name);
            Assert.AreEqual(product2.Category, productAfter.Category);
            Assert.AreEqual("Burton", productAfter.Manufacturer.Name);
            Assert.AreEqual(product2.Price, productAfter.Price); 
            Assert.AreEqual(product2.StockAmount, productAfter.StockAmount);
            Assert.AreEqual("Shirts_ID_1.jpg", productAfter.ImagePath);

        }

        [Test]
        public void GetProductsByManufacturer_GetsAListOfAllProductsFromThatManufacturerInDatabase_WhenCalled()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            int product2Id = service.AddProduct(product2, ".jpg");
            int product3Id = service.AddProduct(product3, ".jpg");
            string manufacturer = "Burton";

            //Act
            List<Product> productsFromManufacturer = service.GetProductsByManufacturer(manufacturer);


            //Assert
            Assert.AreEqual(2, productsFromManufacturer.Count);
            foreach (Product product in productsFromManufacturer)
            {
                Assert.AreEqual("Burton", product.Manufacturer.Name);
            }


        }

        [Test]
        public void GetProductsByCategory_GetsAListOfAllProductsFromThatCategoryInDatabase_WhenCalled()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            int product2Id = service.AddProduct(product2, ".jpg");
            int product3Id = service.AddProduct(product3, ".jpg");
            string category = "shirt";

            //Act
            List<Product> productsInCategory = service.GetProductsByCategory(category);


            //Assert
            Assert.AreEqual(2, productsInCategory.Count);
            foreach (Product product in productsInCategory)
            {
                Assert.AreEqual("Shirts", product.Category);
            }
        }

        [Test]
        public void DeleteProduct_RemovesProductFromDatabase_WhenCalled()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");

            //Act
            service.DeleteProduct(product1Id);


            //Assert
            Product productAfter = dbAfter.Products.Find(product1Id);
            Assert.That(productAfter, Is.Null);
        }

        [Test]
        public void GetProductById_RetrievesReleventProductFromDatabase_WhenCalled()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");

            //Act
            Product productFromDatabase = service.GetProductById(product1Id);

            //Assert
            Assert.That(productFromDatabase, Is.Not.Null);
            Assert.AreEqual(productFromDatabase.Name, product1.Name);
            Assert.AreEqual(productFromDatabase.Category, product1.Category);
            Assert.AreEqual(productFromDatabase.Price, product1.Price);
            Assert.AreEqual(productFromDatabase.StockAmount, product1.StockAmount);
            Assert.AreEqual(productFromDatabase.ManufacturerId, product1.ManufacturerId);
            Assert.AreEqual("Shirts_ID_1.jpg", product1.ImagePath);
        }

        [Test]
        public void GetAllProductsWithManufacturers_RetrievesProductsWithManufacturersFromDatabase_WhenCalled()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            int product2Id = service.AddProduct(product2, ".jpg");
            int product3Id = service.AddProduct(product3, ".jpg");

            //Act
            List<Product> allProducts = service.GetAllProductsWithManufacturers();

            //Assert
            Assert.AreEqual(3, allProducts.Count);
            Assert.AreEqual("Asos", allProducts[0].Manufacturer.Name);
            Assert.AreEqual(product2.Manufacturer.Name, allProducts[1].Manufacturer.Name);
            Assert.AreEqual(product3.Manufacturer.Name, allProducts[2].Manufacturer.Name);
        }

        [Test]
        public void SearchProducts_RetrievesTheProductsFromManufacturer_WhenSearchingForManufacturer()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            int product2Id = service.AddProduct(product2, ".jpg");
            int product3Id = service.AddProduct(product3, ".jpg");

            //Act
            List<Product> searchedProducts = service.SearchProducts("burton");

            //Assert
            Assert.AreEqual(2, searchedProducts.Count);
            Assert.AreEqual("Product2", searchedProducts[0].Name);
            Assert.AreEqual("Product3", searchedProducts[1].Name);
        }

        [Test]
        public void SearchProducts_RetrievesTheProductsFromCategory_WhenSearchingForCategory()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            int product2Id = service.AddProduct(product2, ".jpg");
            int product3Id = service.AddProduct(product3, ".jpg");

            //Act
            List<Product> searchedProducts = service.SearchProducts("shirt");

            //Assert
            Assert.AreEqual(2, searchedProducts.Count);
            Assert.AreEqual("Product1", searchedProducts[0].Name);
            Assert.AreEqual("Product3", searchedProducts[1].Name);
        }

        [Test]
        public void SearchProducts_RetrievesTheProductsThatContainSearchTerm_WhenSearching()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            int product2Id = service.AddProduct(product2, ".jpg");
            int product3Id = service.AddProduct(product3, ".jpg");

            //Act
            List<Product> searchedProducts = service.SearchProducts("pro");

            //Assert
            Assert.AreEqual(3, searchedProducts.Count);
            Assert.AreEqual("Product1", searchedProducts[0].Name);
            Assert.AreEqual("Product2", searchedProducts[1].Name);
            Assert.AreEqual("Product3", searchedProducts[2].Name);
        }

        [Test]
        public void SavedProducts_RetrievesAListOfAllSavedProducts_WhenCalled()
        {
            //Arrange
            int product1Id = service.AddProduct(product1, ".jpg");
            int product2Id = service.AddProduct(product2, ".jpg");
            int product3Id = service.AddProduct(product3, ".jpg");

            //Act
            List<Product> savedProducts = service.SavedProducts("1,3");

            //Assert
            Assert.AreEqual(2, savedProducts.Count);
            Assert.AreEqual("Product1", savedProducts[0].Name);
            Assert.AreEqual("Product3", savedProducts[1].Name);
        }
    }
}
