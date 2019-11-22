using FinalProjectASPDotnet.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Client.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Product> HangHoa { get; set; }
        public IEnumerable<Product> NewArrivalHangHoa { get; set; }
        public IEnumerable<Product> TopSellHangHoa { get; set; }

    }
}
