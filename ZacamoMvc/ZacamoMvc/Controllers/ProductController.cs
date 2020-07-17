using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZacamoMvc.ProductServiceReference;
using ZacamoMvc.ManufacturerServiceReference;
using ZacamoMvc.UserServiceReference;
using Entities;
using ZacamoMvc.Models;
using System.Security.Claims;
using System.Web.UI;

namespace ZacamoMvc.Controllers
{

    [Authorize(Roles = "Admin"), HandleError]
    public class ProductController : Controller
    {
        private ProductServiceClient productRepository;
        private ManufacturerServiceClient manufacturerRepository;
        private UserServiceClient userRepository;

        public ProductController()
        {
           productRepository = new ProductServiceClient();
           manufacturerRepository = new ManufacturerServiceClient();
           userRepository = new UserServiceClient();
        }

        private List<Product> ProductDtosToProducts(List<ProductDto> productDtos)
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
                Manufacturer = manufacturers.Single(m=>m.ManufacturerId == p.ManufacturerId),
                ImagePath = p.ImagePath,
                Price = p.Price,
                StockAmount = p.StockAmount,
                Description = p.Description

            }).ToList();

            return products;
        }

        private Product ProductDtoToProduct(ProductDto productDto)
        {
            List<ManufacturerDto> manufacturerDtos = manufacturerRepository.GetAllManufacturers();

            List<Manufacturer> manufacturers = manufacturerDtos.Select(m => new Manufacturer()
            {
                ManufacturerId = m.ManufacturerId,
                Name = m.Name,
                Website = m.Website
            }).ToList();

            Product product = new Product()
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Category = productDto.Category,
                Manufacturer = manufacturers.Single(m => m.ManufacturerId == productDto.ManufacturerId),
                ImagePath = productDto.ImagePath,
                Price = productDto.Price,
                StockAmount = productDto.StockAmount,
                Description = productDto.Description
            };
            return product;
        }

        List<Product> SortProducts(List<Product> products, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
                case "PriceLow":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "PriceHigh":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "Category":
                    products = products.OrderBy(p => p.Category).ToList();
                    break;
                case "Manufacturer":
                    products = products.OrderBy(p => p.Manufacturer.Name).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Name).ToList();
                    break;
            }

            return products;
        }

        // GET: Product
        [AllowAnonymous]
        public ActionResult Index(string searchTerm, string sortOrder)
        {
            List<Product> products = new List<Product>();
            List<ProductDto> productDtos = new List<ProductDto>();


            var identity = (ClaimsIdentity)User.Identity;
            string savedItems = "";

            if (identity.IsAuthenticated)
            {
                Claim email = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
                savedItems = userRepository.GetUserByEmail(email.Value).SavedItems;
                if(savedItems != null && savedItems != "")
                ViewBag.SavedItems = savedItems.Split(',').ToList();
            }

            if (searchTerm != null)
            {
                if (searchTerm == "Saved")
                {
                    ViewBag.Title = searchTerm;
                    ViewBag.SearchTerm = searchTerm;
                    productDtos = productRepository.SavedProducts(savedItems);
                }
                else
                {
                    ViewBag.Title = searchTerm;
                    ViewBag.SearchTerm = searchTerm;
                    productDtos = productRepository.SearchProducts(searchTerm);
                }
            }
            else
            {
                ViewBag.Title = "Clothes";
                productDtos = productRepository.GetAllProducts();
            }
            products = ProductDtosToProducts(productDtos);

            products = SortProducts(products, sortOrder);

            return View(products);
        }

        // GET: Product/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            ProductDto productDto = productRepository.GetProductById(id);

            if (productDto == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            Product product = ProductDtoToProduct(productDto);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            AddManufacturersAndCategoriesToViewBag();

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductWithImageModel productWithImageModel)
        {
            try
            {
                if (productWithImageModel.PostedFile == null)
                {
                    ModelState.AddModelError("", "Please Upload An Image Of Type JPEG Or PNG");
                    AddManufacturersAndCategoriesToViewBag();
                    return View();
                }

                string newFileExtension = Path.GetExtension(productWithImageModel.PostedFile.FileName);
                productWithImageModel.NewProduct.ImagePath = Path.GetFileName(productWithImageModel.PostedFile.FileName);
                int productId = productRepository.AddProduct(productWithImageModel.NewProduct, newFileExtension);

                if (productId == 0)
                {
                    ModelState.AddModelError("", "Please Upload An Image Of Type JPEG Or PNG");
                    AddManufacturersAndCategoriesToViewBag();
                    return View();
                }

                string newFileName = productWithImageModel.NewProduct.Category + "_ID_" + productId;
                string newFile = newFileName + newFileExtension;

                string path = Path.Combine(Server.MapPath("~/Images"),
                    newFile);

                productWithImageModel.PostedFile.SaveAs(path);


                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Please Upload An Image Of Type JPEG Or PNG");
                AddManufacturersAndCategoriesToViewBag();
                return View();
            }
        }

        

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductDto productDto = productRepository.GetProductById(id);

            if (productDto == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }

            Product product = ProductDtoToProduct(productDto);
            AddManufacturersAndCategoriesToViewBag();
            ProductToUpdateWithImageModel productWithImageModel = new ProductToUpdateWithImageModel()
            {
                NewProduct = product
            };
            return View(productWithImageModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductToUpdateWithImageModel productWithImageModel)
        {
            try
            {
                if (productWithImageModel.PostedFile != null)
                {
                    string newFile = productWithImageModel.NewProduct.ImagePath;

                    string path = Path.Combine(Server.MapPath("~/Images"),
                        newFile);

                    productWithImageModel.PostedFile.SaveAs(path);
                }

                productRepository.UpdateProduct(productWithImageModel.NewProduct);

                return RedirectToAction("Index");
            }
            catch
            {
                AddManufacturersAndCategoriesToViewBag();
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            ProductDto productDto = productRepository.GetProductById(id);

            if (productDto == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            
            Product product = ProductDtoToProduct(productDto);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                productRepository.DeleteProduct(id);

                return RedirectToAction("Index");
            }
            catch
            {
                ProductDto productObjDto = productRepository.GetProductById(id);
                Product productObj = ProductDtoToProduct(productObjDto);
                return View(productObj);
            }
        }

        private void AddManufacturersAndCategoriesToViewBag()
        {
            List<ManufacturerDto> manufacturers = manufacturerRepository.GetAllManufacturers();

            ViewBag.ListOfManufacturers = new SelectList(manufacturers.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.ManufacturerId.ToString()
            }).ToList(), "Value", "Text");

            List<string> categories = new List<string>(){"Shirts", "Trousers", "Suits", "Shoes"};

            ViewBag.ListOfCategories = new SelectList(categories.Select(c => new SelectListItem
            {
                Text = c,
                Value = c
            }).ToList(), "Value", "Text");
        }
    }
}
