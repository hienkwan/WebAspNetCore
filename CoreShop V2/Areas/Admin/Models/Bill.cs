using CoreShop_V2.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectASPDotnet.Areas.Admin.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetails>();
        }
        [Display(Name ="Mã hóa đơn ")]
        [Required]
        public string BillId { get; set; }
        [Display(Name ="Ngày lập đơn")]
        [Required]
        public DateTime CreateDate { get; set; }
        [Display(Name ="Tên khách hàng")]
        [Required(ErrorMessage = "Không được trống")]
        [RegularExpression(@"^.[^\d]{5,}$",ErrorMessage = "Họ tên phải trên 5 kí tự")]
        public string CustomerName { get; set; }
        [Display(Name ="Địa chỉ")]
        [Required(ErrorMessage = "Không được trống")]        
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Địa chỉ phải trên 5 kí tự")]
        public string CustomerAddress { get; set; }
        [Display(Name ="Số điện thoại")]
        [Required(ErrorMessage = "Không được trống")]
        [RegularExpression(@"^[0][1-9]{9}$", ErrorMessage = "Sai định dạng! Định dạng: 0395558787")]        
        public string CustomerPhone { get; set; }
        [Display(Name ="Địa chỉ Email")]
        [Required(ErrorMessage = "Không được trống")]
        [RegularExpression(@"^[a-z][^0][a-z0-9_\.\-]{3,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Sai định dạng! Định dạng: baoan11111@gmail.com")]
        public string CustomerEmail { get; set; }
        public bool Status { get; set; }
        public virtual ApplicationUser Account { get; set; }
        public ICollection<BillDetails> BillDetails { get; set; }
    }
}
