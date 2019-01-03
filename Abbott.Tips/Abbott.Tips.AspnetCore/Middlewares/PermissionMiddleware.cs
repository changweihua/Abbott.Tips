using Abbott.Tips.AspnetCore.Middlewares.Options;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.Middlewares
{
    /// <summary>
    /// 权限中间件
    /// 用户权限按照组(角色)进行分类，不具体到个人
    /// </summary>
    public class PermissionMiddleware
    {
        /// <summary>
        /// 管道代理对象
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 权限中间件的配置选项
        /// </summary>
        private readonly PermissionMiddlewareOption _option;

        /// <summary>
        /// 组(角色)权限关系集合
        /// </summary>
        internal static List<UserPermission> _userPermissions;

        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next">管道代理对象</param>
        /// <param name="permissionResitory">权限仓储对象</param>
        /// <param name="option">权限中间件配置选项</param>
        public PermissionMiddleware(RequestDelegate next, PermissionMiddlewareOption option)
        {
            _option = option;
            _next = next;
            _userPermissions = option.UserPerssions;
        }

        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            //请求Url
            var questUrl = context.Request.Path.Value.ToLower();

            //是否经过验证
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;



                //if (_userPermissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                //{
                //    //用户名
                //    var userName = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                //    if (_userPermissions.Where(w => w.UserName == userName && w.Url.ToLower() == questUrl).Count() > 0)
                //    {
                //        return this._next(context);
                //    }
                //    else
                //    {
                //        //无权限跳转到拒绝页面
                //        context.Response.Redirect(_option.NoPermissionAction);
                //    }
                //}
            }
            return this._next(context);
        }
    }

    /// <summary>
    /// 用户权限
    /// </summary>
    public class UserPermission
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Url
        { get; set; }
    }

    /// <summary>
    /// 组权限设置
    /// </summary>
    public class GroupAuthority
    {
        public int GroupID { get; set; }
        public string Url { get; set; }

        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }

}
