using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WcfService;
using System.Net;
using Entities;
using ZacksLibrary;
using WcfConsoleClient.ManufacturerServiceReference;
using WcfConsoleClient.ProductServiceReference;
using WcfConsoleClient.BasketServiceReference;

namespace WcfConsoleClient
{
    public class Menu
    {
        private WcfConsoleClient.ManufacturerServiceReference.IManufacturerService manufacturerProxy;
        private WcfConsoleClient.ProductServiceReference.IProductService productProxy;
        private WcfConsoleClient.BasketServiceReference.IBasketService basketProxy;

        List<Product> basket;

        public Menu()
        {
            manufacturerProxy = new ManufacturerServiceClient();
            productProxy = new ProductServiceClient();
            basketProxy = new BasketServiceClient();
            basket = new List<Product>();
        }

        public void StartMenu()
        {
            Console.WriteLine("****************************************");
            Console.WriteLine("EVERYONE");
            Console.WriteLine("--------------");
            Console.WriteLine("Enter 1 To: View All Products");
            Console.WriteLine("Enter 2 To: View All Manufacturers");
            Console.WriteLine("Enter 3 To: Find A Product By Manufacturer");
            Console.WriteLine("Enter 4 To: Find A Product By Category");
            Console.WriteLine("\nADMIN");
            Console.WriteLine("--------------");
            Console.WriteLine("Enter 5 To: Add A Manufacturer");
            Console.WriteLine("Enter 6 To: Add A Product");
            Console.WriteLine("Enter 7 To: Update A Manufacturer");
            Console.WriteLine("Enter 8 To: Update A Product");
            Console.WriteLine("Enter 9 To: Remove A Manufacturer");
            Console.WriteLine("Enter 10 To: Remove A Product");
            Console.WriteLine("\nCUSTOMER");
            Console.WriteLine("--------------");
            Console.WriteLine("Enter 11 To: Add Products To Basket");
            Console.WriteLine("Enter 12 To: Remove Products From Basket");
            Console.WriteLine("Enter 13 To: View Basket");
            Console.WriteLine("Enter 14 To: Checkout");
            Console.WriteLine("\n");
            Console.WriteLine("Enter X To: Exit");
            Console.WriteLine("****************************************");

            string choice = Console.ReadLine();
            Console.WriteLine("\n");

            if (choice == "1")
            {
                ViewAllProducts();
            }
            else if (choice == "2")
            {
                ViewAllManufacturers();
            }
            else if (choice == "3")
            {
                FindProductByManufacturer();
            }
            else if (choice == "4")
            {
                FindProductByCategory();
            }
            else if (choice == "5")
            {
                AddManufacturer();
            }
            else if (choice == "6")
            {
                AddProduct();
            }
            else if (choice == "7")
            {
                UpdateManufacturer();
            }
            else if (choice == "8")
            {
                UpdateProduct();
            }

            else if (choice == "9")
            {
                RemoveManufacturer();
            }
            else if (choice == "10")
            {
                RemoveProduct();
            }
            else if (choice == "11")
            {
                AddProductsToBasket();
            }
            else if (choice == "12")
            {
                RemoveProductsFromBasket();
            }
            else if (choice == "13")
            {
                ViewBasket();
            }
            else if (choice == "14")
            {
                Checkout();
            }
            else if (choice == "x")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("INVALID INPUT");
            }

            Continue();
            Console.WriteLine("\n");
            StartMenu();
        }

        private void FindProductByCategory()
        {
            Console.WriteLine("Enter Category");
            string category = Console.ReadLine();

            List<ProductDto> productsFromCategory = productProxy.GetProductsByCategory(category);

            Console.WriteLine("\n" + productProxy.ValidCategory(category));
            Console.WriteLine("---------------------");
            productsFromCategory.ForEach(p => Console.WriteLine($"{p.ProductId} : {p.Name}"));
        }

        private void RemoveProduct()
        {
            Console.WriteLine("Enter Product ID To Remove");
            int id = ExtraMethods.ValidInt(Console.ReadLine());

            Console.WriteLine("\n" + productProxy.DeleteProduct(id));
        }

        private void RemoveManufacturer()
        {
            Console.WriteLine("Enter Manufacturer ID To Remove");
            int id = ExtraMethods.ValidInt(Console.ReadLine());

            Console.WriteLine("\n" + manufacturerProxy.DeleteManufacturer(id));
        }

        private void FindProductByManufacturer()
        {
            Console.WriteLine("Enter Manufacturer");
            string manufacturer = Console.ReadLine();

            List<ProductDto> productsFromManufacturer = productProxy.GetProductsByManufacturer(manufacturer);

            Console.WriteLine("\nProducts By " + ExtraMethods.TitleString(manufacturer));
            Console.WriteLine("---------------------");
            productsFromManufacturer.ForEach(p => Console.WriteLine($"{p.ProductId} : {p.Name} \t £{p.Price}"));
        }

        private void UpdateProduct()
        {
            Console.WriteLine("Enter Product ID");
            int productId = ExtraMethods.ValidInt(Console.ReadLine());
            Console.WriteLine("Enter Updated Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Updated Manufacturer");
            string manufacturer = Console.ReadLine();
            Console.WriteLine("Enter Updated Category");
            string category = Console.ReadLine();
            Console.WriteLine("Enter Updated Price");
            double price = ExtraMethods.ValidDouble(Console.ReadLine());
            Console.WriteLine("Enter Amount Of Stock");
            int amount = ExtraMethods.ValidInt(Console.ReadLine());

            CheckManufacturerExists(manufacturer);

            Product product = new Product()
            {
                ProductId = productId,
                Name = name,
                Category = category,
                Manufacturer = new Manufacturer() { Name = manufacturer },
                Price = price,
                StockAmount = amount,
                ImagePath = "testPath.jpg",
                Description = "Description"
            };

            Console.WriteLine("\n" + productProxy.UpdateProduct(product));
        }

