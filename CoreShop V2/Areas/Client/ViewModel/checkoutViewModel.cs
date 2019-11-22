using CoreShop_V2.Paypal;
using FinalProjectASPDotnet.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Client.ViewModel
{
    public class CheckoutViewModel
    {
        public Bill bill { get; set; }
        public List<CartItem> cartItem { get; set; }
        public PayPalConfig payPalConfig { get; set; }
    }
}
