using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreShop_V2.Areas.Admin.Models;
using CoreShop_V2.Areas.Admin.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreShop_V2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MgnUserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MgnUserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("UserManagement")]
        public async Task<IActionResult> UserManagement(UserManagementViewModel userManagementViewModel)
        {
            userManagementViewModel.identityRole = _roleManager.Roles;
            userManagementViewModel.users = _userManager.Users;
            
            foreach(var user in userManagementViewModel.users){
                var eachUser = await _userManager.FindByIdAsync(user.Id);
                var role = await _userManager.GetRolesAsync(eachUser);
                userManagementViewModel.roleUser.Add(role.FirstOrDefault());
            }

            return View(userManagementViewModel);
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(UserManagementViewModel userManagementViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = userManagementViewModel.createRoleViewModel.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            return View("UserManagement", userManagementViewModel);
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserManagementViewModel userManagementViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = userManagementViewModel.signup.CustomerEmail, Email = userManagementViewModel.signup.CustomerEmail, PhoneNumber = userManagementViewModel.signup.CustomerPhone, Address = userManagementViewModel.signup.CustomerAddress, Fullname = userManagementViewModel.signup.CustomerName };
                var result = await _userManager.CreateAsync(user, userManagementViewModel.signup.Password);

                if (result.Succeeded)
                {
                    return RedirectToActionPreserveMethod("AddUserRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }            

            return View("UserManagement", userManagementViewModel);
        }

        [Route("AddUserRole")]       
        public async Task<IActionResult> AddUserRole(UserManagementViewModel userManagementViewModel)
        {            
            var user = await _userManager.FindByEmailAsync(userManagementViewModel.signup.CustomerEmail);

            if(userManagementViewModel.signup.memberRole == null) userManagementViewModel.signup.memberRole = "user";
            IdentityResult result = await _userManager.AddToRoleAsync(user, userManagementViewModel.signup.memberRole);
            if (result.Succeeded)
            {
                return Json("Sucess");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("UserManagement", userManagementViewModel);
            
        }

        [Route("EditUser")]
        [HttpPost]
        public async Task<IActionResult> EditUser(UserManagementViewModel userManagementViewModel)
        {            
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userManagementViewModel.editInfoViewModel.Id);                
                var userRoleAdd = await _userManager.AddToRoleAsync(user, userManagementViewModel.editInfoViewModel.memberRole);
                if(userManagementViewModel.editInfoViewModel.Password != null)
                {
                    var removePassword = await _userManager.RemovePasswordAsync(user);
                    var addPassword = await _userManager.AddPasswordAsync(user, userManagementViewModel.editInfoViewModel.Password);
                    foreach (var error in addPassword.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }                

                user.Fullname = userManagementViewModel.editInfoViewModel.CustomerName;
                user.Email = userManagementViewModel.editInfoViewModel.CustomerEmail;
                user.Address = userManagementViewModel.editInfoViewModel.CustomerAddress;
                user.PhoneNumber = userManagementViewModel.editInfoViewModel.CustomerPhone;
                user.UserName = userManagementViewModel.editInfoViewModel.CustomerEmail;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json("Success");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                foreach (var error in userRoleAdd.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }                                
            }            

            return RedirectToAction("UserManagement", userManagementViewModel);
        }

        [Route("IsEmailInUse")]
        [HttpGet]
        public async Task<IActionResult> IsEmailInUse(UserManagementViewModel userManagementViewModel)
        {
            var user = await _userManager.FindByEmailAsync(userManagementViewModel.editInfoViewModel.CustomerEmail);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                if(user.Email != userManagementViewModel.editInfoViewModel.CustomerEmail)
                {
                    return Json("Email này đã được sử dụng");
                }
                return Json(true);
            }
        }

        [Route("DeleteUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var delete = await _userManager.DeleteAsync(user);
            if (delete.Succeeded)
            {
                return Json("Success");
            }
            return RedirectToAction("UserManagement");
        }
    }
}