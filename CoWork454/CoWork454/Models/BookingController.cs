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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Models
{
    public class BookingController : Controller
    {
        private readonly CoWork454Context _CoWork454Context;

        public BookingController(CoWork454Context tastingsContext)
        {
            _CoWork454Context = tastingsContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var orderIdCookie = GetEncryptedUserCookie("ORDER_ID");
            if (orderIdCookie == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var existingOrder = _CoWork454Context.Booking.SingleOrDefault(b => b.OrderId == Convert.ToInt32(orderIdCookie));
                ViewData["Bookings"] = existingOrder;
                ViewData["Products"] = _CoWork454Context.Product.ToList();
            }
            //return View();
            return View("Members");
        }

        [HttpPost]
        public IActionResult Index(Booking booking)
        {
            var orderIdCookie = GetEncryptedUserCookie("ORDER_ID");

            if (!ModelState.IsValid)
            {
                // there is a model error
                if (orderIdCookie == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    
                    var existingOrder = _CoWork454Context.Booking.SingleOrDefault(b => b.OrderId == Convert.ToInt32(orderIdCookie));
                    ViewData["Bookings"] = existingOrder;
                    ViewData["Products"] = _CoWork454Context.Product.ToList();
                }
                return View("Members", "Login");
            }

            if (orderIdCookie == null)
            {
                // create new order
                var order = new Order();

                order.Bookings = new List<Booking>();
                order.Bookings.Add(booking);

                var userId = GetEncryptedUserCookie("USER_ID");
                if (userId != null)
                {
                    order.UserId = Convert.ToInt32(userId);
                }

                // add the order to the context, save changes to update database
                _CoWork454Context.Add(order);
                _CoWork454Context.SaveChanges();

                // set the orderId in a cookie
                SetEncryptedUserCookie("ORDER_ID", order.Id.ToString());

                // we are going to show the user 

                ViewData["Bookings"] = _CoWork454Context.Booking.Where(b => b.OrderId == Convert.ToInt32(orderIdCookie));
            }
            else
            {
                // already has a order so get from database
                var orderId = Convert.ToInt32(orderIdCookie);

                // get the order from the database
                var order = _CoWork454Context.Order
                    .SingleOrDefault(o => o.Id == orderId);

                if (order == null)
                {
                    return NotFound();
                }

                // get the existing order item, if any
                var existingBooking = order.Bookings
                    .SingleOrDefault(b => b.ProductId == booking.ProductId);

                // add new booking to order or replace existing booking
                if (existingBooking == null)
                {
                    // new item
                    order.Bookings.Add(booking);
                }
                else
                {
                    existingBooking.Date_start = booking.Date_start;
                    existingBooking.Date_end = booking.Date_end;
                    _CoWork454Context.Update(existingBooking);
                }

                _CoWork454Context.SaveChanges();


                //update viewdata
                var existingOrder = _CoWork454Context.Booking.SingleOrDefault(b => b.OrderId == Convert.ToInt32(orderIdCookie));
                ViewData["Bookings"] = existingOrder;
                ViewData["Products"] = _CoWork454Context.Product.ToList();
            }

            return View("Members","Login");
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
