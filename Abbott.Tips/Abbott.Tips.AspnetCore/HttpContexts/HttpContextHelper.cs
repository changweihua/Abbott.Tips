using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abbott.Tips.AspnetCore.HttpContexts
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpContextHelper
    {
        /// <summary>
        /// 在ASP.NET中，使用负载均衡时，可以通过ServerVariables获取客户端的IP地址。
        /// 但在ASP.NET Core中没有ServerVariables的对应实现，需要换一种方式，可以在HttpContext.Request.Headers中获取，
        /// 需要注意的是key与ServerVariables方式不一样，ServerVariables中是"HTTP_X_FORWARDED_FOR"，
        /// HttpContext.Request.Headers中是"X-Forwarded-For"，
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserHostAddress(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 获取浏览器UserAgent
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string UserAgent(this HttpContext context)
        {
            if (context == null)
            {
                return string.Empty;
            }
            return context.Request.Headers[HeaderNames.UserAgent].ToString();
        }

        public static Uri GetUri(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.Scheme))
                throw new ArgumentException("Http request Scheme is not specified");
            if (!request.Host.HasValue)
                throw new ArgumentException("Http request Host is not specified");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(request.Scheme).Append("://").Append((object)request.Host);
            if (request.Path.HasValue)
                stringBuilder.Append(request.Path.Value);
            if (request.QueryString.HasValue)
                stringBuilder.Append((object)request.QueryString);
            return new Uri(stringBuilder.ToString());
        }
    }
}
