using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abbott.Tips.Application.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abbott.Tips.WebHost.Controllers
{
    public class UsersController : TipsApiController
    {
        public UserService iUserService { get; set; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tuple = await iUserService.GetPagedUsers(0, 10, string.Empty);
            return Ok(new { Code = 0, Items = tuple.Items });
        }
    }
}