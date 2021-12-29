using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        ////////////////////////////////////////////////////////////////////


        //// Key lưu chuỗi json của Cart
        //public const string CARTKEY = "cart";

        //// Lấy cart từ Session (danh sách CartItem)
        //List<CartItem> GetCartItems()
        //{

        //    var session = HttpContext.Session;
        //    string jsoncart = session.GetString(CARTKEY);
        //    if (jsoncart != null)
        //    {
        //        return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
        //    }
        //    return new List<CartItem>();
        //}

        //// Xóa cart khỏi session
        //void ClearCart()
        //{
        //    var session = HttpContext.Session;
        //    session.Remove(CARTKEY);
        //}

        //// Lưu Cart (Danh sách CartItem) vào session
        //void SaveCartSession(List<CartItem> ls)
        //{
        //    var session = HttpContext.Session;
        //    string jsoncart = JsonConvert.SerializeObject(ls);
        //    session.SetString(CARTKEY, jsoncart);
        //}


        //////////////////////////////////////////////////////////////////////
        ///// Thêm sản phẩm vào cart
        //[Route("addcart/{productid:int}", Name = "addcart")]
        //public IActionResult AddToCart([FromRoute] int productid)
        //{

        //    var product = _context.Products
        //        .Where(p => p.ProductId == productid)
        //        .FirstOrDefault();
        //    if (product == null)
        //        return NotFound("Không có sản phẩm");

        //    // Xử lý đưa vào Cart ...
        //    var cart = GetCartItems();
        //    var cartitem = cart.Find(p => p.Product.ProductId == productid);
        //    if (cartitem != null)
        //    {
        //        // Đã tồn tại, tăng thêm 1
        //        cartitem.Amount++;
        //    }
        //    else
        //    {
        //        //  Thêm mới
        //        cart.Add(new CartItem() { Amount = 1, Product = product });
        //    }

        //    // Lưu cart vào Session
        //    SaveCartSession(cart);
        //    // Chuyển đến trang hiện thị Cart
        //    return RedirectToAction(nameof(Cart));
        //}
        ///// xóa item trong cart
        //[Route("/removecart/{productid:int}", Name = "removecart")]
        //public IActionResult RemoveCart([FromRoute] int productid)
        //{
        //    var cart = GetCartItems();
        //    var cartitem = cart.Find(p => p.Product.ProductId == productid);
        //    if (cartitem != null)
        //    {
        //        // Đã tồn tại, tăng thêm 1
        //        cart.Remove(cartitem);
        //    }

        //    SaveCartSession(cart);
        //    return RedirectToAction(nameof(Cart));
        //}

        //[Route("/updatecart", Name = "updatecart")]
        //[HttpPost]
        //public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        //{
        //    // Cập nhật Cart thay đổi số lượng quantity ...
        //    var cart = GetCartItems();
        //    var cartitem = cart.Find(p => p.Product.ProductId == productid);
        //    if (cartitem != null)
        //    {
        //        // Đã tồn tại, tăng thêm 1
        //        cartitem.Amount = quantity;
        //    }
        //    SaveCartSession(cart);
        //    // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
        //    return Ok();
        //}


        //// Hiện thị giỏ hàng
        //[Route("/cart", Name = "cart")]
        //public IActionResult Cart()
        //{
        //    ViewBag.cart = GetCartItems();
        //    ViewBag.total = GetCartItems().Sum(item => item.Product.PromationPrice * item.Amount);
        //    return View(GetCartItems());
        //}

        //[Route("/checkout")]
        //public IActionResult CheckOut()
        //{
        //    // Xử lý khi đặt hàng
        //    return View();
        //}

    }
}
