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

        [HttpPost]
        public IActionResult Index(MailingList mail)
        {
            var existingEmail = _CoWork454Context.MailingList.SingleOrDefault(m => m.Email == mail.Email);
            if(existingEmail == null)
            {
                _CoWork454Context.MailingList.Add(mail);
                _CoWork454Context.SaveChanges();

                ViewData["Subscribe"] = "Thank you for subscribing.";
            }
            else
            {
                ViewData["Subscribe"] = "You've already subscribed. Thanks again";
            }
            return PartialView("_SubscribeConfirm");
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