        private void UpdateManufacturer()
        {
            Console.WriteLine("Enter Manufacturer ID");
            int manufacturerId = ExtraMethods.ValidInt(Console.ReadLine());
            Console.WriteLine("Enter Updated Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Updated Website");
            string website = Console.ReadLine();

            Manufacturer manufacturer = new Manufacturer()
            {
                ManufacturerId = manufacturerId,
                Name = name,
                Website = website
            };
            Console.WriteLine("\n" + manufacturerProxy.UpdateManufacturer(manufacturer));
        }

        private void AddProduct()
        {
            Console.WriteLine("Enter Product Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Category");
            string category = Console.ReadLine();
            Console.WriteLine("Enter Manufacturer");
            string manufacturer = Console.ReadLine();

            Console.WriteLine("Enter Price");
            double price = ExtraMethods.ValidDouble(Console.ReadLine());
            Console.WriteLine("Enter Amount Of Stock");
            int amount = ExtraMethods.ValidInt(Console.ReadLine());

            CheckManufacturerExists(manufacturer);

            Product product = new Product()
            {
                Name = name,
                Category = category,
                Manufacturer = new Manufacturer() { Name = manufacturer },
                Price = price,
                StockAmount = amount,
                ImagePath = "testPath.jpg",
                Description = "Description",
            };

            int id = productProxy.AddProduct(product, "jpg");

            Console.WriteLine("\n" + "A New Product Was Added With ID {0}", id);
        }

        private void AddManufacturer()
        {
            Console.WriteLine("Enter Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Website");
            string website = Console.ReadLine();

            Manufacturer manufacturer = new Manufacturer()
            {
                Name = name,
                Website = website
            };

            int id = manufacturerProxy.AddManufacturer(manufacturer);
            Console.WriteLine("\n" + "A New Manufacturer Was Added With ID {0}", id);
        }

        private void ViewAllManufacturers()
        {
            List<ManufacturerDto> manufacturers = manufacturerProxy.GetAllManufacturers();
            Console.WriteLine("\nManufacturers");
            Console.WriteLine("---------------------");
            manufacturers.ForEach(m => Console.WriteLine($"{m.ManufacturerId} : {m.Name} \t Website: {m.Website} \t Amount Of Products: {m.AmountOfProducts}"));
        }

        private void ViewAllProducts()
        {

            List<ProductDto> products = productProxy.GetAllProducts();
            Console.WriteLine("\nProducts");
            Console.WriteLine("---------------------");
            products.ForEach(p => Console.WriteLine($"{p.ProductId} : {p.Name} \t By {p.ManufacturerName} \t £{p.Price} \t Stock: {p.StockAmount}"));
        }

        private void CheckManufacturerExists(string manufacturer)
        {
            if (manufacturerProxy.GetAllManufacturers().SingleOrDefault(m => m.Name.ToLower() == manufacturer.ToLower()) ==
                null)
            {
                Console.WriteLine("The Manufacturer {0} Doesn't Exist. Please Add A Website To Create A New One", ExtraMethods.TitleString(manufacturer));
                string website = Console.ReadLine();

                Manufacturer newManufacturer = new Manufacturer()
                {
                    Name = manufacturer,
                    Website = website
                };

                int manufacturerId = manufacturerProxy.AddManufacturer(newManufacturer);
                Console.WriteLine("\n" + "A New Manufacturer Was Added With ID {0}", manufacturerId);
            }
        }


        void AddProductsToBasket()
        {
            Console.WriteLine("Enter Product ID");
            int choice = ExtraMethods.ValidInt(Console.ReadLine());

            Console.WriteLine("\n");
            List<ProductDto> basketDto = basketProxy.AddItemToBasket(basket, choice);
            basket = ProductDtosToProducts(basketDto);
            Console.WriteLine("Added Item To Basket");
        }

        void RemoveProductsFromBasket()
        {
            Console.WriteLine("Enter Product ID");
            int choice = ExtraMethods.ValidInt(Console.ReadLine());


            Console.WriteLine("\n");

            List<ProductDto> basketDto = basketProxy.RemoveItemFromBasket(basket, choice);
            basket = ProductDtosToProducts(basketDto);
            Console.WriteLine("Removed Item From Basket");
        }

        void ViewBasket()
        {
            double totalPrice = 0;
            Console.WriteLine("\nBasket");
            Console.WriteLine("---------------------");
            foreach (var item in basket)
            {
                Console.WriteLine(item.Name);
                totalPrice += item.Price;
            }

            Console.WriteLine("\n");
            Console.WriteLine("Total £" + totalPrice);
        }

        void Checkout()
        {
            List<ProductDto> basketDto = basketProxy.Checkout(basket);
            basket = ProductDtosToProducts(basketDto);
            Console.WriteLine("Purchase Successful");
        }

        void Continue()
        {
            Console.WriteLine("\nPress Enter To Continue");
            Console.ReadLine();
        }

        private List<Product> ProductDtosToProducts(List<ProductDto> productDtos)
        {
            List<ManufacturerDto> manufacturerDtos = manufacturerProxy.GetAllManufacturers();

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
                StockAmount = p.StockAmount,
                Description = p.Description

            }).ToList();

            return products;
        }
    }
}
