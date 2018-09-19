using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using IdentityServerCenter.Models;
using IdentityServerCenter.Services;

namespace IdentityServerCenter.Controllers
{

    public class ConsentController : Controller
    {

        private readonly ConsentService _consent;       //用户同意页面
      

        public ConsentController(ConsentService consent)
        {
            _consent = consent;
        }

        [HttpGet]
        public async Task<IActionResult> index(string returnUrl)
        {
            var vm = await _consent.BuildViewModelAsync(returnUrl);
            if (vm != null)
            {
                return View(vm);
            }
            return View("Error");         
        }



        /// <summary>
        /// Handles the consent screen postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            var result = await _consent.ProcessConsent(model);

            ///授权成功后跳转
            if (result.IsRedirect)
            {
                return Redirect(result.RedirectUri);
            }
            //返回错误内容
            if (result.HasValidationError)
            {
                ModelState.AddModelError("", result.ValidationError);
            }
            //需要重新授权
            if (result.ShowView)
            {
                return View("Index", result.ViewModel);
            }
            return View("Error");
        }
    }
}
