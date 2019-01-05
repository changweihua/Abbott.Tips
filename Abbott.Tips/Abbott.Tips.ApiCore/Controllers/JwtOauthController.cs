using Abbott.Tips.ApiCore.Jwts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.ApiCore.Controllers
{
    public class JwtOauthController : ApiCoreController
    {
        public JwtTokenGenerator JwtTokenGenerator { get; set; }

        /// <summary>
        /// 演示性登录 API，返回新的 token 并设置 Cookie
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromForm]string user)
        {
            if (!string.IsNullOrEmpty(user))
            {
                // 假的用户信息
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user, ClaimValueTypes.String)
                };
                var token = JwtTokenGenerator.GenerateToken(user, claims, DateTime.Now.AddDays(1));
                await HttpContext.SignInAsync(user, claims, DateTime.Now.AddDays(1));

                return Ok(new { token = token });
            }

            return BadRequest();
        }
    }
}
