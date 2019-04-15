using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abbott.Tips.ApiCore.Controllers;
using Abbott.Tips.Application.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Abbott.Tips.WebHost.Controllers
{
    public class ValuesController : TipsApiController
    {
        public IConfigurationService iConfigurationService { get; set; }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cfgs = await iConfigurationService.GetConfigurationList("");
            return Ok(new { Code = 0, Items = cfgs });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
