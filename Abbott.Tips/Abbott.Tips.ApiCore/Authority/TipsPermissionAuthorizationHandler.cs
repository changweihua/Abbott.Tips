using Abbott.Tips.AspnetCore.Authority;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Herbalife_HGDX.MVC.Authority
{
    public class TipsPermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected Func<int, string, bool> CheckPermission = null;

        protected Func<int, string, bool> CheckGroupPermission = null;

        public TipsPermissionAuthorizationHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                if (CheckPermission == null || context.User.IsInRole("admin"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //获取GroupSid的Claim信息
                    var groupSidClaim = context.User.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.GroupSid);
                    var userIdClaim = context.User.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);

                    if (groupSidClaim != null)
                    {
                        if (CheckGroupPermission(int.Parse(groupSidClaim.Value), requirement.Name))
                        {
                            context.Succeed(requirement);
                        }
                    }
                    if (userIdClaim != null)
                    {
                        if (CheckPermission(int.Parse(userIdClaim.Value), requirement.Name))
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }

}
