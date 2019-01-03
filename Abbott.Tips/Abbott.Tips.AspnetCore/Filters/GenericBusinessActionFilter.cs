using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Filters
{
    /// <summary>
    /// 基于DbContext,IActionFilter过滤器实现全局UOW
    /// 统一在Action执行没有异常的情况下，提交数据库操作
    /// </summary>
    public class GenericBusinessActionFilter : IActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericBusinessActionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // do something before the action executes
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                //uow commit
                _unitOfWork.SaveChanges();
            }
        }
    }
}
