using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SecondNameAttribute : Attribute
    {
        public string Name { get; set; }
    }

    [AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple = false)]
    public class MapperPropertyAttribute : Attribute
    {
        private string _targetName;
        private Type _targetType;

        public string TargetName
        {
            get
            {
                return _targetName;
            }
        }

        public Type TargetType
        {
            get
            {
                return _targetType;
            }
        }

        public MapperPropertyAttribute(string targetName, Type targetType)
        {
            _targetName = targetName;
            _targetType = targetType;
        }
    }

    [AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple = false)]
    public class MapperIgnoreAttribute : Attribute
    {
        private bool _ignore;
        private Type _targetType;

        public bool Ignore
        {
            get
            {
                return _ignore;
            }
        }

        public Type TargetType
        {
            get
            {
                return _targetType;
            }
        }

        public MapperIgnoreAttribute(bool ignore, Type targetType)
        {
            _ignore = ignore;
            _targetType = targetType;
        }
    }

    /// <summary>
    /// 模型转换
    /// </summary>
    public sealed class MapperUtil
    {
        public static PropertyInfo[] GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        /// <summary>
        /// 实体属性反射
        /// </summary>
        /// <typeparam name="S">赋值对象</typeparam>
        /// <typeparam name="T">被赋值对象</typeparam>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public static void AutoMapping<S, T>(S s, T t)
        {
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            Type target = t.GetType();

            foreach (var pp in pps)
            {

                //查看当前属性是否需要忽略
                var ignoreAttr = pp.GetCustomAttribute<MapperIgnoreAttribute>();

                if (ignoreAttr != null)
                {
                    return;
                }

                var mapperAttr = pp.GetCustomAttribute<MapperPropertyAttribute>();
                var targetPPName = pp.Name;

                if (mapperAttr != null && !string.IsNullOrEmpty(mapperAttr.TargetName) && t.GetType() == mapperAttr.TargetType)
                {
                    targetPPName = mapperAttr.TargetName;
                }

                PropertyInfo targetPP = target.GetProperty(targetPPName);

                object value = pp.GetValue(s, null);

                if (targetPP != null && value != null)
                {
                    targetPP.SetValue(t, value, null);
                }
                else if (targetPP != null && value == null && targetPP.PropertyType.IsGenericType && targetPP.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    NullableConverter nCvter = new NullableConverter(targetPP.PropertyType);
                    targetPP.SetValue(t, nCvter.ConvertFrom(value), null);
                }
            }
        }


        /// <summary>
        /// 实体属性复制值
        /// </summary>
        /// <typeparam name="S">赋值对象</typeparam>
        /// <typeparam name="T">被赋值对象</typeparam>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public static void ReverseMapping<S, T>(S s, T t)
        {
            PropertyInfo[] ppt = GetPropertyInfos(t.GetType());
            PropertyInfo[] pps = GetPropertyInfos(s.GetType());
            Type source = s.GetType();
            Type target = t.GetType();

            foreach (var pp in ppt)
            {
                PropertyInfo targetPP = target.GetProperty(pp.Name);
                string propName = pp.Name;
                SecondNameAttribute attr = targetPP.GetCustomAttribute<SecondNameAttribute>();
                if (attr != null)
                {
                    propName = attr.Name;
                }
                PropertyInfo sourcePP = source.GetProperty(propName);
                if (sourcePP != null && targetPP.PropertyType == sourcePP.PropertyType)
                {

                    if (sourcePP != null)
                    {
                        object value = sourcePP.GetValue(s, null);

                        if (targetPP != null && value != null)
                        {
                            targetPP.SetValue(t, value, null);
                        }
                    }
                }
            }
        }
    }
}
