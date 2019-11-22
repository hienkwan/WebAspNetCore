using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProjectASPDotnet.Areas.Admin.Models
{
    public static class Tool
    {
        public static string ToUrlFriendly(this string title)
        {
            string str = title.ToLower().Trim();

            //thay thế tiếng Việt
            str = Regex.Replace(str, @"[áàảạãăắằẳẵặâấầẩẫậ]", "a");
            str = Regex.Replace(str, @"[éèẻẹẽêếềểễệ]", "e");
            str = Regex.Replace(str, @"[íìỉịĩ]", "i");
            str = Regex.Replace(str, @"[óòỏọõôốồổỗộơớờởợỡ]", "o");
            str = Regex.Replace(str, @"[úùủụũưứừửữự]", "u");
            str = Regex.Replace(str, @"[đ]", "d");

            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", "-").Trim();
            str = Regex.Replace(str, @"\s", "-");
            return str;
        }
    }
}
