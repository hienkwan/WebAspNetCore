using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoreShop_V2.Areas.Client.ViewModel;
using CoreShop_V2.Helper;
using CoreShop_V2.Paypal;
using FinalProjectASPDotnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoreShop.Controllers
{
    [Area("client")]
    public class CalculationController : Controller
    {
        private MyDbContext _context;
        
        public CalculationController(MyDbContext context)
        {
            _context = context;
        }

        [Route("Cart")]
        public IActionResult Cart()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            //var cart = HttpContext.Session.GetString("cart");
            //ViewBag.total = cart.Sum(item => item.Product.Price* item.Quantity);
            ViewBag.cart = cart;
            ViewBag.index = 1;
            return View();
        }
                
        [Route("Buy")]
        [HttpPost]
        public IActionResult Buy(string id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { product = _context.Product.FirstOrDefault(p => p.ProductId == id), Quantity = 1 });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { product = _context.Product.FirstOrDefault(p => p.ProductId == id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return Json("Success");
        }

        [Route("Remove")]
        [HttpPost]
        public IActionResult Remove(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);            
            return Json("Success");
        }

        [Route("RemoveAll")]
        [HttpPost]
        public IActionResult RemoveAll(string[] arrayId)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            foreach(string id in arrayId)
            {
                int index = isExist(id);
                cart.RemoveAt(index);
            }            
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json("Success");
        }

        [Route("Updatecart")]
        [HttpPost]
        public IActionResult UpdateCart(string data)
        {
            UpdateCartViewModel[] result = JsonConvert.DeserializeObject<UpdateCartViewModel[]>(data);
            for (int i = 0; i < result.Length; i++)
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(result[i].Id);
                string id = result[i].Id;
                if (index != -1)
                {
                    cart[index].Quantity = Convert.ToInt32(result[i].Quantity);
                }
                else
                {
                    cart.Add(new CartItem { product = _context.Product.FirstOrDefault(p => p.ProductId == id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return Json(result.Length);
        }

        private int isExist(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        

        [Route("Checkout")]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {   
            checkoutViewModel.cartItem = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session,"cart");
            checkoutViewModel.payPalConfig = PayPalService.GetPayPalConfig();

            return View(checkoutViewModel);
        }

        [Route("Checkout")]
        [HttpPost]
        public IActionResult Checkvalid(CheckoutViewModel checkoutViewModel)
        {
            checkoutViewModel.cartItem = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            checkoutViewModel.payPalConfig = PayPalService.GetPayPalConfig();

            if (ModelState.IsValid)
            {              
                return RedirectPreserveMethod(checkoutViewModel.payPalConfig.PostUrl);
            }
            return View("checkout",checkoutViewModel);
        }

        [Route("Checkout/Success")]
        public IActionResult Success(CheckoutViewModel checkoutViewModel)
        {
            var result = PDTHolder.Success(Request.Query["tx"].ToString());
            checkoutViewModel.cartItem = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            SessionHelper.RemoveSession(HttpContext.Session, "cart");
            return View(checkoutViewModel);
        }
    }
}