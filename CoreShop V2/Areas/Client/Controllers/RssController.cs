using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectASPDotnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoreShop_V2.Areas.Client.Controllers
{
    [Produces("application/xml")]
    public class RssController : Controller
    {
        private readonly MyDbContext _context;
        public RssController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            XNamespace ns = "http://www.w3.org/2005/Atom";
            var rss = new XElement("rss", new XAttribute("version", "2.0"), new XAttribute(XNamespace.Xmlns + "atom", ns));
            var channel = new XElement("channel",
               new XElement("title", "CoreShop"),
               new XElement("link", "https://www.kalapos.net"),
               new XElement("description", "Procduct of CoreShop"),
               new XElement("language", "en-us"),
               //todo: add blogChannel tags
               new XElement("copyright", $"Copyright 2019-{DateTime.UtcNow.Year} Hien Kwan"),
               new XElement("category", "Ecommerce Website"),
               new XElement(ns + "link", new XAttribute("href", "https://kalapos.net/Rss"), new XAttribute("rel", "self"), new XAttribute("type", "application/rss+xml"))
            );
            foreach (var item in _context.Product.Include(p => p.category))
            {
                var postInRss = new XElement("item");
                postInRss.Add(new XElement("title", item.ProductName));
                postInRss.Add(new XElement("description", item.Describe));
                postInRss.Add(new XElement("price", item.Price));
                postInRss.Add(new XElement("category", item.category.CategoryName));
                channel.Add(postInRss);
            }

            rss.Add(channel);

            return Ok(rss);
        }
    }
}