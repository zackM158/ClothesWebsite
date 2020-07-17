using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Entities;
using ZacamoMvc.AddressServiceReference;
using ZacamoMvc.UserServiceReference;

namespace ZacamoMvc.Controllers
{
    [Authorize(Roles = "Admin"), HandleError]
    public class MemberController : Controller
    {
        private UserServiceClient userRepository;
        private AddressServiceClient addressRepository;

        public MemberController()
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

        // GET: Member
        public ActionResult Index()
        {
            List<UserDto> userDtos = userRepository.GetAllUsers();
            List<User> users = UserDtosToUsers(userDtos);

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            return View(users);
        }

        public ActionResult ChangeAdmin(int id)
        {
            List<UserDto> userDtos = userRepository.GetAllUsers();
            List<User> users = UserDtosToUsers(userDtos);


            int adminAmount = users.FindAll(u => u.IsAdmin == true).Count;
            var identity = (ClaimsIdentity)User.Identity;
            Claim email = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
            int currentUserId = userRepository.GetUserByEmail(email.Value).UserId;
            if (adminAmount <= 1 && currentUserId == id)
            {
                ModelState.AddModelError("", "Must Have At Least One Admin");
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            userRepository.ChangeAdmin(id);
            return RedirectToAction("Index");
        }

        public ActionResult ChangePremium(int id)
        {
            userRepository.ChangePremium(id);
            return RedirectToAction("Index");
        }
    }
}