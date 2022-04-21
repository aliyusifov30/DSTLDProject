using DSLTD.Areas.Manage.ViewModels;
using DSLTD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSLTD.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {

        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , RoleManager<IdentityRole> roleManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel loginVM)
        {

            if (!ModelState.IsValid) return View();

            AppUser admin = await _userManager.FindByNameAsync(loginVM.UserName);

            if(admin == null)
            {
                ModelState.AddModelError("", "UserName or Password is not corrent");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, loginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not corrent");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
