using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    /// <summary>
    /// Hashtable DTO Model 转换辅助类
    /// </summary>
    public sealed class HashtableUtil
    {
        /// <summary>
        /// Hashtable 转为 DTO Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static T Convert2Model<T>(Hashtable ht) where T : class
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            Type type = typeof(T);
            var model = (T)Activator.CreateInstance(type);

            foreach (var key in ht.Keys)
            {
                var prop = propertyInfos.FirstOrDefault(_ => _.Name == key.ToString());
                if (prop != null && prop.CanWrite)
                {
                    var keyValue = ht[key].ToString();
                    switch (prop.PropertyType.Name)
                    {
                        case "Int32":
                            if (!string.IsNullOrEmpty(keyValue))
                            {
                                int value = 0;
                                Int32.TryParse(keyValue, out value);
                                prop.SetValue(model, value, null);
                            }
                            break;
                        case "Int16":
                            if (!string.IsNullOrEmpty(keyValue))
                            {
                                short value = 0;
                                Int16.TryParse(keyValue, out value);
                                prop.SetValue(model, value, null);
                            }
                            break;
                        case "DateTime":
                            if (!string.IsNullOrEmpty(keyValue))
                            {
                                DateTime value;
                                DateTime.TryParse(ht[key].ToString(), out value);
                                prop.SetValue(model, value, null);
                            }
                            break;
                        case "Decimal":
                            if (!string.IsNullOrEmpty(keyValue))
                            {
                                decimal value = 0;
                                Decimal.TryParse(keyValue, out value);
                                prop.SetValue(model, value, null);
                            }
                            break;
                        case "Double":
                            if (!string.IsNullOrEmpty(keyValue))
                            {
                                double value = 0;
                                Double.TryParse(keyValue, out value);
                                prop.SetValue(model, value, null);
                            }
                            break;
                        case "String":
                        default:
                            prop.SetValue(model, keyValue ?? "", null);
                            break;
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// DTO Model 转 Hashtable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Hashtable Convert2Hashtable<T>(T model) where T : class
        {
            Hashtable _ht = new Hashtable();
            PropertyInfo[] propertyInfos = model.GetType().GetProperties();
            foreach (PropertyInfo item in propertyInfos)
            {
                _ht.Add(item.Name, item.GetValue(model, null));
            }
            return _ht;
        }

        /// <summary>
        /// 根据默认字段及值，将Hashtable参数填充完整
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="defaults"></param>
        /// <returns></returns>
        public static Hashtable PrepareHashtable(Hashtable ht, IList<KeyValuePair<string, object>> defaults)
        {
            if (defaults == null || defaults.Count == 0)
            {
                return ht;
            }

            if (ht == null)
            {
                ht = new Hashtable();
            }

            foreach (var item in defaults)
            {
                if (!ht.ContainsKey(item.Key))
                {
                    ht[item.Key] = item.Value;
                }
            }


            return ht;
        }

        /// <summary>
        /// 根据默认字段及值，将Hashtable参数填充完整
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="defaults"></param>
        /// <returns></returns>
        public static Hashtable PrepareHashtable(Hashtable ht, IDictionary<string, object> defaults)
        {
            if (defaults == null || defaults.Count == 0)
            {
                return ht;
            }

            if (ht == null)
            {
                ht = new Hashtable();
            }

            foreach (var item in defaults)
            {
                if (!ht.ContainsKey(item.Key))
                {
                    ht[item.Key] = item.Value;
                }
            }


            return ht;
        }
    }
}
