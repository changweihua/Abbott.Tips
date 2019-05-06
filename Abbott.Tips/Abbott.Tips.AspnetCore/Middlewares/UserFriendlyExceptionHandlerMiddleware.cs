using Abbott.Tips.Framework.Exceptions;
using Abbott.Tips.Framework.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.Middlewares
{
    public class UserFriendlyExceptionHandlerMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public UserFriendlyExceptionHandlerMiddleware(RequestDelegate rd)
        {
            requestDelegate = rd;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (UserFriendlyException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null)
            {
                return;
            }

            await WriteExceptionAsync(context, e).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json;charset=utf-8";

            var errorMessage = e.Message;
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                //空值处理
                setting.NullValueHandling = NullValueHandling.Ignore;
                setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //高级用法九中的Bool类型转换 设置
                //setting.Converters.Add(new BoolConvert("是,否"));

                return setting;
            });
            var result = JsonConvert.SerializeObject(new ApiResultModel() { Code = (int)ResultCode.ERROR, Result = errorMessage }, setting);

            await context.Response.WriteAsync(result).ConfigureAwait(false);
        }
    }
}
