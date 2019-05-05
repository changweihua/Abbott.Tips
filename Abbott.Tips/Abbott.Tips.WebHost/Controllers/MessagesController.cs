using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abbott.Tips.ApiCore.Controllers;
using Abbott.Tips.WebHost.Hubs;
using Abbott.Tips.WebHost.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Abbott.Tips.WebHost.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [EnableCors("SignalrCore")]
    public class MessagesController : ApiCoreController
    {
        private readonly IHubContext<TestHub> _testHub;
        private readonly IHubContext<SignalrHubs> _signalrHubs;

        public MessagesController(IHubContext<TestHub> testHub, IHubContext<SignalrHubs> signalrHubs)
        {
            _testHub = testHub;
            _signalrHubs = signalrHubs;
        }
        #region MyRegion

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _testHub.Clients.All.SendAsync("someFunc", new { random = "尚未签到" });

            // 202: 请求已被接受并处理，但是还没有处理完成
            //return Ok();
            return Accepted(1);
        }

        #endregion

        #region MyRegion

        /// <summary>
        /// 单个connectionid推送
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        [HttpPost, Route("AnyOne")]
        public IActionResult AnyOne([FromBody]IEnumerable<SignalrGroups> groups)
        {
            if (groups != null && groups.Any())
            {
                var ids = groups.Select(c => c.ShopId);
                var list = SignalrGroups.UserGroups.Where(c => ids.Contains(c.ShopId));
                foreach (var item in list)
                    _signalrHubs.Clients.Client(item.ConnectionId).SendAsync("AnyOne", $"{item.ConnectionId}: {item.Content}");
            }
            return Ok();
        }

        /// <summary>
        /// 全部推送
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet, Route("EveryOne")]
        public IActionResult EveryOne(string message)
        {
            _signalrHubs.Clients.All.SendAsync("EveryOne", $"{message}");
            return Ok();
        }

        /// <summary>
        /// 组推送
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost, Route("AnyGroups")]
        public IActionResult AnyGroups([FromBody]SignalrGroups group)
        {
            if (group != null)
            {
                _signalrHubs.Clients.Group(group.GroupName).SendAsync("AnyGroups", $"{group.Content}");
            }
            return Ok();
        }

        /// <summary>
        /// 多参数接收方式
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet, Route("MoreParamsRequest")]
        public IActionResult MoreParamsRequest(string message)
        {
            _signalrHubs.Clients.All.SendAsync("MoreParamsRequest", message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
            return Ok();
        }

        #endregion

    }
}