using FinalProjectASPDotnet.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Admin.ViewModel
{
    public class ProductManagementViewModel
    {        
        public List<Product> product { get; set; }
        public Product AddProduct { get; set; }
        public ProductManagementViewModel()
        {
            product = new List<Product>();
        }
    }
}
