using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.FCL
{
    /// <summary>
    /// String 扩展类
    /// </summary>
    public static class StringExt
    {

        /// <summary>
        /// 字符串为 null 返回 ""
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EmptyNull(this string str)
        {
            return str ?? "";
        }

    }
}
