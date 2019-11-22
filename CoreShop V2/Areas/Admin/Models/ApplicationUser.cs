using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Admin.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
    }
}
