using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.CAP;

namespace CAPDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        //发布者
        private readonly ICapPublisher _capPublisher;

        private readonly ILogger<ValuesController> _logger;
        public ValuesController(ApplicationDbContext applicationDbContext, ILogger<ValuesController> logger,ICapPublisher capPublisher)
        {
            _dbContext = applicationDbContext;
            _logger = logger;
            _capPublisher = capPublisher;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //模拟发送事件
            using (var transAction = _dbContext.Database.BeginTransaction())
            {
                //进行内容的发布 使用 routeKey = "capdemo.values.getmethodevent",事件订阅者根据routekey进行订阅
                await _capPublisher.PublishAsync("capdemo.values.getmethodevent", new { Id = Guid.NewGuid(),Time = DateTime.Now, Message = "Hello World!" });

                transAction.Commit();
            }

            return Ok("发送成功"+ DateTime.Now.ToString());
        }

        ///// <summary>
        ///// 定义消息消费者
        ///// </summary>
        ///// <param name="model"></param>
        //[NonAction]
        //[CapSubscribe("capdemo.values.getmethodevent")]
        //public void ReceiveMessage(dynamic model)
        //{
        //    Console.WriteLine($"[capdemo.values.getmethodevent] message received: Id:{model.Id}  Time:{model.Time}  Message:{model.Message} ");
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
