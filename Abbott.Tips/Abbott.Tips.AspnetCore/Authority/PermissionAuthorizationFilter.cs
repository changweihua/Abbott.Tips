using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.Authority
{
    /// <summary>
    /// 受一个name参数，代表权限的名称，然后将权限名称转化为PermissionAuthorizationRequirement，
    /// 最后直接调用 authorizationService 来完成授权。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class PermissionAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        public PermissionAuthorizationFilter(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, new PermissionAuthorizationRequirement(Name));
            if (!authorizationResult.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
