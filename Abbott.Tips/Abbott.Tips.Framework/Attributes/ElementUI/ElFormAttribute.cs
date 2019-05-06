using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ElFormAttribute : Attribute
    {
        public string FormName { get; set; }
    }
}
