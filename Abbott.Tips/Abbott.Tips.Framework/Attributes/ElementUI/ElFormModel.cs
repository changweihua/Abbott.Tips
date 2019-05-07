using Abbott.Tips.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.ElementUI
{
    public class ElFormModel
    {
        public string FormName { get; set; }

        public List<ElFormItemModel> FormItems { get; set; }

        //public List<ElFormItemRuleModel> FormRules { get; set; }

        public dynamic FormRules { get; set; }
    }

    public class ElFormItemModel
    {
        public int Order { get; set; }
        public string Label { get; set; }
        public string Prop { get; set; }
        public string Type { get; set; }
        public object DefaultValue { get; set; }
        public object Options { get; set; }
        public bool Hidden { get; set; }
    }

    public class ElFormItemRuleModel
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
