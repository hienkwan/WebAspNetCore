using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreShop_V2.Areas.Admin.ViewModel;
using FinalProjectASPDotnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreShop_V2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MgnProductController : Controller
    {
        private readonly MyDbContext _dbContext;
        public MgnProductController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Route("ProductManagement")]
        public async Task<IActionResult> ProductManagement(ProductManagementViewModel productMngViewModel)
        {
            productMngViewModel.product = await _dbContext.Product.ToListAsync();
            return View(productMngViewModel);
        }
    }
}