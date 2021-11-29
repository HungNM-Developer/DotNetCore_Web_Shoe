using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web_Shoe_PTWeb.Models;

namespace Web_Shoe_PTWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Web_Shoe_PTWebContext _context;

        public HomeController(ILogger<HomeController> logger, Web_Shoe_PTWebContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //var catList = _context.Categories.ToList();
            //ViewBag.catList = catList;
            var productList = _context.Products.ToList().GetRange(5, 4);
            ViewBag.productList = productList;
            return View();
        }

        public IActionResult Privacy()
        {
            //var catList = _context.Categories.ToList();
            //ViewBag.catList = catList;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
