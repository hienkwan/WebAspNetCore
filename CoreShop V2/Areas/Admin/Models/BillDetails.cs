using FinalProjectASPDotnet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectASPDotnet.Areas.Admin.Models
{
    public partial class BillDetails
    {
        [Display(Name ="ID chi tiết hóa đơn")]
        [Required]
        public string BillDetailsId { get; set; }
        public string BillId { get; set; }
        public string ProductId { get; set; }
        [Display(Name ="Số lượng")]
        [Required]
        public int Quantity { get; set; }
        [Display(Name ="Tổng tiền")]
        [Required]
        public double TotalPrice { get; set; }
        public Product Product { get; set; }
        public Bill Bill { get; set; }
    }
}
