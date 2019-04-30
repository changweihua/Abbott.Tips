using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abbott.Tips.WebHost.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Abbott.Tips.WebHost.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class MessagesController : TipsApiController
    {
        private readonly IHubContext<TestHub> _testHub;

        public MessagesController(IHubContext<TestHub> testHub)
        {
            _testHub = testHub;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _testHub.Clients.All.SendAsync("someFunc", new { random = "abcd" });

            // 202: 请求已被接受并处理，但是还没有处理完成
            return Ok();
            return Accepted(1);
        }

    }
}