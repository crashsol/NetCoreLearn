using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LifeCycleLearn.Services;
namespace LifeCycleLearn.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {


        public readonly ITestService service1;
        public readonly ITestService2 service2;
        public readonly ITestService3 service3;

        public ValuesController(ITestService testServic,ITestService2 testService2, ITestService3 testService3)
        {
            service1 = testServic;
            service2 = testService2;
            service3 = testService3;
        }
        
        // GET api/values
        [HttpGet]
        public IActionResult  Get([FromServices]ITestService testService,[FromServices]ITestService2 testService2)
        {

            var msg = $"Controller Transient: {service1.Id} \r\n" +
                      $"    Action Transient: {testService.Id}\r\n" +
                      $"Controller    Scoped:{ service2.Id} \r\n" +
                      $"Action        Scoped:{ testService2.Id} \r\n" +
                      $" Singleton:{service3.Id}";

            return Ok(msg);
        }

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
