using CoreShop_V2.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectASPDotnet.Areas.Admin.Models
{
    public class Rating
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        [Display(Name ="Bình luận")]
        [MaxLength]
        public string Comment { get; set; }
        public int Ratings { get; set; }
        public virtual ApplicationUser Account { get; set; }
    }
}
