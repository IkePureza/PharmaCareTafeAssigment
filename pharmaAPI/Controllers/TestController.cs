using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace pharmaAPI.Controllers
{
    public class Message
    {
        public string name;

        public Message(string cname) {
            this.name = cname;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        // GET api/test
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/test/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            // Look at me !!!
            return "value" + " | " + id.ToString();
        }

        // // GET api/test/5
        // [HttpGet("name/{value}")]
        // public ActionResult<string> Get(string value)
        // {
        //     // Look at me !!!
        //     return "value" + " | " + value.ToString();
        // }

        // GET api/test/5
        [HttpGet("name/")]
        public ActionResult<string> Get(string value)
        {
            Message message = new Message("reginald");

            return JsonConvert.SerializeObject(message);
        }

        // POST api/test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/test/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
