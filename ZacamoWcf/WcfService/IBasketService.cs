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
    public interface IBasketService
    {
        [OperationContract]
        List<ProductDto> AddItemToBasket(List<Product> basket, int id);

        [OperationContract]
        List<ProductDto> RemoveItemFromBasket(List<Product> basket, int id);

        [OperationContract]
        List<ProductDto> Checkout(List<Product> basket);
    }
}
