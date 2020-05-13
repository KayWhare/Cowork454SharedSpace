using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoWork454.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            var userIdCookie = GetEncryptedUserCookie("ADMIN");
            if (userIdCookie != null)
            {
                return View();

            }

            return RedirectToAction("Index", "Home");
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