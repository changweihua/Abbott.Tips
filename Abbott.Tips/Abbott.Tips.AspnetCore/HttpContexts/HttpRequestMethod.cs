using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.HttpContexts
{
    /// <summary>
    /// 请求支持类型
    /// </summary>
    [Flags]
    public enum HttpRequestMethod
    {
        GET = 0x1,
        POST = 0x2,
        PUT = 0x4,
        HEAD = 0x8,
        OPTIONS = 0x10,
        TRACE = 0x20,
        DELETE = 0x40
    }

    public static class HttpRequestMethodExt
    {
        public static string GetHttpRequestMethodNameList(this HttpRequestMethod value)
        {
            List<string> names = new List<string>();

            foreach (HttpRequestMethod m in Enum.GetValues(typeof(HttpRequestMethod)))
            {
                if (((HttpRequestMethod)m & value) != 0)
                {
                    names.Add(m.ToString());
                }
            }

            return string.Join(", ", names);
        }
    }
}
