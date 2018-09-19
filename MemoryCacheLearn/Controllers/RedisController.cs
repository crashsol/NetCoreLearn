using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryCacheLearn.Cache;
using Microsoft.AspNetCore.Mvc;

namespace MemoryCacheLearn.Controllers
{
    public class RedisController : Controller
    {

        private readonly RedisHelper _redisHelper;


        public RedisController(RedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }
        public IActionResult Index()
        {
           
            var time =  _redisHelper.StringGet("Now");
            if (time == null)
            {
                _redisHelper.StringSet("Now", DateTime.Now.ToString());
            }

            return Json(new { CacheTime = time ,NowTime = DateTime.Now.ToString() });
        }
    }
}