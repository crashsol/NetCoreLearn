using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[Action]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]       
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public IActionResult GetIdentity()
        {
            return  new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

       
    }
}
