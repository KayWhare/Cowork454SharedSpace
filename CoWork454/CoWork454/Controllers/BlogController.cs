using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Common;
using CoWork454.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Controllers
{
    public class BlogController : Controller
    {

        private readonly CoWork454Context _CoWork454Context;

        public BlogController(CoWork454Context tastingsContext)
        {
            _CoWork454Context = tastingsContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var userIdCookie = GetEncryptedUserCookie("USER_ID");

            if (userIdCookie == null)
            {
                //can't make a booking without login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var currentBookings = _CoWork454Context.Booking.Include(b => b.Order)
                    .Where(b => b.Order.UserId == Convert.ToInt32(userIdCookie)).ToList();
                ViewData["Bookings"] = currentBookings;
                ViewData["Products"] = _CoWork454Context.Product.ToList();
                ViewData["User"] = _CoWork454Context.User.SingleOrDefault(u => u.Id == Convert.ToInt32(userIdCookie));
            }
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
