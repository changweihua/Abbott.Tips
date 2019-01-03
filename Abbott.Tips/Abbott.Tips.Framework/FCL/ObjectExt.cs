using Abbott.Tips.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.Framework.FCL
{
    /// <summary>
    /// 实现对象的深复制
    /// </summary>
    public static class ObjectExt
    {
        /// <summary>
        /// Object 转 String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string NullEmpty(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }

        /// <summary>
        /// 获取 BindingFlags.Public | BindingFlags.Instance 特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public static IEnumerable<string> DifferentFields(this object obj, object target, IEnumerable<string> includeFields, bool ignoreCase = false)
        {

            //待比较的两个对象类型不一致
            if((obj == null || target == null) || (obj.GetType() != target.GetType()))
            {
                return null;
            }

            //需要判断的属性为空，表示全部比较，只比较简单类型的数据，复杂的直接 ToString()
            if(includeFields == null || includeFields.Count() == 0)
            {

            }

            PropertyInfo[] sourceProps = obj.GetType().GetPublicProperties();
            PropertyInfo[] targetProps = target.GetType().GetPublicProperties();

            foreach (var pp in sourceProps)
            {
                //查看当前属性是否需要忽略
                var ignoreAttr = pp.GetCustomAttribute<MapperIgnoreAttribute>();

                if (ignoreAttr != null || (includeFields ?? new List<string>()).Contains(pp.Name))
                {
                    continue;
                }

                var mapperAttr = pp.GetCustomAttribute<MapperPropertyAttribute>();
                var targetPPName = pp.Name;

                //if (mapperAttr != null && !string.IsNullOrEmpty(mapperAttr.TargetName) && t.GetType() == mapperAttr.TargetType)
                //{
                //    targetPPName = mapperAttr.TargetName;
                //}

                //PropertyInfo targetPP = target.GetProperty(targetPPName);

                //object value = pp.GetValue(s, null);

                //if (targetPP != null && value != null)
                //{
                //    targetPP.SetValue(t, value, null);
                //}
                //else if (targetPP != null && value == null && targetPP.PropertyType.IsGenericType && targetPP.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                //{
                //    NullableConverter nCvter = new NullableConverter(targetPP.PropertyType);
                //    targetPP.SetValue(t, nCvter.ConvertFrom(value), null);
                //}
            }

            IEnumerable<string> fields = new List<string>();


            return fields;
        }

    }

}
