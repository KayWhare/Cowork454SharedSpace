using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoWork454.Common;
using CoWork454.Data;
using CoWork454.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoWork454.Controllers
{
    public class BlogController : Controller
    {

        private readonly CoWork454Context _CoWork454Context;
        private readonly IConfiguration _Configuration;

        public BlogController(CoWork454Context tastingsContext, IConfiguration configuration)
        {
            _CoWork454Context = tastingsContext;
            _Configuration = configuration;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var userIdCookie = GetEncryptedUserCookie("USER_ID");

            ViewData["BlogPosts"] = _CoWork454Context.BlogPost.ToList();
            ViewData["User"] = _CoWork454Context.User.SingleOrDefault(u => u.Id == Convert.ToInt32(userIdCookie));

            return View();
        }


        [HttpPost]
        public IActionResult Index(IFormFile file, [FromForm] BlogPost post)
        {
            var userIdCookie = GetEncryptedUserCookie("USER_ID");

            if (userIdCookie == null)
            {
                //can't Blog without login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string filePath = null;
                using (var stream = file.OpenReadStream())
                {
                    var connectionString = _Configuration.GetConnectionString("StorageConnection");
                    filePath = AzureStorage.AddUpdateFile(file.FileName, stream, connectionString, "Team1");
                }

                post.ImageUrl = filePath;
                _CoWork454Context.Add(post);
                _CoWork454Context.SaveChanges();

                return new JsonResult(post);
            }
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
