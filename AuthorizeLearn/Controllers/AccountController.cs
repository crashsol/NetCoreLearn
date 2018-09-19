using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using AuthorizeLearn.Models;
using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace AuthorizeLearn.Controllers
{
    public class AccountController : Controller
    {
        private readonly JwtSetting _jwtSetting;

        public AccountController(IOptions<JwtSetting> options)
        {
            _jwtSetting = options.Value;
        }

        #region Cookies 登录

        public async Task<IActionResult> Login(string returnUrl)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,"渣渣辉"),
                new Claim("Depart","信息中心"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Permission","User_List"),
                new Claim(ClaimTypes.DateOfBirth,"2000-01-01")            

            };
            var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); //链式identity
            var principal = new ClaimsPrincipal(ClaimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if(string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("index", "home");
            }
            return RedirectToLocal(returnUrl);
            
        }

      


        public async Task<IActionResult> LoginDocument()
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,"Document"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Permission","User_List"),
                new Claim(ClaimTypes.DateOfBirth,"2000-01-01")

            };
            var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); //链式identity
            var principal = new ClaimsPrincipal(ClaimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok("Document账户登录成功");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("退出登录");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        //#region Jwt 登录

        //[HttpPost]
        //public IActionResult Login([FromBody]LoginViewModel model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        if(!(model.Name=="123" && model.Password=="123"))
        //        {
        //            return BadRequest("用户名密码错误");
        //        }
        //        var claims = new Claim[] {
        //            new Claim(ClaimTypes.Name,model.Name),
        //            new Claim(ClaimTypes.Role,"Admin")
        //        };
        //        //获得 加密后的key
        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));

        //        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);     //使用加密后的key 创建 登录证书

        //        //生产加密token
        //        var token = new JwtSecurityToken(
        //            _jwtSetting.Issuer,
        //            _jwtSetting.Audience,
        //            claims,
        //            DateTime.Now,
        //            DateTime.Now.AddMinutes(30),
        //            cred
        //            );

        //        //返回token
        //        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

        //    }
        //    return Ok("请输入用户名，密码");
        //}

        //public IActionResult Logout()
        //{
        //    return Ok();
        //}
        //#endregion



        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}