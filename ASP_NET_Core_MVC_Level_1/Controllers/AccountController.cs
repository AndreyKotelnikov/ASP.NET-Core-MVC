using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core_MVC.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;

namespace ASP_NET_Core_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> IsNameFree(string userName)
        {
            if (await _userManager.FindByNameAsync(userName) is null)
            {
                return Json(true);
            }

            return Json("Имя пользователя уже занято!");
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
            _logger.LogInformation("Регистрация нового пользователя {0}", user.UserName);

            var registrationResult = await _userManager.CreateAsync(user, modelUser.Password);
            if (registrationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.User);
                _logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);
                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation("Пользователь {0} вошёл в систему", user.UserName);
                return RedirectToAction("Index", "Home");
            }
           
            foreach (var identityError in registrationResult.Errors)
            {
                ModelState.AddModelError("", identityError.Description);
            }
            _logger.LogInformation("Ошибка при регистрации пользователя {0}: {1}", user.UserName, 
                string.Join(", ", registrationResult.Errors.Select(err => err.Description)));

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
                _logger.LogInformation("Пользователь {0} вошёл в систему", loginModel.UserName);
                if (Url.IsLocalUrl(loginModel.ReturnUrl))
                {
                    return Redirect(loginModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Указаны некорректные имя или пароль");
            _logger.LogWarning("Ошибка при входе пользователя {0} в систему", loginModel.UserName);
            return View(loginModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Пользователь {0} вышел из системы", User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}