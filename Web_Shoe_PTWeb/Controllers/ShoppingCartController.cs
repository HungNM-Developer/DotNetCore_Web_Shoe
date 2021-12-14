using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Shoe_PTWeb.Helpers;
using Web_Shoe_PTWeb.Models;

namespace Web_Shoe_PTWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly Web_Shoe_PTWebContext _context;

        public ShoppingCartController(Web_Shoe_PTWebContext context)
        {
            _context = context;
        }

        //[Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.PromationPrice * item.Amount);
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(int Id)
        {
            //Product productModel = new Product();
            try
            {
                Product product = _context.Products.SingleOrDefault(p => p.ProductId == Id);
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                if (cart == null)
                {
                    List<CartItem> cartNew = new List<CartItem>();

                    cartNew.Add(new CartItem { Product = product, Amount = 1 });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cartNew);
                }
                else
                {
                    //List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                    int index = isExist(Id);
                    if (index != -1)
                    {
                        cart[index].Amount++;
                    }
                    else
                    {
                        cart.Add(new CartItem { Product = product, Amount = 1 });
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }

        public IActionResult UpdateCart(int Id, int? Amount)
        {
            //Product product = _context.Products.SingleOrDefault(p => p.ProductId == Id);
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(Id);
            if (cart != null)
            {
                CartItem item = cart.SingleOrDefault(p => p.Product.ProductId == Id);
                if (item != null && Amount.HasValue)
                {
                    item.Amount = Amount.Value;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }
            return RedirectToAction("Index");

        }
        //[Route("remove/{id}")]
        public IActionResult Remove(int Id)
        {
            try
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(Id);
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }

        private int isExist(int Id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(Id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
