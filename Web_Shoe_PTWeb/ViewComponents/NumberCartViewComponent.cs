using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Shoe_PTWeb.Helpers;
using Web_Shoe_PTWeb.Models;

namespace Web_Shoe_PTWeb.ViewComponents
{
    public class NumberCartViewComponent : ViewComponent
    {
        private readonly Web_Shoe_PTWebContext _context;
        public NumberCartViewComponent(Web_Shoe_PTWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            //int soluongsanpham = 0;
            //if (cart != null)
            //{
            //    soluongsanpham = cart.Count();
            //}
            return View(cart);
        }
    }
}
