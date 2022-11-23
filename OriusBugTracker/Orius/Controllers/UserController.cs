using DataLibrary.BuisnessLogic;
using Orius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Orius.Controllers
{
    public class UserController : Controller
    {
        public const int WORK_FACTOR = 13;

        // GET: User
        public ActionResult Index()
        {
            return View("SignIn");
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
                    return RedirectToAction("Index", "Tickets");
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

        public ActionResult ViewProfile(string username)
        {
            var user = UserProcessor.LoadUsers().Where(x => x.Username == username).FirstOrDefault();

            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "User");
        }
    }
}