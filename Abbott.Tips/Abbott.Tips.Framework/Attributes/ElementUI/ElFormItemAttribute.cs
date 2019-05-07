using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ElFormItemAttribute : Attribute
    {
        public int Order { get; set; }
        public string Label { get; set; }
        public string Prop { get; set; }
        public ElFormItemType Type { get; set; }
        public object DefaultValue { get; set; }
        public object Options { get; set; }
        public bool Hidden { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ElFormItemIgnoreAttribute : Attribute
    {

    }

    public enum ElFormItemType
    {
        TEXT,
        DATE,
        DATERANGE,
        SELECT,
        CHECKBOX,
        RADIOBOX,
        SWITCH,
        TEXTAREA
    }

}
