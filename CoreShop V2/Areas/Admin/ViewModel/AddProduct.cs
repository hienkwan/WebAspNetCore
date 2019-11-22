using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Admin.ViewModel
{
    public class AddProduct
    {    
        [Required]
        public string ProductName { get; set; }
        public int CateId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double DiscountPrice { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Describe { get; set; }
        public string category { get; set; }
    }
}
