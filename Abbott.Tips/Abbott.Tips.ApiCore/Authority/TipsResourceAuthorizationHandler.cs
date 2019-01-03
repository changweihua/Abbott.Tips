using Abbott.Tips.AspnetCore.Authority;
using Abbott.Tips.Framework.Audition;
using Abbott.Tips.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Herbalife_HGDX.MVC.Authority
{
    public class TipsResourceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IResource resource)
        {
            // 如果是Admin角色就直接授权成功
            if (context.User.IsInRole("admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
                // 允许任何人创建或读取资源
                if (requirement == Operations.Create || requirement == Operations.Read)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    // 只有资源的创建者才可以修改和删除
                    if (context.User.Identity.Name == resource.Creator)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
