using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abbott.Tips.Model.Entities
{
    [Table("T_Configuration")]
    public class ConfigurationModel : TipsEntity
    {
        public int ConfigType { get; set; }
        public string ConfigValue { get; set; }
        public string ConfigName { get; set; }
        public string ConfigDescription { get; set; }
    }

    public enum ConfigType
    {
        SITE = 1
    }
}
