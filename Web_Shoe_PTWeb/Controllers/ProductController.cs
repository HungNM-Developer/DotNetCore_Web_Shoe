using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Shoe_PTWeb.Models;

namespace Web_Shoe_PTWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly Web_Shoe_PTWebContext _context;

        public ProductController(Web_Shoe_PTWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var catList = _context.Categories.ToList();
            //ViewBag.catList = catList;
            return View();
        }

        public IActionResult List(int id)
        {
            //var catList = _context.Categories.ToList();
            //ViewBag.catList = catList;
            List<Product> productList = _context.Products.Where(p => p.CategoryId == id).ToList();
            ViewBag.productList = productList;
            return View();
        }

        public IActionResult Detail(int id)
        {
            try
            {
                //var catList = _context.Categories.ToList();
                //ViewBag.catList = catList;
                Product product = _context.Products.Find(id);
                ViewBag.product = product;
                return View();
            }
            catch
            {
                return View();
            }
            
        }
    }
}
