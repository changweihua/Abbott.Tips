using Abbott.Tips.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.AspnetCore.Jils
{
    /// <summary>
    /// 序列化标识特性
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property)]
    public abstract class JilSerializationFilterAttribute : SerializationFilterAttribute
    {

    }
}
