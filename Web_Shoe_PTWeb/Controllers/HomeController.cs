using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var productList = _context.Products.ToList().GetRange(5, 4);
            ViewBag.productList = productList;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Search(string q)
        {
            List<Product> productList = _context.Products.Where(p => p.Name.Contains(q)).ToList();
            ViewBag.productList = productList;
            return View("Index");
        }
        
    }
}
