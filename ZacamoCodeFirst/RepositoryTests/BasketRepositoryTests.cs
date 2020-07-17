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
    public class BasketRepositoryTests
    {
        private ProductsContext dbBefore;
        private ProductsContext dbAfter;
        private ProductRepository productRepository;
        private BasketRepository service;

        private Product product1;
        private Product product2;
        private Product product3;

        List<Product> basket;

        [SetUp]
        public void SetUp()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            dbBefore = new ProductsContext(connection);
            dbAfter = new ProductsContext(connection);
            ProductsContext dbService = new ProductsContext(connection);
            productRepository = new ProductRepository(dbService);
            service = new BasketRepository(dbService);

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


            dbBefore.SaveChanges();

            product1 = new Product()
            {
                Name = "Product1",
                Category = "Shirts",
                Price = 16.99,
                Manufacturer = new Manufacturer() { Name = "Asos" },
                StockAmount = 45,
                ImagePath = "testPath1.jpg",
                Description = "Description"
            };

            product2 = new Product()
            {
                Name = "Product2",
                Category = "Trousers",
                Price = 34.99,
                Manufacturer = new Manufacturer() { Name = "Burton" },
                StockAmount = 50,
                ImagePath = "testPath2.jpg",
                Description = "Description"
            };

            product3 = new Product()
            {
                Name = "Product3",
                Category = "Shirts",
                Price = 44.99,
                Manufacturer = new Manufacturer() { Name = "Burton" },
                StockAmount = 1,
                ImagePath = "testPath3.jpg",
                Description = "Description"
            };

            productRepository.AddProduct(product1, ".jpg");
            productRepository.AddProduct(product2, ".jpg");
            productRepository.AddProduct(product3, ".jpg");

            basket = new List<Product>();
        }


        [Test]
        public void AddItemsToBasket_AddsProductToList_WhenCalled()
        {
            //Arrange

            //Act
            basket = service.AddItemToBasket(basket, 1);
            basket = service.AddItemToBasket(basket, 2);
            basket = service.AddItemToBasket(basket, 2);


            //Assert
            Assert.AreEqual(3, basket.Count);
            Assert.AreEqual("Product1", basket[0].Name);
            Assert.AreEqual("Product2", basket[1].Name);
            Assert.AreEqual("Product2", basket[2].Name);
        }

        [Test]
        public void AddItemsToBasket_DoesntAddTheProductsToBasketIfStockIsTooLow_WhenCalled()
        {
            //Arrange

            //Act
            basket = service.AddItemToBasket(basket, 3);
            basket = service.AddItemToBasket(basket, 3);
            basket = service.AddItemToBasket(basket, 3);

            //Assert
            Assert.AreEqual(1, basket.Count);
        }

        [Test]
        public void RemoveItemsFromBasket_SuccessfullyRemovesTheProductFromList_WhenCalled()
        {
            //Arrange
            basket = service.AddItemToBasket(basket, 1);
            basket = service.AddItemToBasket(basket, 2);
            basket = service.AddItemToBasket(basket, 3);

            //Act
            basket = service.RemoveItemFromBasket(basket, 3);


            //Assert
            Assert.AreEqual(2, basket.Count);
            Assert.AreEqual(1, basket[0].ProductId);
            Assert.AreEqual(2, basket[1].ProductId);
        }

        [Test]
        public void Checkout_RemovesTheAmountOfProductsFromStock_WhenCalled()
        {
            //Arrange
            service.AddItemToBasket(basket, 1);
            service.AddItemToBasket(basket, 2);
            service.AddItemToBasket(basket, 3);

            //Act
            basket = service.Checkout(basket);

            //Assert
            Assert.AreEqual(0, basket.Count);
            Assert.AreEqual(44, dbAfter.Products.Single(p => p.ProductId == 1).StockAmount);
            Assert.AreEqual(49, dbAfter.Products.Single(p => p.ProductId == 2).StockAmount);
            Assert.AreEqual(0, dbAfter.Products.Single(p => p.ProductId == 3).StockAmount);
        }

    }
}
