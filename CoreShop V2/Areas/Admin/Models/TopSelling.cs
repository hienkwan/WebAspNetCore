using FinalProjectASPDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectASPDotnet.Areas.Admin.Models
{
    public class TopSelling
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int? SellingQuantity { get; set; }
        public Product Product { get; set; }
    }
}
