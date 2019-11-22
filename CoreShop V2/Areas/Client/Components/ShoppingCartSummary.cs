using CoreShop_V2.Areas.Client.ViewModel;
using CoreShop_V2.Helper;
using FinalProjectASPDotnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Client.Components
{
    [Area("Client")]
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly MyDbContext _context;
        public ShoppingCartSummary(MyDbContext context)
        {
            _context = context;
        }


        public Task<IViewComponentResult> InvokeAsync()
        {
            List<CartItem> list = GetCartAsync();
            if (list == null) { list = new List<CartItem>(); }
            ShoppingCartViewModel giohang = new ShoppingCartViewModel() { Items = list };
            return Task.FromResult<IViewComponentResult>(View(giohang));
        }

        private List<CartItem> GetCartAsync()
        {
            List<CartItem> cart = new List<CartItem>();
            cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            return cart;
        }
    }



}
