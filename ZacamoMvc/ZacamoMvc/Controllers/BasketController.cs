using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Entities;
using ZacamoMvc.ManufacturerServiceReference;
using ZacamoMvc.BasketServiceReference;
using ZacamoMvc.Models;
using ZacamoMvc.ProductServiceReference;
using ProductDto = ZacamoMvc.ProductServiceReference.ProductDto;

namespace ZacamoMvc.Controllers
{
    [Authorize, HandleError]
    public class BasketController : Controller
    {
        public static List<Product> basket = new List<Product>();
        private BasketServiceClient basketRepository;
        private ProductServiceClient productRepository;
        private ManufacturerServiceClient manufacturerRepository;

        public BasketController()
        {
            basketRepository = new BasketServiceClient();
            productRepository = new ProductServiceClient();
            manufacturerRepository = new ManufacturerServiceClient();
        }

        private List<Product> ProductDtosToProducts(List<BasketServiceReference.ProductDto> productDtos)
        {
            List<ManufacturerDto> manufacturerDtos = manufacturerRepository.GetAllManufacturers();

            List<Manufacturer> manufacturers = manufacturerDtos.Select(m => new Manufacturer()
            {
                ManufacturerId = m.ManufacturerId,
                Name = m.Name,
                Website = m.Website
            }).ToList();

            List<Product> products = productDtos.Select(p => new Product()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Category = p.Category,
                Manufacturer = manufacturers.Single(m => m.ManufacturerId == p.ManufacturerId),
                ImagePath = p.ImagePath,
                Price = p.Price,
                StockAmount = p.StockAmount

            }).ToList();

            return products;
        }

        // GET: Basket
        public ActionResult Index()
        {
            double totalPrice = 0;
            basket.ForEach(p => totalPrice += p.Price);
            ViewBag.TotalPrice = totalPrice;
            return View(basket);
        }

        public ActionResult AddItemToBasket(int id)
        {
            List<Product> sameItemInBasket = basket.FindAll(p => p.ProductId == id);

            if (sameItemInBasket.Count > 0)
            {
                if (sameItemInBasket[0].StockAmount <= sameItemInBasket.Count)
                {
                    ModelState.AddModelError("", "Not Enough Stock");
                    return RedirectToAction("Details", "Product", new {id = id});
                }
            }

            List<BasketServiceReference.ProductDto> basketDto = basketRepository.AddItemToBasket(basket, id);
            basket = ProductDtosToProducts(basketDto);

            return RedirectToAction("Index", "Product");
        }

        public ActionResult Checkout()
        {
            var identity = (ClaimsIdentity)User.Identity;
            ViewBag.FirstName = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
            ViewBag.LastName = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;

            string[] addressArray = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.StreetAddress).Value.Split();
            string streetName = addressArray[1];
            for (int i = 2; i < addressArray.Length; i++)
            {
                streetName += " " + addressArray[i];
            }
            ViewBag.HouseNumber = addressArray[0];
            ViewBag.StreetName = streetName;
            ViewBag.City = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Locality).Value;
            ViewBag.Postcode = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.PostalCode).Value;
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(CreditCardModel cardModel)
        {
            try
            {
                List<BasketServiceReference.ProductDto> basketDto = basketRepository.Checkout(basket);
                basket = ProductDtosToProducts(basketDto);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult RemoveFromBasket(int id)
        {
            try
            {
                List<BasketServiceReference.ProductDto> basketDto = basketRepository.RemoveItemFromBasket(basket, id);
                basket = ProductDtosToProducts(basketDto);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}