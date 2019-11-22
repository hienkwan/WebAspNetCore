using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace CoreShop_V2.Areas.Admin.Controllers
{
    public class SendMailController : Controller
    {
        [Route("sendmail")]
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                ) {
                    return true;
                };
        }
        public IActionResult Index()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hello", "hienkwan@gmail.com"));
            message.To.Add(new MailboxAddress("Send mail asp net core", "hienkwan@gmail.com"));
            message.Subject = "Learn to send mail using asp net core";
            //message.Body = new TextPart("plain")
            //{
            //    Text = "I am using mailkit to send email with asp net core 2.2" +
            //    "<h1>HEllO<h1>"

            //};
            BodyBuilder bodyBuilder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\TextFile.txt", "")))
            {
                bodyBuilder.HtmlBody = SourceReader.ReadToEnd();
            }
            message.Body = bodyBuilder.ToMessageBody();
            
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //client.Connect("MAIL_SERVER", 465, SecureSocketOptions.SslOnConnect);
                client.Connect("smtp.gmail.com", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("hienkwan@gmail.com", "09041998123");
                client.Send(message);
                client.Disconnect(true);
            }

            return View();
        }
    }
}