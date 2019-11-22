using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using MailChimp;
using CoreShop_V2.Areas.Client.Models;
using System.IO;
using MailChimp.Net.Models;
using MailChimp.Net;
using MailChimp.Net.Interfaces;
using BraintreeHttp;

namespace CoreShop_V2.Areas.Client.Controllers
{
    public class MailChimpController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [Route("/AddSubcribeUser")]
        public async Task<IActionResult> AddSubcribeUser(string userEmail)
        {
            //using (var http = new HttpClient())
            //{
            //    http.DefaultRequestHeaders.Authorization =
            //         new AuthenticationHeaderValue("Basic", "7b6eca284d892915d0c10bd48b0a28cb-us5");
            //    MailChimpManager mc = new MailChimpManager("7b6eca284d892915d0c10bd48b0a28cb-us5");
            //    EmailParameter email = new EmailParameter()
            //    {
            //        Email = userEmail
            //    };
            //    EmailParameter results = mc.Subscribe("206aea1391", email);

            //}
            //string html="";
            //MailChimpRepository rs = new MailChimpRepository();
            //using (StreamReader SourceReader = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\TextFile.txt", "")))
            //{
            //    html = SourceReader.ReadToEnd();
            //}
            //MailChimpRepository.userEmail = userEmail;
            //rs.CreateAndSendCampaign(html);
            IMailChimpManager _mailChimpManager = new MailChimpManager("7b6eca284d892915d0c10bd48b0a28cb-us5");
            var listId = "206aea1391";
            // Use the Status property if updating an existing member
            var member = new Member { EmailAddress = userEmail, StatusIfNew = Status.Subscribed };
            await _mailChimpManager.Members.AddOrUpdateAsync(listId, member);

            return View();
        }

        [HttpPost]
        [Route("/CreateSubscriber")]
        public IActionResult CreateSubscriber( string value)
        {

            //return RedirectPermanentPreserveMethod("https://facebook.us5.list-manage.com/subscribe/post?u=d165a90e8028fd239e0349abc&amp;id=206aea1391");
            return Json(value);
        }
    }
}