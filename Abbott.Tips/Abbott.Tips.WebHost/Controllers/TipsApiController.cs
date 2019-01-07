using Abbott.Tips.ApiCore.Controllers;
using Abbott.Tips.ApiCore.Jwts;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abbott.Tips.WebHost.Controllers
{

    [EnableCors("Tips")]
    [TipsJwtAuthorize]
    public class TipsApiController : ApiCoreController
    {
    }
}
