using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Shoe_PTWeb.Areas.Identity.Data;
using Web_Shoe_PTWeb.Helpers;
using Web_Shoe_PTWeb.Models;

namespace Web_Shoe_PTWeb.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Web_Shoe_PTWebContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CheckoutController(Web_Shoe_PTWebContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("checkout", Name = "Checkout")]
        public async Task<IActionResult> Index(CheckoutModel model, string returnUrl = null)
        {
            //returnUrl ??= Url.Content("~/");
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            //var khachhang = new AspNetUser();
            //var userId = _userManager.GetUserId(user);
            var khachhang = await _userManager.GetUserAsync(HttpContext.User);
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            //var khachhang = _context.AspNetUsers.SingleOrDefault(kh => kh.Id == HttpContext.User.);
            //var khachhang = _context.AspNetUsers.Where(x => x.Id == model.UserId);
            //AspNetUser uid = new AspNetUser();
            //HttpContext.Session.SetString("UserId", uid.Id);
            //if(checkoutID != null)

            model.UserId = khachhang.Id;
            model.FirstName = khachhang.FirstName;
            model.LastName = khachhang.LastName;
            model.PhoneNumber = khachhang.PhoneNumber;
            //model.ShipAdress =khachhang.;

            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.PromationPrice * item.Amount);
            return View(model);

        }

        [HttpPost]
        [Route("checkout", Name = "Checkout")]
        public async Task<IActionResult> Index(CheckoutModel checkout)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var khachhang = await _userManager.GetUserAsync(HttpContext.User);

            CheckoutModel model = new CheckoutModel();

            
            model.UserId = khachhang.Id;
            model.FirstName = khachhang.FirstName;
            model.LastName = khachhang.LastName;
            model.PhoneNumber = khachhang.PhoneNumber;
            model.ShipAdress = checkout.ShipAdress;
            
            //_context.Update(khachhang);
            _context.SaveChanges();
            //}
            try {
                if (ModelState.IsValid)
                {
                    Order donhang = new Order();
                    donhang.UserId = model.UserId;
                    donhang.ShipAdress = model.ShipAdress;
                    donhang.Status = "";
                    donhang.OrderDate = DateTime.Now;
                    donhang.Total = Convert.ToDouble(cart.Sum(x => x.TotalMoney));
                    _context.Add(donhang);
                    _context.SaveChanges();

                    foreach (var item in cart)
                    {
                        OrderDetail chitietdonhang = new OrderDetail();
                        chitietdonhang.OrderId = donhang.OrderId;
                        chitietdonhang.ProductId = item.Product.ProductId;
                        chitietdonhang.Quantity = item.Amount;
                        chitietdonhang.Price = item.Product.Price;
                        _context.Add(chitietdonhang);
                    }
                    _context.SaveChanges();

                    HttpContext.Session.Remove("cart");

                    ViewBag.cart = cart;
                    return RedirectToAction("Success");
                }
            }
            catch(Exception e)
            {
                ViewBag.cart = cart;
                //ViewBag.donhang = donhang;
                return View(model);
            }
            
            return View(model);


        }

        [Route("success", Name = "Success")]
        public IActionResult Success()
        {
            
            return View();

        }
    }
}
