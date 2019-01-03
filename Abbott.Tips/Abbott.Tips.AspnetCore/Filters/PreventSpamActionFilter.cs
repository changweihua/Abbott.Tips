using Abbott.Tips.AspnetCore.HttpContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Abbott.Tips.AspnetCore.Filters
{
    public class PreventSpamActionFilter : IActionFilter
    {

        private readonly IMemoryCache _cache;

        public PreventSpamActionFilter(IMemoryCache cache)
        {
            _cache = cache;
        }

        //处理请求之间的延迟
        public int DelayRequest = 10;
        //防止多次请求时的错误提示信息
        public string ErrorMessage = "Excessive Request Attempts Detected.";
        //出错时URL重定向
        public string RedirectURL;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if ((filterContext.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(PreventSpamAttribute), false).Any())
            {
                //存储 HttpContext 
                var request = filterContext.HttpContext.Request;

                //获取IP请求者的IP地址
                var originationInfo = filterContext.HttpContext.GetUserHostAddress();

                //Append the User Agent
                originationInfo += filterContext.HttpContext.UserAgent();

                //目标URL信息
                var targetInfo = request.GetEncodedUrl() + request.QueryString;

                //创建希哈值
                var hashValue = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(originationInfo + targetInfo)).Select(s => s.ToString("x2")));

                //如果希哈值在缓存中,(重复提交)
                if (_cache.Get(hashValue) != null)
                {
                    //添加错误信息
                    filterContext.ModelState.AddModelError("ExcessiveRequests", ErrorMessage);
                }
                else
                {
                    //使用希哈值的key添加一个空对象到缓存中(决定是否过期)
                    //if the Request is valid or not
                    _cache.Set(hashValue, hashValue, DateTime.Now.AddSeconds(DelayRequest));
                }
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

    public class PreventSpamAttribute : Attribute
    {

    }

}
