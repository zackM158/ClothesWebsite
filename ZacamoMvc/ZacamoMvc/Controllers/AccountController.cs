using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Entities;
using ZacamoMvc.AddressServiceReference;
using ZacamoMvc.Models;
using ZacamoMvc.UserServiceReference;

namespace ZacamoMvc.Controllers
{
    [Authorize, HandleError]
    public class AccountController : Controller
    {
        private UserServiceClient userRepository;
        private AddressServiceClient addressRepository;

        public AccountController()
        {
            userRepository = new UserServiceClient();
            addressRepository = new AddressServiceClient();
        }

        private List<User> UserDtosToUsers(List<UserDto> userDtos)
        {

            List<User> users = userDtos.Select(u => new User()
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
                Address = addressRepository.GetAddressById(u.AddressId),
                SavedItems = u.SavedItems

            }).ToList();

            return users;
        }

        private User UserDtoToUser(UserDto userDto)
        {
            Address address = addressRepository.GetAddressById(userDto.AddressId);

            User user = new User()
            {
                UserId = userDto.UserId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                EmailAddress = userDto.EmailAddress,
                Password = userDto.Password,
                Salt = userDto.Salt,
                IsAdmin = userDto.IsAdmin,
                IsPremiumUser = userDto.IsPremiumUser,
                AddressId = userDto.AddressId,
                Address = address,
                SavedItems = userDto.SavedItems
            };
            return user;
        }

        // GET: User
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            Claim email = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
            UserDto userDto = userRepository.GetUserByEmail(email.Value);
            User user = UserDtoToUser(userDto);
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            UserDto userDto = userRepository.GetUserById(id);
            User user = UserDtoToUser(userDto);
            return View(user);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                Address address = new Address()
                {
                    HouseNumber = user.Address.HouseNumber,
                    StreetName = user.Address.StreetName,
                    City = user.Address.City,
                    Postcode = user.Address.Postcode
                };

                int addressId = addressRepository.AddAddress(address);
                user.AddressId = addressId;

                UserDto userDto = userRepository.GetUserById(id);
                User oldUser = UserDtoToUser(userDto);

                user.Password = oldUser.Password;
                user.Salt = oldUser.Salt;
                user.IsPremiumUser = oldUser.IsPremiumUser;
                user.IsAdmin = oldUser.IsAdmin;
                string updateMessage = userRepository.UpdateUser(user);

                if(updateMessage == "Email Taken")
                {
                    ModelState.AddModelError("", "Email Already In Use");
                    return View();
                }

                return RedirectToAction("RefreshAccount", "Security", new { id = id});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            UserDto userToDeleteDto = userRepository.GetUserById(id);

            User userToDelete = UserDtoToUser(userToDeleteDto);
            return View(userToDelete);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, User user)
        {
            try
            {
                return RedirectToAction("DeleteAccount", "Security", new { id = id });
            }
            catch
            {
                return View(user);
            }
        }

        public ActionResult UpdatePassword(int id)
        {
            UpdatePasswordModel updatePasswordModel = new UpdatePasswordModel
            {
                UserId = id
            };
            return View(updatePasswordModel);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        public ActionResult UpdatePassword(int id, UpdatePasswordModel passwordModel)
        {
            try
            {
                if (passwordModel.Password != passwordModel.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords must match!");
                    return View(passwordModel);
                }

                return RedirectToAction("UpdatePassword", "Security", new { id = id, password = passwordModel.Password });
            }
            catch
            {
                return View(passwordModel);
            }
        }

        public ActionResult Premium()
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
        public ActionResult Premium(CreditCardModel cardModel)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                Claim email = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
                UserDto userDto = userRepository.GetUserByEmail(email.Value);
                User user = UserDtoToUser(userDto);
                userRepository.AddPremium(user.UserId);
                return RedirectToAction("RefreshAccount", "Security");
            }
            catch
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult ToggleSave(int productId)
        {
            var identity = (ClaimsIdentity)User.Identity;

            if (identity.IsAuthenticated)
            {
                Claim email = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
                UserDto userDto = userRepository.GetUserByEmail(email.Value);
                User user = UserDtoToUser(userDto);

                userRepository.ToggleSavedItems(user.UserId, productId);
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return RedirectToAction("Login", "Security");
            }
        }
    }
}