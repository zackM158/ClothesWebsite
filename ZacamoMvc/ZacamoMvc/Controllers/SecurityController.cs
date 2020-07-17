using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Entities;
using ZacamoMvc.Models;
using ZacamoMvc.UserServiceReference;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ZacamoMvc.AddressServiceReference;

namespace ZacamoMvc.Controllers
{
    [HandleError]
    public class SecurityController : Controller
    {
        private UserServiceClient userRepository;
        private AddressServiceClient addressRepository;

        public SecurityController()
        {
            userRepository = new UserServiceClient();
            addressRepository = new AddressServiceClient();
        }

        private User UserDtoToUser(UserDto userDto)
        {
            if (userDto == null)
                return null;

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

        // GET: Security
        public ActionResult Login(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string returnUrl, LoginModel model)
        {
            if (!ModelState.IsValid)
                return View();

            bool userCredentialsAreValid = false;

            List<string> userRoles = new List<string>();

            UserDto userDto;
            User user;

            try
            {
                userDto = userRepository.GetUserByEmail(model.EmailAddress);
                user = UserDtoToUser(userDto);

                if (user != null)
                {
                    string password = Security.Sha256(user.Salt, model.Password);

                    userCredentialsAreValid = (password == user.Password);
                    if (user.IsAdmin)
                        userRoles.Add("Admin");
                    if(user.IsPremiumUser)
                        userRoles.Add("Premium");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

            if (!userCredentialsAreValid)
            {
                ModelState.AddModelError("", "User credentials not recognised. Please try again.");
                return View();
            }

            //Get list of claims
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address.HouseNumber + " " + user.Address.StreetName));
            claims.Add(new Claim(ClaimTypes.Locality, user.Address.City));
            claims.Add(new Claim(ClaimTypes.PostalCode, user.Address.Postcode));

            foreach (string role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            //prepare settings for the cookie
            AuthenticationProperties authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddHours(1)
            };

            //Sign out (just in case)
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn(authenticationProperties, claimsIdentity);

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";

            return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Redirect("/");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            //passwords must match
            if (user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords must match!");
                return View(user);
            }

            try
            {
                Address address = new Address()
                {
                    HouseNumber = user.HouseNumber,
                    StreetName = user.StreetName,
                    City = user.City,
                    Postcode = user.Postcode
                };

                int addressId = addressRepository.AddAddress(address);

                User newUser = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress,
                    AddressId = addressId,
                    IsPremiumUser = false,
                    IsAdmin = false,
                    Password = Security.Sha256(out string salt, user.Password),
                    Salt = salt
                };

                int userId = 0;
                userId = userRepository.AddUser(newUser);

                if (userId == 0)
                {
                    ModelState.AddModelError("", "Email Already In Use");
                    return View(user);
                }

                return RedirectToAction("Login", "Security", new { email= user.EmailAddress });
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "This Email Already Exists");
                return View(user);
            }
            catch
            {
                return View(user);
            }
        }

        public ActionResult DeleteAccount(int id)
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            userRepository.DeleteUser(id);


            return RedirectToAction("Index", "Home");
        }   
        
        public ActionResult UpdatePassword(int id, string password)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string email = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email).Value;

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            UserDto userToUpdateDto = userRepository.GetUserById(id);
            User userToUpdate = UserDtoToUser(userToUpdateDto);
            password = Security.Sha256(out string salt, password);
            string UpdatedSalt = salt;
            userRepository.UpdatePassword(id, UpdatedSalt, password);


            return RedirectToAction("Login", "Security", new {email = email});
        }

        public ActionResult RefreshAccount()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string email = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email).Value;

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login", "Security", new { email = email });
        }
    }
}