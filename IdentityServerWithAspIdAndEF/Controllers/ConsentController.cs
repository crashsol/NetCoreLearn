using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerWithAspIdAndEF.Services;
using Microsoft.AspNetCore.Mvc;
using IdentityServerWithAspIdAndEF.Models.ConsentViewModels;

namespace IdentityServerWithAspIdAndEF.Controllers
{
    /// <summary>
    /// 同意授权 
    /// </summary>
    public class ConsentController : Controller
    {

        private readonly ConsentService _consentService;

        public ConsentController(ConsentService consentService)
        {
            _consentService = consentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            ViewBag.Error = false;
            var vm = await _consentService.BuildConsentViewModelAsync(returnUrl,null);
            if(vm == null)
            {
                //回显错误页面
              
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {           
            var result = await _consentService.ProcessConsentAsync(model);
            if(result.IsRedirect)
            {
                return Redirect(result.RedirectUri);
            }
            //如果有错误，添加错误信息
            if(result.HasValidationError)
            {
                ViewBag.Error = true;
                ModelState.AddModelError("", result.ValidationError);
            }
            if (result.ShowView)
            {
                return View("Index", result.ViewModel);
            }
            return View("Error");
        }

    }
}