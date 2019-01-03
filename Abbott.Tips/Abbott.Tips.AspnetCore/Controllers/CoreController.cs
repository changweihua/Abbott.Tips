using Abbott.Tips.AspnetCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Controllers
{
    /// <summary>
    /// Controller 基类
    /// </summary>
    [AuthorHeaderResultFilter("Author", "Lance Chang Softtek")]
    public class AspnetCoreController : Controller
    {
        public ILogger logger { get; set; }
        public ILoggerFactory loggerFactory { get; set; }
        public IAuthorizationService iIAuthorizationService { get; set; }
    }
}
