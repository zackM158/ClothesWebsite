using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.ServiceModel;

namespace WcfService
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<ProductDto> GetAllProducts();

        [OperationContract]
        List<ProductDto> GetAllProductsWithManufacturers();

        [OperationContract]
        int AddProduct(Product product, string fileType);

        [OperationContract]
        string UpdateProduct(Product product);

        [OperationContract]
        List<ProductDto> GetProductsByManufacturer(string manufacturer);

        [OperationContract]
        List<ProductDto> GetProductsByCategory(string category);

        [OperationContract]
        string DeleteProduct(int id);

        [OperationContract]
        string ValidCategory(string initalCategory);

        [OperationContract]
        ProductDto GetProductById(int id);

        [OperationContract]
        List<ProductDto> SearchProducts(string searchTerm);

        [OperationContract]
        List<ProductDto> SavedProducts(string savedProductsString);
    }
}
