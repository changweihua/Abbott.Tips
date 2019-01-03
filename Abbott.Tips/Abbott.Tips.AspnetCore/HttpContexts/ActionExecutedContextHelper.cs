using Abbott.Tips.Framework.FCL;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.AspnetCore.HttpContexts
{
    public static class ActionExecutedContextHelper
    {
        public static Dictionary<string, string> GetParameterDictionary(this ActionExecutedContext filterContext)
        {
            //请求类各个字段的值
            Dictionary<string, string> parmsObj = new Dictionary<string, string>();

            try
            {
                var controllActionDesc = filterContext.ActionDescriptor as ControllerActionDescriptor;

                if (controllActionDesc != null)
                {
                    foreach (var item in controllActionDesc.MethodInfo.GetParameters())
                    {
                        var itemType = item.ParameterType;
                        if (itemType.IsClass && itemType.Name != "String")
                        {
                            PropertyInfo[] infos = itemType.GetProperties();
                            foreach (PropertyInfo info in infos)
                            {
                                if (info.CanRead)
                                {
                                    var propertyValue = filterContext.GetParameterDictionary()[info.Name];// 暂不支持多层嵌套 后期优化?
                                    if (!parmsObj.ContainsKey(info.Name))
                                    {
                                        parmsObj.Add(info.Name, propertyValue.EmptyNull());
                                    }
                                }
                            }
                        }
                        else
                        {
                            var parameterValue = filterContext.GetParameterDictionary()[item.ParameterType.ToString()];
                            if (!parmsObj.ContainsKey(item.ParameterType.ToString()))
                            {
                                parmsObj.Add(item.ParameterType.ToString(), parameterValue.EmptyNull());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return parmsObj;
        }
    }
}
