using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer2;
using ZacksLibrary;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace ZacamoRepositories
{
    public class UserRepository : IUserRepository
    {
        private ProductsContext context;
        public UserRepository(ProductsContext db)
        {
            context = db; 
        }

        public UserRepository()
        {
            context = new ProductsContext();
        }

        public int AddUser(User user)
        {
            try
            {
                Address address = context.Addresses.SingleOrDefault(a => a.AddressId == user.AddressId);
                if (address != null)
                {
                    user.Address = address;
                }

                user.FirstName = ExtraMethods.TitleString(user.FirstName);
                user.LastName = ExtraMethods.TitleString(user.LastName);
                context.Users.Add(user);
                context.SaveChanges();
                return user.UserId;
            }
            catch
            {
                return 0;
            }
        }

        public string DeleteUser(int id)
        {
            User user = context.Users.Find(id);

            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return "User With ID " + id + " Successfully Removed";
            }
            else
            {
                return "No User With ID Of " + id;
            }
        }

        public List<User> GetAllUsers()
        {
            return context.Users.Include(u=>u.Address).ToList();
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.Include(u => u.Address).SingleOrDefault(u => u.EmailAddress == email);
        }

        public User GetUserById(int id)
        {
            return context.Users.Include(u => u.Address).SingleOrDefault(u => u.UserId == id);
        }

        public string UpdateUser(User user)
        {
            User userToUpdate = context.Users.Find(user.UserId);

            user.FirstName = ExtraMethods.TitleString(user.FirstName);
            user.LastName = ExtraMethods.TitleString(user.LastName);

            Address address = context.Addresses.SingleOrDefault(a => a.AddressId == user.AddressId);
            if (address != null)
            {
                user.Address = address;
            }

            if (userToUpdate != null)
            {
                if(user.EmailAddress != userToUpdate.EmailAddress && GetUserByEmail(user.EmailAddress) != null)
                {
                    return "Email Taken";
                }

                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.EmailAddress = user.EmailAddress;
                userToUpdate.AddressId = user.AddressId;
                if (address != null)
                {
                    userToUpdate.Address = address;
                }

                context.SaveChanges();
                return "Updated User With ID " + user.UserId;
            }
            else
            {
                return "There Is No User With ID Of " + user.UserId;
            }
        }

        public string UpdatePassword(int id, string salt, string password)
        {
            User userToUpdate = context.Users.Find(id);

            if (userToUpdate != null)
            {
                userToUpdate.Salt = salt;
                userToUpdate.Password = password;
                context.SaveChanges();
                return "Updated Password";
            }
            else
            {
                return "There Is No User With ID Of " + id;
            }
        }

        public string AddPremium(int id)
        {
            User userToUpdate = context.Users.Find(id);

            if (userToUpdate != null)
            {
                userToUpdate.IsPremiumUser = true;
                context.SaveChanges();
                return "Added Premium!";
            }
            else
            {
                return "There Is No User With ID Of " + id;
            }
        }

        public void ChangeAdmin(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                user.IsAdmin = !user.IsAdmin;
                context.SaveChanges();
            }
        }

        public void ChangePremium(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                user.IsPremiumUser = !user.IsPremiumUser;
                context.SaveChanges();
            }
        }

        public void ToggleSavedItems(int userId, int productId)
        {
            User user = context.Users.Find(userId);
            string productIdString = productId.ToString();

            if (user.SavedItems == null || user.SavedItems == "")
            {
                user.SavedItems = productIdString;
                context.SaveChanges();
            }
            else
            {
                List<string> savedProductIds = user.SavedItems.Split(',').ToList();

                if (savedProductIds.Contains(productIdString))
                {
                    savedProductIds.Remove(productIdString);
                    user.SavedItems = string.Join(",", savedProductIds);
                }
                else 
                { 
                    user.SavedItems += "," + productIdString;
                }
                context.SaveChanges();
            }
        }

    }
}
