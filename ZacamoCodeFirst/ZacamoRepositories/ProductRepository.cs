using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer2;
using System.Data.Entity;
using System.Data.Entity.Validation;
using ZacksLibrary;

namespace ZacamoRepositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductsContext context;
        public ProductRepository(ProductsContext db)
        {
            context = db;
        }

        public ProductRepository()
        {
            context = new ProductsContext();
        }

        private List<string> categories = new List<string>() { "Shirts", "Suits", "Shoes", "Trousers"};

        public int AddProduct(Product product, string fileType)
        {
            try
            {
                Manufacturer manufacturer;

                if (product.ManufacturerId == 0)
                    manufacturer =
                        context.Manufacturers.SingleOrDefault(m =>
                            m.Name.ToLower() == product.Manufacturer.Name.ToLower());
                else
                    manufacturer =
                        context.Manufacturers.SingleOrDefault(m => m.ManufacturerId == product.ManufacturerId);


                product.Name = ExtraMethods.TitleString(product.Name);

                if (manufacturer != null)
                    product.Manufacturer = manufacturer;

                product.Category = ValidCategory(product.Category);
                product.Price = Math.Round(product.Price, 2);

                context.Products.Add(product);
                context.SaveChanges();

                UpdateImagePath(product, fileType);
                return product.ProductId;
            }
            catch (DbEntityValidationException ex)
            {
                return 0;
            }
        }

        public void UpdateImagePath(Product product, string fileType)
        {
            string newImagePath = product.Category + "_ID_" + product.ProductId + fileType;
            Product productInDatabase = context.Products.Find(product.ProductId);
            productInDatabase.ImagePath = newImagePath;
            context.SaveChanges();
        }

        public string DeleteProduct(Product product)
        {
            return DeleteProduct(product.ProductId);
        }

        public string DeleteProduct(int id)
        {
            Product product = context.Products.Find(id);

            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return "Product With ID " + id + " Successfully Removed";
            }
            else
            {
                return "No Product With ID Of " + id;
            }
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public List<Product> GetAllProductsWithManufacturers()
        {
            return context.Products.Include(p => p.Manufacturer).ToList();
        }

        public List<Product> GetProductsByManufacturer(string manufacturer)
        {
            return context.Products.Where(p => p.Manufacturer.Name.ToLower() == manufacturer.ToLower()).ToList();
        }

        public List<Product> GetProductsByCategory(string category)
        {
            string validCategory = ValidCategory(category);
            return context.Products.Where(p => p.Category.ToLower() == validCategory.ToLower()).ToList();
        }

        public string UpdateProduct(Product product)
        {
            product.Name = ExtraMethods.TitleString(product.Name);

            Product productToUpdate = context.Products.Find(product.ProductId);

            Manufacturer manufacturer;

            if (product.ManufacturerId == 0)
                manufacturer = context.Manufacturers.SingleOrDefault(m => m.Name.ToLower() == product.Manufacturer.Name.ToLower());
            else
                manufacturer = context.Manufacturers.SingleOrDefault(m => m.ManufacturerId == product.ManufacturerId);

            if (manufacturer != null)
                product.Manufacturer = manufacturer;

            product.Category = ValidCategory(product.Category);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Category = product.Category;
                productToUpdate.Manufacturer = product.Manufacturer;
                productToUpdate.Price = Math.Round(product.Price, 2);
                productToUpdate.StockAmount = product.StockAmount;
                productToUpdate.ImagePath = product.ImagePath;
                productToUpdate.Description = product.Description;
                context.SaveChanges();
                return "Updated Product With ID " + product.ProductId;
            }
            else
            {
                return "There Is No Product With ID Of " + product.ProductId;
            }
        }

        public string ValidCategory(string initalCategory)
        {
            if (initalCategory.Length >= 4)
            {
                foreach (string category in categories)
                {
                    if (category.ToLower().StartsWith(initalCategory.ToLower().Substring(0, 4)))
                    {
                        return category;
                    }
                }
            }

            return null;
        }

        public Product GetProductById(int id)
        {
            return context.Products.Find(id);
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            Manufacturer manufacturer = context.Manufacturers.SingleOrDefault(m=>m.Name.ToLower() == searchTerm);
            if (manufacturer != null)
            {
                return GetProductsByManufacturer(manufacturer.Name);
            }

            if (ValidCategory(searchTerm) != null)
                return GetProductsByCategory(searchTerm);

            List<Product> searchedProducts =
                GetAllProductsWithManufacturers().Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            return searchedProducts;
        }

        public List<Product> SavedProducts(string savedProductsString)
        {

            List<string> savedProductsList = new List<string>();

            if (savedProductsString != null && savedProductsString != "")
            {
                savedProductsList = savedProductsString.Split(',').ToList();
            }

            List<Product> searchedProducts =
                    GetAllProductsWithManufacturers().Where(p => savedProductsList.Contains(p.ProductId.ToString())).ToList();

            return searchedProducts;
        }
    }
}
