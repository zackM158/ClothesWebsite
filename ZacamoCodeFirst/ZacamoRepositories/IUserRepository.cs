using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ZacamoRepositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        int AddUser(User user);
        string UpdateUser(User user);
        string DeleteUser(int id);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        string UpdatePassword(int id, string salt, string password);

        string AddPremium(int id);

        void ChangeAdmin(int id);
        void ChangePremium(int id);

        void ToggleSavedItems(int userId, int productId);
    }
}
