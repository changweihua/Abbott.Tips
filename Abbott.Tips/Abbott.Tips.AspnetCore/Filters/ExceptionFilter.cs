using Abbott.Tips.Framework.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is UserFriendlyException)
            {
                var ex = (UserFriendlyException)context.Exception;
                context.Result = new JsonResult(new { ex.Message });
            }
            return Task.CompletedTask;
        }
    }
}
