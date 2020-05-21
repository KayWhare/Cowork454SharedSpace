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

        [HttpPost]
        public IActionResult Index(MakeBooking makeBooking)
        {
            //change makeBooking to Booking and convert to DateTimeOffset

            if(makeBooking.Date == null)
            {
                return PartialView("_MakeBookingPartial");
            }
            Booking booking = new Booking()
            {
                ProductId = makeBooking.ProductId,
                Date_start = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeStart}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Date_end = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeFinish}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
            //retrieve all bookings of the same productclass from DB if any
            var currentBookings = _CoWork454Context.Booking
                .Where(b => b.Product.ProductClass == makeBooking.ProductClass);

            //retrieve all products of the same productClass that isAvailable
            var availableProducts = _CoWork454Context.Product
                .Where(p => p.ProductClass == makeBooking.ProductClass
                && p.isAvailable == true).ToList();

            //if not null, check the incoming booking request times for overlaps with currentbookings
            if(currentBookings != null)
            {
                foreach (var b in currentBookings)
                {   //this is an algorithm that checks for overlap
                    if (b.Date_start <= booking.Date_end && booking.Date_start <= b.Date_end)
                    {
                        //if overlap occurs, remove from available products list
                        availableProducts.Remove(availableProducts.SingleOrDefault(p => p.Id == b.ProductId));
                    }
                };
            };

            var userIdCookie = GetEncryptedUserCookie("USER_ID");
            if (userIdCookie != null)
            {
                var LogginUser = _CoWork454Context.User.SingleOrDefault(l => l.Id == Convert.ToInt32(userIdCookie));
                ViewData["User"] = LogginUser;
            }
            ViewData["BookingRequest"] = makeBooking;
            ViewData["Products"] = availableProducts;

            var referer = Request.Headers.SingleOrDefault(h => h.Key == "Referer").Value.ToString();
            var refererParts = referer.Split('/');


            if (refererParts[3] == "Booking")
            {
                return PartialView("_MakeBookingPartial");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddBooking(MakeBooking makeBooking)
        {
            //create a booking from makebooking. Parse the date/time strings to a datetimeoffset. 
            Booking booking = new Booking()
            {
                ProductId = makeBooking.ProductId,
                Date_start = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeStart}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Date_end = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeFinish}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };

            var orderIdCookie = GetEncryptedUserCookie("ORDER_ID");

            if (orderIdCookie == null)
            {

                // create new order
                var order = new Order();

                // create bookings list and add to new booking
                order.Bookings = new List<Booking>();
                order.Bookings.Add(booking);

                var userId = GetEncryptedUserCookie("USER_ID");
                if (userId != null)
                {
                    order.UserId = Convert.ToInt32(userId);
                }

                //check for lapsed bookings and remove
                _CoWork454Context.Booking.RemoveRange(_CoWork454Context.Booking.Where(b => b.Date_end <= DateTimeOffset.Now));

                // add the order to the context, save changes to update database
                _CoWork454Context.Add(order);
                _CoWork454Context.SaveChanges();

                // set the orderId in a cookie
                SetEncryptedUserCookie("ORDER_ID", order.Id.ToString());

                // we are going to show the user

                var currentBookings = _CoWork454Context.Booking.Include(o => o.Order)
                    .Where(o => o.Order.UserId == order.UserId).ToList();
                ViewData["Bookings"] = currentBookings;
                ViewData["Products"] = _CoWork454Context.Product.ToList();
                ViewData["BookingRequest"] = makeBooking;
                ViewData["User"] = _CoWork454Context.User
                    .SingleOrDefault(u => u.Id == order.UserId);
            }
            else
            {
                // already has an order
                var orderId = Convert.ToInt32(orderIdCookie);

                // get the order from the database
                var order = _CoWork454Context.Order.
                    Include(o => o.Bookings)
                    .SingleOrDefault(o => o.Id == orderId);

                if (order.Bookings == null)
                {
                    return NotFound();
                }

                // get the existing order item/s with the same ProductID, if any

                var existingBookings = _CoWork454Context.Booking
                    .SingleOrDefault(b => b.OrderId == orderId &&
                    b.ProductId == booking.ProductId);

                if (existingBookings == null)
                {
                    //no bookings of same product on the order so add booking
                    order.Bookings.Add(booking);

                    //update database
                    _CoWork454Context.SaveChanges();
                }
                else
                {
                    //Already has booking of that product id so make new order
                    var newOrder = new Order();

                    // create bookings list and add booking
                    newOrder.Bookings = new List<Booking>();
                    newOrder.Bookings.Add(booking);

                    var userId = GetEncryptedUserCookie("USER_ID");
                    if (userId != null)
                    {
                        newOrder.UserId = Convert.ToInt32(userId);
                    }

                    // add the order to the context, save changes to update database
                    _CoWork454Context.Add(newOrder);
                    _CoWork454Context.SaveChanges();

                    // set the orderId in a cookie
                    SetEncryptedUserCookie("ORDER_ID", newOrder.Id.ToString());

                }


                //update viewdata
                var currentBookings = _CoWork454Context.Booking.Include(o => o.Order)
                    .Where(o => o.Order.UserId == order.UserId).ToList();
                ViewData["Bookings"] = currentBookings;
                ViewData["Products"] = _CoWork454Context.Product.ToList();
                ViewData["User"] = _CoWork454Context.User
                    .SingleOrDefault(u => u.Id == order.UserId);
            }

            return View("Members");
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
