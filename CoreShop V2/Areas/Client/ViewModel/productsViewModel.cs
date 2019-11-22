using CoreShop_V2;
using FinalProjectASPDotnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop.ViewModels
{
    public class ProductsViewModel
    {        
        public PaginatedList<Product> items { get; set; }
        public int PageFrom { get { return items.PageIndex * 10 - 9; } }
        public int PageTo
        {
            get
            {
                int count = 0;
                foreach (Product item in items)
                {
                    count++;
                }
                return PageFrom + count - 1;
            }
        }
        public IQueryable<Product> TotalItem { get; set; }
        public List<SelectListItem> Options { set; get; }
        public string sortOrder { get; set; }
        public string searchString { get; set; }
    }
}
