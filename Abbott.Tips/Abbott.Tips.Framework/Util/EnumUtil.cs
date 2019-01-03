using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public sealed class EnumUtil
    {
        public static IEnumerable<T> GetEnumValuesFromFlagsEnum<T>(Int32 value) where T : struct
        {
            T[] values = (T[])Enum.GetValues(typeof(T));

            foreach (var itemValue in values)
            {
                if ((value & Convert.ToInt32(itemValue)) != 0)
                {
                    yield return itemValue;
                }
            }
        }

        /// <summary>
        /// 获取枚举描述列表，并转化为键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isHasAll">是否包含“全部”</param>
        /// <param name="filterItem">过滤项</param>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> ToKVList<T>()
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();

            #region 方式一
            foreach (var field in typeof(T).GetFields())
            {
                // 获取描述
                var attr = field.GetCustomAttribute(typeof(DescriptionAttribute), true) as DescriptionAttribute;
                if (attr != null && !string.IsNullOrEmpty(attr.Description))
                {
                    var key = (int)Enum.Parse(typeof(T), field.Name);
                    var value = attr.Description;
                    list.Add(new KeyValuePair<int, string>(key, value));
                }
            }
            #endregion

            #region 方式二
            //foreach (int item in Enum.GetValues(typeof(T)))
            //{
            //    // 获取描述
            //    FieldInfo fi = typeof(T).GetField(Enum.GetName(typeof(T), item));
            //    var attr = fi.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
            //    if (attr != null && !string.IsNullOrEmpty(attr.Description))
            //    {
            //        // 跳过过滤项
            //        if (Array.IndexOf<string>(filterItem, attr.Description) != -1)
            //        {
            //            continue;
            //        }
            //        // 添加
            //        EnumKeyValue model = new EnumKeyValue();
            //        model.Key = item;
            //        model.Name = attr.Description;
            //        list.Add(model);
            //    }
            //} 
            #endregion

            return list;
        }

        public static List<string> GetEnumNameList<T>(T model)
        {
            List<string> names = new List<string>();

            //if (Enum.IsDefined(typeof(T), value))
            //{
            //    return (T)Enum.ToObject(typeof(T), value);
            //}

            Array enumValues = Enum.GetValues(typeof(T));

            foreach (var field in typeof(T).GetFields())
            {
                Enum.Parse(typeof(T), field.Name);
            }



            return names;
        }
    }
}
