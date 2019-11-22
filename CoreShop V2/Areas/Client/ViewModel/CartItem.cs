using FinalProjectASPDotnet.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Client.ViewModel
{
    public class CartItem
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => product.Price * Quantity;

        
    }
}
