using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    public static class ReflectionUtil
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static object CreateInstance(string assemblyName, string classFullName)
        {
            try
            {
                object ect = Assembly.LoadFrom(assemblyName).CreateInstance(classFullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch (Exception ex)
            {
                //发生异常，返回类型的默认值
                return null;
            }
        }

        #region 反射调用某个类的某个方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="classFullName"></param>
        /// <param name="methodName"></param>
        /// <param name="parameterTypes"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static object TryInvoke(string assemblyName, string classFullName, string methodName, IList<Type> parameterTypes, IList<object> parameterValues)
        {
            //加载程序集(dll文件地址)，使用Assembly类   
            Assembly assembly = Assembly.LoadFrom(assemblyName);

            //获取类型，参数（名称空间+类）   
            Type type = assembly.GetType(classFullName);

            //创建该对象的实例，object类型，参数（名称空间+类）   
            object instance = assembly.CreateInstance(classFullName);

            //执行Show_Str方法   
            object value = type.GetMethod(methodName, parameterTypes.ToArray()).Invoke(instance, parameterValues.ToArray());

            return value;
        }

        #endregion

    }
}
