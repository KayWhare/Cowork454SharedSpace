using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Common;
using CoWork454.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoWork454.Models;

namespace CoWork454.Controllers
{
    public class AdminController : Controller
    {

        private readonly CoWork454Context _CoWork454Context;

        public AdminController(CoWork454Context tastingsContext)
        {
            _CoWork454Context = tastingsContext;
        }

        public IActionResult Index()
        {
            var userIdCookie = GetEncryptedUserCookie("ADMIN");
            if (userIdCookie == null)
            {
                //Add back when we have proper admin user
                return RedirectToAction("Index", "Home");

            }
            var existingUser = _CoWork454Context.User.SingleOrDefault(l => l.Id == Convert.ToInt32(userIdCookie));
            ViewData["User"] = existingUser;
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