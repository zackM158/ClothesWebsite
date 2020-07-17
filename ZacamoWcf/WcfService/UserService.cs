using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using ZacamoRepositories;

namespace WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserService : IUserService
    {
        private UserRepository repository;

        public UserService()
        {
            repository = new UserRepository();
        }

        public int AddUser(User user)
        {
            try
            {
                return repository.AddUser(user);
            }
            catch
            {
                return 0;
            }
        }

        public string DeleteUser(int id)
        {
            return repository.DeleteUser(id);
        }

        public List<UserDto> GetAllUsers()
        {
            List<User> users = repository.GetAllUsers();
            List<UserDto> userDtos = UsersToUserDtos(users);

            return userDtos;
        }

        public UserDto GetUserByEmail(string email)
        {
            User user = repository.GetUserByEmail(email);
            UserDto userDto = UserToUserDto(user);
            return userDto;
        }

        public UserDto GetUserById(int id)
        {
            User user = repository.GetUserById(id);
            UserDto userDto = UserToUserDto(user);
            return userDto;
        }

        public string UpdateUser(User user)
        {
            return repository.UpdateUser(user);
        }

        public string UpdatePassword(int id, string salt, string password)
        {
            return repository.UpdatePassword(id, salt, password);
        }

        public string AddPremium(int id)
        {
            return repository.AddPremium(id);
        }

        public void ChangeAdmin(int id)
        {
            repository.ChangeAdmin(id);
        }

        public void ChangePremium(int id)
        {
            repository.ChangePremium(id);
        }

        public void ToggleSavedItems(int userId, int productId)
        {
            repository.ToggleSavedItems(userId, productId);
        }

        List<UserDto> UsersToUserDtos(List<User> users)
        {
            List<UserDto> userDtos = users.Select(u => new UserDto()
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EmailAddress = u.EmailAddress,
                Password = u.Password,
                Salt = u.Salt,
                IsAdmin = u.IsAdmin,
                IsPremiumUser = u.IsPremiumUser,
                AddressId = u.AddressId,
                HouseNumber = u.Address.HouseNumber,
                StreetName = u.Address.StreetName,
                City = u.Address.City,
                Postcode = u.Address.Postcode,
                SavedItems = u.SavedItems
            }).ToList();

            return userDtos;
        }     
        
        UserDto UserToUserDto(User user)
        {
            if (user == null)
                return null;

            UserDto userDto = new UserDto()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                Salt = user.Salt,
                IsAdmin = user.IsAdmin,
                IsPremiumUser = user.IsPremiumUser,
                AddressId = user.AddressId,
                HouseNumber = user.Address.HouseNumber,
                StreetName = user.Address.StreetName,
                City = user.Address.City,
                Postcode = user.Address.Postcode,
                SavedItems = user.SavedItems
            };

            return userDto;
        }
    }
}
