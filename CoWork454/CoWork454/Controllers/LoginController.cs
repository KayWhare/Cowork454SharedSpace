using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoWork454.Data;
using CoWork454.Models;
using CoWork454.Controler;

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
            return View();
        }



        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            // lookup the user by email address
            var existingUser = _CoWork454Context.User.SingleOrDefault(l => l.Email == model.Email);
            if (existingUser == null)
            {
                ModelState.AddModelError("UserName", "Either the user name or password was incorrect");
                return View();
            }

            // validate the incoming password with the password hash in the database
            //var passwordVerified = BCrypt.Net.BCrypt.Verify(model.Pass, existingUser.PasswordHash);
            var passwordVerified = _CoWork454Context.User.SingleOrDefault(l => l.PasswordHash == model.Password);
            //replace above
            if (passwordVerified == null)
            {
                ModelState.AddModelError("UserName", "Either the user name or password was incorrect");
                return View();
            }

            if (existingUser.UserRole == 1)
            {
                //return admin page
            }

            // if it matches, set a cookie with the userId
            //SetEncryptedMemberCookie("USER_ID", existingUser.Id.ToString());

            // redirect to admin panel
            return RedirectToAction("Index", "Home");

        }

    }
}