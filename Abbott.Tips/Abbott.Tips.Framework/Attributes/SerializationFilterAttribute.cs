using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Attributes
{
    /// <summary>
    /// 序列化标识特性
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property)]
    public abstract class SerializationFilterAttribute : Attribute
    {

    }
}
