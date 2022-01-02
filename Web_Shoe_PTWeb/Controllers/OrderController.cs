using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Shoe_PTWeb.Areas.Identity.Data;
using Web_Shoe_PTWeb.Models;

namespace Web_Shoe_PTWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly Web_Shoe_PTWebContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(
            Web_Shoe_PTWebContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
        )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListAsync()
        {
            //var catList = _context.Categories.ToList();
            //ViewBag.catList = catList;
            var khachhang = await _userManager.GetUserAsync(HttpContext.User);
            List<Order> orderList = _context.Orders.Where(p => p.UserId == khachhang.Id).ToList();
            ViewBag.orderList = orderList;
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            try {
                var khachhang = await _userManager.GetUserAsync(HttpContext.User);
                if(khachhang == null)
                {
                    return NotFound();
                }
                var donhang = _context.Orders.Include(x => x.OrderDetails).ThenInclude(orderDetail => orderDetail.Product).FirstOrDefault(x => x.OrderId == id && x.UserId == khachhang.Id);
                if(donhang == null)
                {
                    return NotFound();
                }
                //var chitietdonhang = _context.OrderDetails.Where(x => x.OrderId == id).OrderBy(x => x.OrderDetailId).ToList();

                ViewBag.donhang = donhang;
                //ViewBag.chitietdonhang = chitietdonhang;
                return View(donhang);
            
            } catch {
                return NotFound();
            }

        }
    }
}
