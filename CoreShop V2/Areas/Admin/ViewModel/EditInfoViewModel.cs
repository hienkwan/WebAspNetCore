using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreShop_V2.Areas.Client.ViewModel
{
    public class EditInfoViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Không được trống")]
        [RegularExpression(@"^.[^\d]{5,}$", ErrorMessage = "Họ tên phải trên 5 ký tự")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Không được trống")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Địa chỉ phải trên 5 ký tự")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Không được trống")]
        [RegularExpression(@"^[0][1-9]{9}$", ErrorMessage = "Sai định dạng! Định dạng: 0395558787")]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Không được trống")]
        [RegularExpression(@"^[a-z][^0][a-z0-9_\.\-]{3,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$", ErrorMessage = "Sai định dạng! Định dạng: baoan11111@gmail.com")]
        [Remote(action: "IsEmailInUse", controller: "MgnUser", areaName: "admin")]
        public string CustomerEmail { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Không khớp mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^(user)|(admin)$",ErrorMessage ="Chỉ user hoặc admin")]
        public string memberRole { get; set; }
    }
}
