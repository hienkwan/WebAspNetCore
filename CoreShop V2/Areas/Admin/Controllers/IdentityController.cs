using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreShop_V2.Areas.Admin.Models;
using CoreShop_V2.Areas.Admin.ViewModel;
using FinalProjectASPDotnet.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoreShop_V2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public IdentityController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Login")]
        [Route("Signup")]
        public IActionResult Login()
        {
            IdentityViewModel identityViewModel = new IdentityViewModel();
            identityViewModel.invalid = false;
            return View(identityViewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> IsEmailInUse(IdentityViewModel identityViewModel)
        {            
            var user = await _userManager.FindByEmailAsync(identityViewModel.signup.CustomerEmail);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json("Email này đã được sử dụng");
            }
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(IdentityViewModel identityViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = identityViewModel.signup.CustomerEmail, Email = identityViewModel.signup.CustomerEmail, PhoneNumber = identityViewModel.signup.CustomerPhone, Address = identityViewModel.signup.CustomerAddress, Fullname = identityViewModel.signup.CustomerName };
                var result = await _userManager.CreateAsync(user, identityViewModel.signup.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new { Area = "Client" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("login", identityViewModel);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(IdentityViewModel identityViewModel)
        {            
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(identityViewModel.login.LoginEmail, identityViewModel.login.LoginPassword, identityViewModel.login.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { Area = "Client" });
                }

                ModelState.AddModelError("invalid", "Tài khoản hoặc mật khẩu không đúng");
                identityViewModel.invalid = true;

            }
            return View(identityViewModel);
        }       

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home", new { Area = "Client" });
        }
    }
}