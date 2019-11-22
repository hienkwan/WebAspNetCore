using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Admin.ViewModel
{
    public class UserRoleViewModel 
    {        
        public string UserId { get; set; }
        public string UserRole { get; set; }
    }
}
