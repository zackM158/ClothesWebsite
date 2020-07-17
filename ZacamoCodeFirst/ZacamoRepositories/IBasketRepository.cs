using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ZacamoRepositories
{
    public interface IBasketRepository
    {
        List<Product> AddItemToBasket(List<Product> basket, int id);
        List<Product> RemoveItemFromBasket(List<Product> basket, int id);
        List<Product> Checkout(List<Product> basket);
    }
}
