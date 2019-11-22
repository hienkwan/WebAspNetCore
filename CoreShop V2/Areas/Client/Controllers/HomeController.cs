using CoreShop_V2.Areas.Client.ViewModel;
using FinalProjectASPDotnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        public HomeController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? category)
        {
            HomeViewModel ViewModel = new HomeViewModel();

            ViewModel.HangHoa = await _context.Product.ToListAsync();

            var Context = _context.Product.Include(p => p.category).OrderBy(p => p.ProductId);
            var data = await _context.Product.ToListAsync();
            ViewModel.NewArrivalHangHoa = Context;
            //ViewModel.NewArrivalHangHoa = (from t in data
            //                               select t).Take(10);

            var topsell = await _context.TopSelling.ToListAsync();
            ViewModel.TopSellHangHoa = (from t in data
                                        from ts in topsell
                                        where t.ProductId == ts.ProductId
                                        orderby (ts.SellingQuantity) descending
                                        select t
                                      ).Take(10);
            return View(ViewModel);
        }

    }

}
