using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.FCL
{
    public static class HashtableExt
    {
        public static string GetOrDefault(this Hashtable ht, object key)
        {

            if (!ht.ContainsKey(key))
            {
                return string.Empty;
            }

            return (ht[key] ?? "").ToString();
        }
    }
}
