using FinalProjectASPDotnet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectASPDotnet.Areas.Admin.Models
{
    public partial class Product
    {
        public Product()
        {
            BillDetail = new HashSet<BillDetails>();
            TopSelling = new HashSet<TopSelling>();

        }
        [Display(Name = "Mã Hàng Hóa")]
        [Required]
        public string  ProductId { get; set; }
        [Display(Name = "Tên hàng hóa")]
        [Required]
        public string ProductName { get; set; }
        public string CateId  { get; set; }
        [Display(Name = "Giá")]
        [Required]
        public double Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]// fix
        [Required]
        public double DiscountPrice { get; set; } // fix-end
        [Display(Name = "Hình")]
        [Required] 
        public string Image { get; set; }
        [Display(Name = "Mô tả")]
        [Required]
        public string Describe { get; set; }
        public Category category { get; set; }
        public ICollection<BillDetails> BillDetail { get; set; }
        public ICollection<TopSelling> TopSelling { get; set; }
        public string ProductNameSeo => ProductName.ToUrlFriendly();
        public string CategoryNameSeo => category.CategoryName.ToUrlFriendly();

    }
}
