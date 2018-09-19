using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using IdentityServerCenter.Models;
using IdentityServer4.Test;
using System;

namespace IdentityServerCenter.Controllers
{
    public class AccountController : Controller
    {
        #region 本地Cookies 认证配置
        //private UserManager<ApplicationUser> _userManager;
        //private SignInManager<ApplicationUser> _signInManager;

        //public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}
        #region 注册
        //public IActionResult Register(string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewData["ReturnUrl"] = returnUrl;
        //        var identityUser = new ApplicationUser
        //        {
        //            Email = registerViewModel.UserName,
        //            UserName = registerViewModel.UserName,
        //            NormalizedUserName = registerViewModel.UserName,
        //        };

        //        var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
        //        if (identityResult.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(identityUser, new AuthenticationProperties { IsPersistent = true });
        //            return RedirectToLoacl(returnUrl);
        //        }
        //        else
        //        {
        //            AddErrors(identityResult);
        //        }
        //    }

        //    return View();
        //}
        #endregion

        #region 登录 / 退出
        //public IActionResult Login(string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewData["ReturnUrl"] = returnUrl;
        //        var user = await _userManager.FindByEmailAsync(loginViewModel.UserName);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError(nameof(loginViewModel.UserName), "Email not exists");
        //        }
        //        else
        //        {
        //            await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = true });
        //            return RedirectToLoacl(returnUrl);
        //        }
        //    }

        //    return View();
        //}

        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");


        //}

        #endregion

        #endregion


        #region IdentityServer 内存 / 登录


        private readonly TestUserStore userStore;

        public AccountController(TestUserStore store)
        {
            userStore = store;
        }


        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var user = userStore.FindByUsername(loginViewModel.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginViewModel.UserName), "UserName not exists");
                }
                else
                {
                    //认证用户名密码
                    if (userStore.ValidateCredentials(loginViewModel.UserName, loginViewModel.Password))
                    {
                        var prop = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };

                        //使用 IdentityServer 的扩展方法 用户登录成功，记录Cookies
                        await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext,
                              user.SubjectId,
                              user.Username,
                              prop);


                        //如果用户不需要查看 Consent 页面，将会直接跳转回Client
                        //否则 跳转到本地授权页面，并带上returnUrl     Consent/index?returnUrl=xxxx
                        return RedirectToLoacl(returnUrl);

                    }
                    ModelState.AddModelError(nameof(loginViewModel.Password), "wrong password");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion


        #region Helper


        private IActionResult RedirectToLoacl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion


    }
}
