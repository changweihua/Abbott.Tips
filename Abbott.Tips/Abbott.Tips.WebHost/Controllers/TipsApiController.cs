using Abbott.Tips.ApiCore.Controllers;
using Abbott.Tips.ApiCore.Jwts;
using Abbott.Tips.Application.BCL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abbott.Tips.WebHost.Controllers
{
    /// <summary>
    /// Tips 基础 API 控制器，包含基本的 CRUD 方法，对应 HTTP METHOD
    /// </summary>
    [EnableCors("Tips")]
    //[TipsJwtAuthorize]
    public class TipsApiController : ApiCoreController
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var cfgs = await iConfigurationService.GetConfigurationList("");
        //    return Ok(new { Code = 0, Items = cfgs });
        //}
    }
}
