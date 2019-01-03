using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Middlewares.Options
{
    /// <summary>
    /// 权限中间件选项
    /// </summary>
    public class PermissionMiddlewareOption
    {
        /// <summary>
        /// 登录action
        /// </summary>
        public string LoginAction
        { get; set; }

        /// <summary>
        /// 无权限导航action
        /// </summary>
        public string NoPermissionAction
        { get; set; }

        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<UserPermission> UserPerssions
        { get; set; } = new List<UserPermission>();

        /// <summary>
        /// 组权限集合
        /// </summary>
        public IList<GroupAuthority> GroupAuthorities { get; set; } = new List<GroupAuthority>();
    }
}
