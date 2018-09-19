using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerClientMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace IdentityServerClientMvc.Controllers
{
    public class HomeController : Controller
    {

        public readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/Values/get");
            return Ok(content);
        }

        [Authorize]
        public async Task<IActionResult> CallApiIdentity()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/values/GetIdentity");
            return Ok(content);
        }
            


        [Authorize]
        public IActionResult Login()
        {
            _logger.LogInformation("登录成功");
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
           await HttpContext.SignOutAsync("Cookies"); //退出本地登录
           await HttpContext.SignOutAsync("oidc");    //退出远程登录
           return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {
            var refresh_token = await HttpContext.GetTokenAsync("refresh_token");

            List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
            paramList.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            paramList.Add(new KeyValuePair<string, string> ("refresh_token",refresh_token));
            var client = new HttpClient();            
            var response = client.PostAsync(new Uri("http://localhost:5000/token "), new FormUrlEncodedContent(paramList)).Result; 
            var result = response.Content.ReadAsStringAsync().Result;

            return Ok(result);


        }


    }
}
