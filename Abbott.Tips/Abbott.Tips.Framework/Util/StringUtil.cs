using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    public static class StringUtil
    {
        static readonly char[] padding = { '=' };

        public static string SafeEncode(string source)
        {
            var bytes = Encoding.UTF8.GetBytes(source);
            return SafeEncode(bytes);
        }

        public static string SafeEncode(byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes).TrimEnd(padding).Replace('+', '-').Replace('/', '_');
        }

        public static string SafeDecode(string source)
        {
            string result = null;
            try
            {
                string incoming = source.Replace('_', '/').Replace('-', '+');
                switch (source.Length % 4)
                {
                    case 2: incoming += "=="; break;
                    case 3: incoming += "="; break;
                }
                byte[] bytes = Convert.FromBase64String(incoming);

                result = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public static string Escape(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            return source.Replace(@"""", "_DB_QUTO_").Replace("'", "_SG_QUTO_").Replace("\n", "_N_L_");
        }

        public static string Unscape(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            return source.Replace("_DB_QUTO_", @"""").Replace("_SG_QUTO_", "'").Replace("_N_L_", "\n");
        }
    }
}
