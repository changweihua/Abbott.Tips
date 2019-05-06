using Abbott.Tips.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.FCL
{
    public static class TypeExt
    {
        public static ElFormItemType ToElFormItemType(this Type type)
        {
            var formItemType = ElFormItemType.TEXT;
            switch (type.Name)
            {
                case "DateTime":
                    formItemType = ElFormItemType.DATE;
                    break;
                case "Int32":
                case "Int64":
                    formItemType = ElFormItemType.TEXT;
                    break;
                case "Boolean":
                    formItemType = ElFormItemType.RADIOBOX;
                    break;
                case "String":
                default:
                    break;
            }

            return formItemType;
        }
    }
}
