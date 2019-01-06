using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abbott.Tips.ApiCore.Controllers;
using Abbott.Tips.ApiCore.Jwts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Abbott.Tips.WebHost.Controllers
{
    [EnableCors("Tips")]
    public class OAuthController : JwtOauthController
    {
    }

    [EnableCors("Tips")]
    [TipsJwtAuthorize]
    public class TipsApiController : ApiCoreController
    {
    }
}