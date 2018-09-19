using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ConfigurationOption.Options;

namespace ConfigurationOption.Controllers
{
    public class HomeController : Controller
    {

        private Class myClass;

        public HomeController(IOptions<Class> _class)
        {
            myClass = _class.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}