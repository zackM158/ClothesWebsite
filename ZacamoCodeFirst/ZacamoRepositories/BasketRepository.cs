using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer2;
using System.Data.Entity;
using Entities;

namespace ZacamoRepositories
{
    public class BasketRepository : IBasketRepository
    {
        private ProductsContext context;

        public BasketRepository()
        {
            context = new ProductsContext(); 
        }

        public BasketRepository(ProductsContext db)
        {
            context = db;

        }

        public List<Product> AddItemToBasket(List<Product> basket, int id)
        {
            Product product = context.Products.Find(id);

            int amountOfProductInBasket = basket.FindAll(p => p.ProductId == id).Count;

            if (product != null && product.StockAmount - amountOfProductInBasket > 0)
            {
                basket.Add(product);
            }

            return basket;

        }


        public List<Product> RemoveItemFromBasket(List<Product> basket, int id)
        {
            Product productToRemove = basket.FirstOrDefault(p => p.ProductId == id);

            if (productToRemove != null)
            {
                basket.Remove(productToRemove);
            }

            return basket;
        }

        public List<Product> Checkout(List<Product> basket)
        {
            int totalProducts = basket.Count;
            double totalPrice = 0;

            foreach (var item in basket)
            {
                totalPrice += item.Price;
                Product product = context.Products.Find(item.ProductId);
                product.StockAmount -= 1;
            }

            context.SaveChanges();

            basket = new List<Product>();
            return basket;
            
        }
    }
}
