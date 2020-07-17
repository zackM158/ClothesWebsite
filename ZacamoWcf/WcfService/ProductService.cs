using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZacamoRepositories;
using Entities;
using System.ServiceModel.Activation;

namespace WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProductService : IProductService
    {
        private ProductRepository repository;
        private List<string> Categories = new List<string>() { "Shirts", "Suits", "Shoes", "Trousers" };

        public ProductService()
        {
            repository = new ProductRepository();
        }

        public int AddProduct(Product product, string fileType)
        {
            try
            {
                return repository.AddProduct(product, fileType);
            }
            catch
            {
                return 0;
            }
        }

        public string DeleteProduct(int id)
        {
            return repository.DeleteProduct(id);
        }

        public List<ProductDto> GetAllProducts()
        {
            List<Product> products = repository.GetAllProducts();

            return ProductsToProductDtos(products);
        }

        public List<ProductDto> GetAllProductsWithManufacturers()
        {
            List<Product> products = repository.GetAllProductsWithManufacturers();

            return ProductsToProductDtos(products);
        }

        public ProductDto GetProductById(int id)
        {
            Product product = repository.GetProductById(id);

            if (product == null)
                return null;

            ProductDto productDto = new ProductDto()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Category = product.Category,
                ManufacturerId = product.Manufacturer.ManufacturerId,
                ManufacturerName = product.Manufacturer.Name,
                StockAmount = product.StockAmount,
                ImagePath = product.ImagePath,
                Price = product.Price,
                Description = product.Description
            };

            return productDto;
        }

        public List<ProductDto> GetProductsByCategory(string category)
        {
            List<Product> products = repository.GetProductsByCategory(category);

            return ProductsToProductDtos(products);
        }

        public List<ProductDto> GetProductsByManufacturer(string manufacturer)
        {
            List<Product> products = repository.GetProductsByManufacturer(manufacturer);

            return ProductsToProductDtos(products);
        }

        public List<ProductDto> SearchProducts(string searchTerm)
        {
            List<Product> products = repository.SearchProducts(searchTerm);

            return ProductsToProductDtos(products);
        }

        public List<ProductDto> SavedProducts(string savedProductsString)
        {
            List<Product> products = repository.SavedProducts(savedProductsString);

            return ProductsToProductDtos(products);
        }

        public string UpdateProduct(Product product)
        {
            return repository.UpdateProduct(product);
        }

        public string ValidCategory(string initalCategory)
        {
            if (initalCategory.Length >= 4)
            {
                foreach (string category in Categories)
                {
                    if (category.ToLower().StartsWith(initalCategory.ToLower().Substring(0, 4)))
                    {
                        return category;
                    }
                }
            }

            return null;
        }

        List<ProductDto> ProductsToProductDtos(List<Product> products)
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
                ImagePath = p.ImagePath,
                Description = p.Description
            }).ToList();

            return productsDtos;
        }
    }
}
