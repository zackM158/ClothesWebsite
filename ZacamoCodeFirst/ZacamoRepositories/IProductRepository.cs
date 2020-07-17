using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ZacamoRepositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        List<Product> GetAllProductsWithManufacturers();
        int AddProduct(Product product, string fileType);
        string UpdateProduct(Product product);
        List<Product> GetProductsByManufacturer(string manufacturer);
        List<Product> GetProductsByCategory(string category);

        string DeleteProduct(Product product);
        string DeleteProduct(int id);

        List<Product> SearchProducts(string searchTerm);
        List<Product> SavedProducts(string savedProductsString);
    }
}
