using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Admin.ViewModel
{
    public class IdentityViewModel
    {
        public LoginViewModel login { get; set; }
        public SignupViewModel signup { get; set; }
        public bool invalid { get; set; }

        public IdentityViewModel()
        {
            invalid = false;
        }
    }
}
