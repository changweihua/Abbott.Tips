using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 首字母小写写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToLowerCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToLower() + input.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToUpperCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1);
            return str;
        }


        public static string ToPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1);
            return str;
        }
    }
}
