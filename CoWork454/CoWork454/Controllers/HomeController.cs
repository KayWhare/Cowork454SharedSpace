using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoWork454.Models;
using CoWork454.Common;
using CoWork454.Data;

namespace CoWork454.Controllers
{
    public class HomeController : Controller
    {


        private readonly CoWork454Context _CoWork454Context;

        public HomeController(CoWork454Context tastingsContext)
        {
            _CoWork454Context = tastingsContext;
        }



        public IActionResult Index()
        {
            var userIdCookie = GetEncryptedUserCookie("USER_ID");
            if (userIdCookie != null)
            {
                var existingUser = _CoWork454Context.User.SingleOrDefault(l => l.Id == Convert.ToInt32(userIdCookie));
                ViewData["User"] = existingUser;

            }
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
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

    }

}
