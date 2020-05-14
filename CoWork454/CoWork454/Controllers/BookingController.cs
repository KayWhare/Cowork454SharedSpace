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
        public IActionResult Index(MakeBooking makeBooking)
        {
            var BookingDate = DateTimeOffset.ParseExact($"{makeBooking.Date} ", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var ordType = makeBooking.ProductClass;
            var bookings = _CoWork454Context.Booking.Include(b => b.Product).Where(b => b.Product.ProductClass == ordType && b.Date_start.Date == BookingDate).OrderBy(b => b.Date_start);
            var ProductList = _CoWork454Context.Product.Where(p => p.ProductClass == ordType).ToList();

            
            var BookingTimeStart = DateTimeOffset.ParseExact($"{makeBooking.TimeStart}", "hh:mm", CultureInfo.InvariantCulture);
            var BookingTimeEnd = DateTimeOffset.ParseExact($"{makeBooking.TimeFinish}", "hh:mm", CultureInfo.InvariantCulture);
            var isAvail = new Dictionary<int, bool>();


            foreach (var order in bookings){
                var Productid = order.ProductId;
                if (!isAvail.ContainsKey(Productid))
                {
                    isAvail.Add(Productid, true);

                }
                if (isAvail[Productid])
                {
                    if (order.Date_start.TimeOfDay.CompareTo(BookingTimeStart) == -1)//start time before requested start time
                    {
                        if (order.Date_end.TimeOfDay.CompareTo(BookingTimeEnd) == 1)//booking end time after requested start time
                        {
                            isAvail[Productid] = false; //set false because overlap
                        }

                    }
                    else if (order.Date_start.TimeOfDay.CompareTo(BookingTimeStart) == 0)//start time same as requested start time
                    {
                        isAvail[Productid] = false; //set false because overlap
                    }
                    else //start time must be after requested start
                    {
                        if(order.Date_start.TimeOfDay.CompareTo(BookingTimeEnd) == -1)
                        {
                            isAvail[Productid] = false; //set false because overlap
                        }
                    }
                }

                foreach (var Product in isAvail)
                {
                    if (!Product.Value)
                    {
                        ProductList.Remove(ProductList.Single(p => p.Id == Product.Key));
                    }
                }

                return new JsonResult(ProductList);


            }




            return View("Members", "Login");
        }
        [HttpPost]
        public IActionResult OldIndex(MakeBooking makeBooking)
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
            Booking booking = new Booking()
            {
                Date_start = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeStart}", "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture),

                Date_end = DateTimeOffset.ParseExact($"{makeBooking.Date} {makeBooking.TimeFinish}", "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture)
            };

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
