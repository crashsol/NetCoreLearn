using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MysqlLearn.Data;
using MysqlLearn.Models;

namespace MysqlLearn.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

     
        private readonly ProductContext _dbContext;

        public ValuesController(ProductContext productContext)
        {
            _dbContext = productContext;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var s1 = new Product { CreatedTime = DateTime.Now, Name = "111" };
            var s2= new Product { CreatedTime = DateTime.Now, Name = "111" };
            var s3 = new Product { CreatedTime = DateTime.Now, Name = "111" };
            var s4 = new Product { CreatedTime = DateTime.Now, Name = "111" };

            var ss = _dbContext.Add(s1).Entity;
            _dbContext.SaveChanges();
            return Ok();
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
