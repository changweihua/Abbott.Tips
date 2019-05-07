using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ElFormItemRuleAttribute : Attribute
    {
        public string Type { get; set; }
        public bool Required { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public string Trigger { get; set; }
        public List<string> Triggers { get; set; }
        public string Message { get; set; }
    }
}
