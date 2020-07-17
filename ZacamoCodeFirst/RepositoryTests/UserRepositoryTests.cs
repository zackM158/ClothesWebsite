using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer2;
using Entities;
using NUnit.Framework;
using ZacamoRepositories;


namespace RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private ProductsContext dbBefore;
        private ProductsContext dbAfter;
        private AddressRepository addressRepository;
        private UserRepository userRepository;

        Address address1;
        Address address2;
        User user1;
        User user2;

        [SetUp]
        public void Setup()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            dbBefore = new ProductsContext(connection);
            dbAfter = new ProductsContext(connection);
            ProductsContext dbService = new ProductsContext(connection);
            userRepository = new UserRepository(dbService);
            addressRepository = new AddressRepository(dbService);

            address1 = new Address
            {
                HouseNumber = 64,
                StreetName = "zoo lane",
                City = "sheffield",
                Postcode = "s10 1er"
            };

            address2 = new Address
            {
                HouseNumber = 21,
                StreetName = "West Street",
                City = "Leeds",
                Postcode = "LE1 4NQ"
            };

            addressRepository.AddAddress(address1);
            addressRepository.AddAddress(address2);

            user1 = new User
            {
                FirstName = "Liam",
                LastName = "Gallagher",
                EmailAddress = "LG@gmail.com",
                AddressId = 1,
                Address = address1,
                IsAdmin = true,
                IsPremiumUser = true,
                Password = "1bc1a361f17092bc7af4b2f82bf9194ea9ee2ca49eb2e53e39f555bc1eeaed74", //encrypted password for "test"
                Salt = "salt"
            };

            user2 = new User
            {
                FirstName = "Karl",
                LastName = "Pilkington",
                EmailAddress = "KP@gmail.com",
                AddressId = 2,
                Address = address2,
                IsAdmin = false,
                IsPremiumUser = false,
                Password = "1bc1a361f17092bc7af4b2f82bf9194ea9ee2ca49eb2e53e39f555bc1eeaed74", //encrypted password for "test"
                Salt = "salt"
            };
        }


        [Test]
        public void AddUser_AddsUserToDatabase_WhenCalled()
        {
            //Arrange

            //Act
            int user1Id = userRepository.AddUser(user1);

            //Assert
            User user = dbAfter.Users.Find(user1Id);
            Assert.That(user, Is.Not.Null);
            Assert.That(user.FirstName == "Liam");
        }

        [Test]
        public void GetAllUsers_ReturnsAllUsersInDatabase_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);
            int user2Id = userRepository.AddUser(user2);

            //Act
            List<User> users = userRepository.GetAllUsers();

            //Assert
            Assert.AreEqual(2, users.Count);
            Assert.AreEqual("Liam", users[0].FirstName);
            Assert.AreEqual("Karl", users[1].FirstName);

        }

        [Test]
        public void UpdateUser_UpdatesUserInDatabase_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);

            //Act
            User userToUpdate = dbAfter.Users.Find(user1Id);
            userToUpdate.FirstName = "Noel";
            userToUpdate.EmailAddress = "NG@gmail.com";

            userRepository.UpdateUser(userToUpdate);

            //Assert
            User updatedUser = dbAfter.Users.Find(user1Id);
            Assert.AreEqual("Noel", updatedUser.FirstName);
            Assert.AreEqual("NG@gmail.com", updatedUser.EmailAddress);
        }

        [Test]
        public void DeleteUser_DeletesTheUserFromDatabase_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);
            int user2Id = userRepository.AddUser(user2);

            //Act
            userRepository.DeleteUser(user1Id);

            //Assert
            List<User> users = userRepository.GetAllUsers();
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("Karl", users[0].FirstName);

        }

        [Test]
        public void GetUserById_GetsRequiredUserFromDatabase_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);
            int user2Id = userRepository.AddUser(user2);

            //Act
            User chosenUser = userRepository.GetUserById(2);

            //Assert
            Assert.That(chosenUser, Is.Not.Null);
            Assert.AreEqual("Karl", chosenUser.FirstName);
        }

        [Test]
        public void GetUserByEmail_GetsRequiredUserFromDatabase_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);
            int user2Id = userRepository.AddUser(user2);

            //Act
            User chosenUser = userRepository.GetUserByEmail("LG@gmail.com");

            //Assert
            Assert.That(chosenUser, Is.Not.Null);
            Assert.AreEqual("Liam", chosenUser.FirstName);
        }

        [Test]
        public void UpdatePassword_ChangesPasswordOfUser_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);

            //Act
            userRepository.UpdatePassword(1, "hello", "password");

            //Assert
            User user = dbAfter.Users.Find(1);
            Assert.AreEqual("hello", user.Salt);
            Assert.AreEqual("password", user.Password);
        }

        [Test]
        public void AddPremium_GivesUserPremium_WhenCalled()
        { 
            //Arrange
            int user1Id = userRepository.AddUser(user1);
            int user2Id = userRepository.AddUser(user2);

            //Act
            userRepository.AddPremium(2);

            //Assert
            User chosenUser = dbAfter.Users.Find(2);
            Assert.AreEqual(true, chosenUser.IsPremiumUser);

        }

        [Test]
        public void ChangeAdmin_TogglesAdminOfUser_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);
            int user2Id = userRepository.AddUser(user2);

            //Act
            userRepository.ChangeAdmin(2);
            userRepository.ChangeAdmin(1);

            //Assert
            User chosenUser1 = dbAfter.Users.Find(1);
            User chosenUser2 = dbAfter.Users.Find(2);
            Assert.AreEqual(false, chosenUser1.IsAdmin);
            Assert.AreEqual(true, chosenUser2.IsAdmin);
        }

        [Test]
        public void ChangePremium_TogglesPremiumOfUser_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);
            int user2Id = userRepository.AddUser(user2);

            //Act
            userRepository.ChangePremium(1);
            userRepository.ChangePremium(2);

            //Assert
            User chosenUser1 = dbAfter.Users.Find(1);
            User chosenUser2 = dbAfter.Users.Find(2);
            Assert.AreEqual(false, chosenUser1.IsPremiumUser);
            Assert.AreEqual(true, chosenUser2.IsPremiumUser);
        }

        [Test]
        public void ToggleSavedItems_AddsAProductToStringWhenItIsNotThere_WhenCalled()
        {
            //Arrange
            int user1Id = userRepository.AddUser(user1);

            //Act
            userRepository.ToggleSavedItems(1, 2);
            userRepository.ToggleSavedItems(1, 5);
            userRepository.ToggleSavedItems(1, 7);

            //Arrange
            User user = dbAfter.Users.Find(1);
            Assert.AreEqual("2,5,7", user.SavedItems);
        }

        [Test]
        public void ToggleSavedItems_RemovesProductFromStringWhenItIsThere_WhenCalled()
        {   
            //Arrange
            int user1Id = userRepository.AddUser(user1);

            //Act
            userRepository.ToggleSavedItems(1, 2);
            userRepository.ToggleSavedItems(1, 5);
            userRepository.ToggleSavedItems(1, 7);
            userRepository.ToggleSavedItems(1, 2);

            //Arrange
            User user = dbAfter.Users.Find(1);
            Assert.AreEqual("5,7", user.SavedItems);

        }
    }
}
