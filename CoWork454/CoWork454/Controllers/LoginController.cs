using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoWork454.Data;
using CoWork454.Models;
using CoWork454.Controler;
using CoWork454.Common;

namespace CoWork454.Controllers
{
    public class LoginController : Controller
    { 

        private readonly CoWork454Context _CoWork454Context;

        public LoginController(CoWork454Context tastingsContext)
        {
            _CoWork454Context = tastingsContext;
        }

        // GET: Login
        public ActionResult Login()
        {
            var userIdCookie = GetEncryptedUserCookie("USER_ID");

            if(userIdCookie == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var existingUser = _CoWork454Context.User.SingleOrDefault(l => l.Id == Convert.ToInt32(userIdCookie));
                ViewData["User"] = existingUser;
            }
            //return View();
            return View("Members");
        }



        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            // lookup the user by email address
            var existingUser = _CoWork454Context.User.SingleOrDefault(l => l.Email == model.Email);
            if (existingUser == null)
            {
                ModelState.AddModelError("Email", "Either the email or password was incorrect");
                //return View();
                return RedirectToAction("Index", "Home");
            }

            // validate the incoming password with the password hash in the database
            var passwordVerified = BCrypt.Net.BCrypt.Verify(model.Password, existingUser.PasswordHash);
            if (!passwordVerified)
            {
                ModelState.AddModelError("Email", "Either the email or password was incorrect");
                //return View();
                return RedirectToAction("Index", "Home");
            }

            if (existingUser.UserRole == UserRole.Admin)
            {
                SetEncryptedUserCookie("ADMIN", existingUser.Id.ToString());
                return RedirectToAction("Index", "Admin");
            }
            else {
                // if it matches, set a cookie with the userId
                SetEncryptedUserCookie("USER_ID", existingUser.Id.ToString());
                ViewData["User"] = existingUser;
            }

                // redirect to admin panel
                return View("Members");

        }


        //register
        public IActionResult Register()
        {
            //only available if logged in
            //gets user cookie if already logged in
            var userIdCookie = GetEncryptedUserCookie("USER_ID");

            if (userIdCookie != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Register submit view
        // [POST] /Login/Register
        [HttpPost]
        public IActionResult Register(Register model)
        {

            //only available if logged in
            var userIdCookie = GetEncryptedUserCookie("USER_ID");
            if (userIdCookie != null)
            {
                var LogginUser = _CoWork454Context.User.SingleOrDefault(l => l.Id == Convert.ToInt32(userIdCookie));
                ViewData["User"] = LogginUser;
                return View("Members");
            }

            // check if email already exists in database (if so throw exception)
            var existingUser = _CoWork454Context.User.SingleOrDefault(u => u.Email == model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already in use");
                //return View();
                return RedirectToAction("Index", "Home");
            }

            // hash the incoming password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // create a new user record
            var User = new User();
            User.FirstName = model.FirstName;
            User.LastName = model.LastName;
            User.Email = model.Email;
            User.CompanyName = model.CompanyName;
            User.Phone = model.Phone;
            User.PasswordHash = passwordHash;


            try
            {
                _CoWork454Context.User.Add(User);
                _CoWork454Context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                //return View();
                return RedirectToAction("Index", "Home");
            }

            // redirect to the login
            return RedirectToAction("Index", "Home");
        }

        // /User/Logout
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("USER_ID");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Members()
        {
            var userIdCookie = GetEncryptedUserCookie("USER_ID");
            if (userIdCookie == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var existingUser = _CoWork454Context.User.SingleOrDefault(l => l.Id == Convert.ToInt32(userIdCookie));
                ViewData["User"] = existingUser;
                return View();
            }
        }

        protected string GetEncryptedUserCookie(string cookieKey)
        {
            var encryptedShopCookie = HttpContext.Request.Cookies[cookieKey];

            if (encryptedShopCookie == null)
            {
                return null;
            }

            var cookieContent = EncryptionHelper.DecryptString(encryptedShopCookie, EncryptionHelper.EncryptionKey);

            return cookieContent;
        }

        protected void SetEncryptedUserCookie(string cookieKey, string cookieValue)
        {
            var cookieOptions = new CookieOptions();

            if (cookieValue != "GUEST")
            {
                cookieOptions.Expires = DateTimeOffset.MaxValue;
            }

            var encryptedCookieContent = EncryptionHelper.EncryptString(cookieValue, EncryptionHelper.EncryptionKey);

            HttpContext.Response.Cookies.Append(cookieKey, encryptedCookieContent, cookieOptions);
        }

    }
}