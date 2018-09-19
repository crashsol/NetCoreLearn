using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthorizeLearn.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizeLearn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "Permission")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Policy = "Contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize(Policy = "AtLeast18")]
        public IActionResult AgeOver18()
        {
            return Ok("年龄大于18岁");
        }

        [Authorize(Policy = "AtLeast20")]
        public IActionResult AgeOver20()
        {
            return Ok("年龄大于20");
        }

      
    }
}
