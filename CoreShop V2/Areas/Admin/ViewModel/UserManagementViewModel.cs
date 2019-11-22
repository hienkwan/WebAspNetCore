
using CoreShop_V2.Areas.Admin.Models;
using CoreShop_V2.Areas.Client.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Admin.ViewModel
{
    public class UserManagementViewModel
    {
        public CreateRoleViewModel createRoleViewModel { get; set; }
        public IEnumerable<IdentityRole> identityRole { get; set; }
        public IEnumerable<ApplicationUser> users { get; set; }
        public SignupViewModel signup { get; set; }
        //public IEnumerable<UserRoleViewModel> role { get; set; }
        public List<string> roleUser { get; set; }
        public EditInfoViewModel editInfoViewModel { get; set; }
        public UserManagementViewModel()
        {
            roleUser = new List<string>();
        }

    }
}
