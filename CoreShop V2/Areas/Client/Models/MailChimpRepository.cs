using System;
using MailChimp.Net;
using MailChimp.Net.Models;
using MailChimp.Net.Core;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using MailChimp.Net.Interfaces;
using System.Linq;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CoreShop_V2.Areas.Client.Models
{
    public class MailChimpRepository
    {
        private const string ApiKey = "7b6eca284d892915d0c10bd48b0a28cb-us5";
        private const string ListId = "206aea1391";
        
        private IMailChimpManager _mailChimpManager = new MailChimpManager(ApiKey);
        private const int TemplateId = 248261; // (your template id)
        public static string userEmail;
        private Setting _campaignSettings = new Setting
        {
            ReplyTo = userEmail,
            FromName = userEmail,
            Title = "Your campaign title",
            SubjectLine = "The email subject",
            PreviewText="abc",
            TemplateId= 248261
        };
        // `html` contains the content of your email using html notation
        public void CreateAndSendCampaign(string html)
        {
            var campaign = _mailChimpManager.Campaigns.AddAsync(new Campaign
            {
                Settings = _campaignSettings,
                Recipients = new Recipient { ListId = ListId },
                Type = CampaignType.Regular
            }).Result;
            var timeStr = DateTime.Now.ToString();
            //var content =  _mailChimpManager.Content.AddOrUpdateAsync(
            // campaign.Id,
            // new ContentRequest()
            // {
            //     Template = new ContentTemplate
            //     {
            //         Id = TemplateId,
            //         Sections = new Dictionary<string, object>()
            //    {
            //    { "body_content", html },
            //     //{ "preheader_leftcol_content", $"<p>{timeStr}</p>" }
            //    }
            //     }
            // }).Result;

            var member = new Member { EmailAddress = userEmail, EmailType = "html", StatusIfNew = Status.Subscribed };
            member.MergeFields.Add("FNAME", "HOLY");
            member.MergeFields.Add("LNAME", "COW");
            //var member = new Member
            //{
            //    ListId = "206aea1391",
            //    StatusIfNew = Status.Subscribed,
            //    MemberRating = 5,
            //    EmailAddress = userEmail,
            //    Status = Status.Pending,
            //    EmailType = "html",
            //    //IpSignup = Request.UserHostAddress,
            //    TimestampSignup = DateTime.UtcNow.ToString("s"),
            //    MergeFields = new Dictionary<string, object>
            //    {
            //        { "FNAME", "Hien" },
            //        { "LNAME", "Kwan" }
            //    }
            //};
            var result = _mailChimpManager.Members.AddOrUpdateAsync("206aea1391", member);
            //var result=  _mailChimpManager.Campaigns.TestAsync("206aea1391", new CampaignTestRequest
            //{
            //    Emails = new string[] { userEmail },
            //    EmailType = "html"
            //});
            _mailChimpManager.Campaigns.SendAsync(campaign.Id).Wait();
        }
        public List<Template> GetAllTemplates()
          => _mailChimpManager.Templates.GetAllAsync().Result.ToList();
        public List<List> GetAllMailingLists()
          => _mailChimpManager.Lists.GetAllAsync().Result.ToList();
        public Content GetTemplateDefaultContent(string templateId)
          => (Content)_mailChimpManager.Templates.GetDefaultContentAsync(templateId).Result;
    }
}
