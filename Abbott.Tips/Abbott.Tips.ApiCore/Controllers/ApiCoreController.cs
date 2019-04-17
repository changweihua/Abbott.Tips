using Abbott.Tips.ApiCore.Jwts;
using Abbott.Tips.Application.Localization;
using Abbott.Tips.Core.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCoreController : ControllerBase
    {
        public IObjectMapper ObjectMapper { get; set; }

        //public ApiCoreController(IStringLocalizerFactory factory)
        //{
        //    _sharedLocalizer = factory.Create(typeof(SharedResource));
        //    //_localizer2 = factory.Create("SharedResource", location: null);
        //}
        ////private readonly IHtmlLocalizer<BookController> _localizer;
        ////private readonly IStringLocalizer<InfoController> _localizer;
        //private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
    }
}
