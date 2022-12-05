using DataLibrary.BuisnessLogic;
using Orius.Models;
using Orius.Scripts;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Orius.Controllers
{
    public class UserController : Controller
    {
        public const int WORK_FACTOR = 13;

        public ActionResult Index(string search, int? page)
        {
            var data = UserProcessor.LoadUsers();
            var users = new List<UserModel>();

            foreach(var row in data)
            {
                users.Add(new UserModel
                {
                    Id = row.Id,
                    Username = row.Username,
                    EmailAddress = row.Email
                });
            }

            if (search != null)
            {
                users = users.Where(x => x.Username.Contains(search) || x.Id.ToString().Contains(search) || x.EmailAddress.Contains(search)).ToList();
            }

            return View(users.ToPagedList(page ?? 1, 5));
        }


        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserModel model)
        {
            var user = UserProcessor.LoadUsers().Where(x => x.Username == model.Username).ToList().FirstOrDefault();

            if (user != null && model.Password != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, WORK_FACTOR);
                int recordsCreated = UserProcessor.CreateUser(model.Username, encryptedPassword, model.EmailAddress);

                return RedirectToAction("SignIn");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ViewProfile(string username)
        {
            var data = UserProcessor.LoadUsers().Where(x => x.Username == username).FirstOrDefault();

            if (data == null) return RedirectToAction("Index", "Tickets");

            var user = new UserModel
            {
                Username = data.Username,
                Id = data.Id,
                EmailAddress = data.Email,
            };

            return View(user);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "User");
        }
    }
}