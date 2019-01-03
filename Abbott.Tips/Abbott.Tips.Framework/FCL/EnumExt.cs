using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.Framework.FCL
{
    public class EnumListIgnoreAttribute : Attribute
    {

    }

    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstend">当枚举没有定义DescriptionAttribute,是否用枚举名代替，默认使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription(this Enum value, bool nameInstend = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null && nameInstend == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }

        /// <summary>
        /// 枚举项转为集合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<KeyValuePair<int, string>> ToList(this Enum value)
        {
            var list = new List<KeyValuePair<int, string>>();

            foreach (var item in value.GetType().GetFields())
            {
                // 获取描述
                if (item.GetCustomAttributes(false).Any(att => att.GetType() == typeof(EnumListIgnoreAttribute)))
                {
                    continue;
                }

                var attr = item.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null && !string.IsNullOrEmpty(attr.Description))
                {
                    list.Add(new KeyValuePair<int, string>((int)Enum.Parse(value.GetType(), item.Name), attr.Description));
                }
            }

            return list;
        }

        /// <summary>
        /// 枚举项转为集合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<KeyValuePair<int, Tuple<string, string>>> ToTupleList(this Enum value)
        {
            var list = new List<KeyValuePair<int, Tuple<string, string>>>();

            foreach (var item in value.GetType().GetFields())
            {
                // 获取描述
                if (item.GetCustomAttributes(false).Any(att => att.GetType() == typeof(EnumListIgnoreAttribute)))
                {
                    continue;
                }

                var attr = item.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null && !string.IsNullOrEmpty(attr.Description))
                {
                    list.Add(new KeyValuePair<int, Tuple<string, string>>((int)Enum.Parse(value.GetType(), item.Name), new Tuple<string, string>(item.Name, attr.Description)));
                }
            }

            return list;
        }

        /// <summary>
        /// 数值类型转为枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T AsEnum<T>(this int value) where T : struct
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            return default(T);
        }

        /// <summary>
        /// 整数转为枚举值集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsEnumList<T>(this int value) where T : struct
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
        /// Flags 枚举值，或运算
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int AsEnum<T>(this IEnumerable<int> values) where T : struct
        {
            return values.Aggregate((pb1, pb2) => pb1 | pb2);
        }

    }
}
