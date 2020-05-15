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
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Controllers
{
    public class practiceController : Controller
    {

        private readonly CoWork454Context _CoWork454Context;

        public practiceController(CoWork454Context context)
        {
            _CoWork454Context = context;
        }
        // GET: /<controller>/
        public IActionResult Availability(MakeBooking makeBooking)
        {
            Booking booking = new Booking()
            {
                Date_start = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeStart}", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),

                Date_end = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeFinish}", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            };

            var currentBookings = _CoWork454Context.Booking
                .Where(b => b.Product.ProductClass == booking.Product.ProductClass);

            var productsList = _CoWork454Context.Product
                .Where(p => p.ProductClass == booking.Product.ProductClass
                && p.isAvailable == true).ToList();

            foreach (var b in currentBookings)
            {
                if (b.Date_start <= booking.Date_end && booking.Date_start <= b.Date_end)
                {
                    productsList.Remove(productsList.SingleOrDefault(p => p.Id == b.ProductId));
                }
            };

            ViewData["Products"] = productsList;

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
