using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;

namespace ASP_NET_Core_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel modelUser)
        {
            if (!ModelState.IsValid)
            {
                return View(modelUser);
            }

            var user = new User
            {
                UserName = modelUser.UserName
            };

            var registrationResult = await _userManager.CreateAsync(user, modelUser.Password);
            if (registrationResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
           
            foreach (var identityError in registrationResult.Errors)
            {
                ModelState.AddModelError("", identityError.Description);
            }

            return View(modelUser);
        }


        public IActionResult Login(string returnUrl) => View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(
                loginModel.UserName,
                loginModel.Password,
                loginModel.RememberMe,
                false);

            if (loginResult.Succeeded)
            {
                if (Url.IsLocalUrl(loginModel.ReturnUrl))
                {
                    return Redirect(loginModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Указаны некорректные имя или пароль");
            return View(loginModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}