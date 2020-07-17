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
    public class BasketService : IBasketService
    {
        private BasketRepository repository;

        public BasketService()
        {
            repository = new BasketRepository();
        }

        public List<ProductDto> AddItemToBasket(List<Product> basket, int id)
        {
            List<Product> products = repository.AddItemToBasket(basket, id);
            List<ProductDto> productDtos = ProductsToProductDtos(products);
            return productDtos;
        }

        public List<ProductDto> Checkout(List<Product> basket)
        {
            List<Product> products = repository.Checkout(basket);
            List<ProductDto> productDtos = new List<ProductDto>();
            return productDtos;
        }

        public List<ProductDto> RemoveItemFromBasket(List<Product> basket, int id)
        {
            List<Product> products = repository.RemoveItemFromBasket(basket, id);
            List<ProductDto> productDtos = ProductsToProductDtos(products);
            return productDtos;
        }

        public List<ProductDto> ProductsToProductDtos(List<Product> products)
        {
            List<ProductDto> productsDtos = products.Select(p => new ProductDto()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                ManufacturerId = p.Manufacturer.ManufacturerId,
                ManufacturerName = p.Manufacturer.Name,
                StockAmount = p.StockAmount,
                ImagePath = p.ImagePath
            }).ToList();

            return productsDtos;
        }
    }
}
