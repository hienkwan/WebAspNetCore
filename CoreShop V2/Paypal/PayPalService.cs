﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Paypal
{
    public class PayPalService
    {
        public static PayPalConfig GetPayPalConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return new PayPalConfig()
            {
                AuthToken = configuration["PayPal:AuthToken"],
                Business = configuration["PayPal:Business"],
                PostUrl = configuration["PayPal:PostUrl"],
                ReturnUrl = configuration["PayPal:ReturnUrl"]
            };
        }
    }
}
