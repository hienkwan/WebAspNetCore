using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Client.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> Items { get; set; }
        public int TotalQuantity { get; set; }
        public ShoppingCartViewModel()
        {
            TotalQuantity = 0;
        }
    }
}
