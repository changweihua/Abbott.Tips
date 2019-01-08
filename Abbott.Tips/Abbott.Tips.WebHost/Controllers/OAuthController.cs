using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abbott.Tips.ApiCore.Controllers;
using Abbott.Tips.ApiCore.Events;
using Abbott.Tips.ApiCore.Jwts;
using Abbott.Tips.Application.Users;
using Abbott.Tips.Framework.EventBus;
using Abbott.Tips.Framework.EventBus.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abbott.Tips.WebHost.Controllers
{
    [EnableCors("Tips")]
    public class OAuthController : JwtOauthController
    {
        public UserService iUserService { get; set; }

        public IEventBus EventBus { get; set; }

        public OAuthController(IEventBus eventBus)
        {
            EventBus = eventBus;
        }

        /// <summary>
        /// 返回新的 token 并设置 Cookie
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public override async Task<IActionResult> Post([FromBody]AccountModel account)
        {
            if (account != null && !string.IsNullOrEmpty(account.UserName))
            {
                var estUser = iUserService.Login(account.UserName, account.Password);

                if (estUser != null)
                {
                    // 假的用户信息
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, estUser.LoginName, ClaimValueTypes.String)
                    };
                    var token = JwtTokenGenerator.GenerateToken(estUser.LoginName, claims, DateTime.Now.AddDays(1));
                    await HttpContext.SignInAsync(estUser.LoginName, claims, DateTime.Now.AddDays(1));
                    await this.EventBus.PublishAsync(new UserLoginedEvent(estUser.LoginName));
                    return Ok(new { Code = 0, Token = token, User = estUser });
                }
            }

            return BadRequest();
        }
    }

}