using FinalProjectASPDotnet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectASPDotnet.Areas.Admin.Models
{
    public partial class Category
    {
        public Category()
        {
            product = new HashSet<Product>();
        }
        [Display(Name ="ID loại")]
        [Key]
        public string CateID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> product { get; set; }
    }
}
