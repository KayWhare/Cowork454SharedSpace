using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoWork454.Controllers
{
    public class SpacesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}