﻿using Abbott.Tips.AspnetCore.Middlewares.Options;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Middlewares
{
    /// <summary>
    /// 扩展权限中间件
    /// </summary>
    public static class PermissionMiddlewareExtensions
    {
        /// <summary>
        /// 引入权限中间件
        /// </summary>
        /// <param name="builder">扩展类型</param>
        /// <param name="option">权限中间件配置选项</param>
        /// <returns></returns>
        public static IApplicationBuilder UsePermission(
              this IApplicationBuilder builder, PermissionMiddlewareOption option)
        {
            return builder.UseMiddleware<PermissionMiddleware>(option);
        }
    }
}
