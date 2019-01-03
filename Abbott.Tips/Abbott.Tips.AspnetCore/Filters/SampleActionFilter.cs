using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.Filters
{
    /// <summary>
    /// 过滤器同时支持同步和异步两种不同的接口定义。您可以根据执行的任务类型，选择同步或异步实现。
    /// 同步过滤器定义OnStageExecuting和OnStageExecuted方法，会在管道特定阶段之前和之后运行代码的。
    /// </summary>
    public class SampleActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // do something before the action executes
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }

    /// <summary>
    /// 过滤器同时支持同步和异步两种不同的接口定义。您可以根据执行的任务类型，选择同步或异步实现。
    /// 异步过滤器定义了一个OnStageExecutionAsync方法。
    /// 该方法提供了FilterTypeExecutionDelegate的委托，当调用该委托时会执行具体管道阶段的工作。
    /// </summary>
    public class SampleAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // do something before the action executes
            await next();
            // do something after the action executes
        }
    }
}
