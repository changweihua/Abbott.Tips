using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.FCL
{
    /// <summary>
    /// Lambda 扩展类
    /// </summary>
    public static class LambdaExt
    {
        // var query = people.DistinctBy(p => p.Id);
        // var query = people.DistinctBy(p => new { p.Id, p.Name });
        /// <summary>
        /// 去重复数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">用于去重的表达式，单个字段如：var query = people.DistinctBy(p => p.Id);多个字段如：var query = people.DistinctBy(p => new { p.Id, p.Name });</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
