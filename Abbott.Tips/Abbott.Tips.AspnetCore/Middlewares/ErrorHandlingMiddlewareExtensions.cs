using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Middlewares
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            //return builder.UseMiddleware<ErrorHandlingMiddleware>();
            return builder.UseMiddleware<UserFriendlyExceptionHandlerMiddleware>();
        }
    }
}
