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

        //HttpGet
        [Route("checkout", Name = "Checkout")]
        public async Task<IActionResult> Index(Order order)
        {
            //returnUrl ??= Url.Content("~/");
            var khachhang = await _userManager.GetUserAsync(HttpContext.User);
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            CheckoutModel modelCheckout = new CheckoutModel();

            modelCheckout.UserId = khachhang.Id;
            modelCheckout.FirstName = khachhang.FirstName;
            modelCheckout.LastName = khachhang.LastName;

            var user = _context.AspNetUsers.SingleOrDefault(x => x.Id == khachhang.Id);
            modelCheckout.PhoneNumber = user.PhoneNumber;

            modelCheckout.ShipAdress = order.ShipAdress;

            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.PromationPrice * item.Amount);
            return View(modelCheckout);

        }

        [HttpPost]
        [Route("checkout", Name = "Checkout")]
        public async Task<IActionResult> Index(CheckoutModel checkout)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var khachhang = await _userManager.GetUserAsync(HttpContext.User);

            CheckoutModel modelCheckout = new CheckoutModel();


            modelCheckout.UserId = khachhang.Id;
            modelCheckout.FirstName = khachhang.FirstName;
            modelCheckout.LastName = khachhang.LastName;
            modelCheckout.PhoneNumber = khachhang.PhoneNumber;
            modelCheckout.ShipAdress = checkout.ShipAdress;
            var user = _context.AspNetUsers.SingleOrDefault(x => x.Id == khachhang.Id);
            user.PhoneNumber = checkout.PhoneNumber;
            
            _context.AspNetUsers.Update(user);
            _context.SaveChanges();
       
            try {
                if (ModelState.IsValid)
                {
                    Order donhang = new Order();
                    donhang.UserId = modelCheckout.UserId;
                    donhang.ShipAdress = modelCheckout.ShipAdress;
                    donhang.Status = "Processing";
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
            catch(Exception ex)
            {
                //ViewBag.cart = cart;
                //ViewBag.donhang = donhang;
                return View(modelCheckout);
            }
            ViewBag.cart = cart;
            return View(modelCheckout);


        }

        [Route("success", Name = "Success")]
        public IActionResult Success()
        {
            
            return View();

        }
    }
}
