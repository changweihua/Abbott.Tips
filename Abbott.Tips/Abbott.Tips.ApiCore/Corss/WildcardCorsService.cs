using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abbott.Tips.ApiCore.Corss
{
    /// <summary>
    /// 支持在域名中使用通配符（*.cmono.net）
    /// </summary>
    public class WildcardCorsService : CorsService
    {
        public WildcardCorsService(IOptions<CorsOptions> options)
            : base(options)
        {
        }

        public override void EvaluateRequest(HttpContext context, CorsPolicy policy, CorsResult result)
        {
            var origin = context.Request.Headers[CorsConstants.Origin];
            EvaluateOriginForWildcard(policy.Origins, origin);
            base.EvaluateRequest(context, policy, result);
        }

        public override void EvaluatePreflightRequest(HttpContext context, CorsPolicy policy, CorsResult result)
        {
            var origin = context.Request.Headers[CorsConstants.Origin];
            EvaluateOriginForWildcard(policy.Origins, origin);
            base.EvaluatePreflightRequest(context, policy, result);
        }

        private void EvaluateOriginForWildcard(IList<string> origins, string origin)
        {
            //只在没有匹配的origin的情况下进行操作
            if (!origins.Contains(origin))
            {
                //查询所有以星号开头的origin
                var wildcardDomains = origins.Where(o => o.StartsWith("*"));
                if (wildcardDomains.Any())
                {
                    //遍历以星号开头的origin
                    foreach (var wildcardDomain in wildcardDomains)
                    {
                        //如果以.cnblogs.com结尾
                        if (origin.EndsWith(wildcardDomain.Substring(1))
                            //或者以//cmono.net结尾，针对http://cmono.net
                            || origin.EndsWith("//" + wildcardDomain.Substring(2)))
                        {
                            //将http://www.cmono.net添加至origins
                            origins.Add(origin);
                            break;
                        }
                    }
                }
            }
        }
    }
}
