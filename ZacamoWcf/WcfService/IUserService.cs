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
    public interface IUserService
    {
        [OperationContract]
        List<UserDto> GetAllUsers();

        [OperationContract]
        int AddUser(User user);
        [OperationContract]
        string UpdateUser(User user);
        [OperationContract]
        string DeleteUser(int id);
        [OperationContract]
        UserDto GetUserById(int id);
        [OperationContract]
        UserDto GetUserByEmail(string email);

        [OperationContract]
        string UpdatePassword(int id, string salt, string password);

        [OperationContract]
        string AddPremium(int id);

        [OperationContract]
        void ChangeAdmin(int id);

        [OperationContract]
        void ChangePremium(int id);

        [OperationContract]
        void ToggleSavedItems(int userId, int productId);
    }
}